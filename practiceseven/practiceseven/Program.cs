using System;
using System.Collections.Generic;



namespace t7
{
    class BinaryTree : IComparable
    {
        public BinaryTree Left = null;
        public BinaryTree Right = null;
        public double Value;
        public string Word;

        public BinaryTree()
        { }
        public BinaryTree(double value)
        {
            this.Value = value;
        }

        public void Print(int l = 0)
        {
            if (this.Left != null)
                this.Left.Print(l + 3);   // переход к левому поддереву

            for (int i = 0; i < l; i++)   // формирование отступа
                Console.Write(" ");
            Console.WriteLine(this.Word); // печать узла
            if (this.Right != null)
                this.Right.Print(l + 3);  // переход к правому поддереву
        }

        public int CompareTo(object obj)
        {
            BinaryTree tree = obj as BinaryTree;
            if (this.Value < tree.Value)
                return -1;
            if (this.Value > tree.Value)
                return 1;
            else
                return 0;
        }

        public override string ToString()
        {
            return "tree: " + this.Value;
        }
    }

    class Words
    {
        private static List<string> listWord;

        public static List<string> GetWords(BinaryTree mainTree)
        {
            listWord = new List<string>();
            _getWords(mainTree);
            return listWord;
        }
        private static void _getWords(BinaryTree tree, string lastLetters = "")
        {
            if (tree.Right == null && tree.Left == null)
            {
                tree.Word = lastLetters;
                listWord.Add(lastLetters);
            }
            if (tree.Right != null)
                _getWords(tree.Right, lastLetters + "0");
            if (tree.Left != null)
                _getWords(tree.Left, lastLetters + "1");
        }
    }

    class Program
    {
        static double GetListSum(List<double> list)
        {
            double sum = 0;
            foreach (var t in list)
                sum += t;
            return sum;
        }

        static double GetHuffmanElongation(List<double> frequences, List<string> codes) // удлиннение при кодировании Хаффманом
        {
            codes.Reverse();
            double result = 0;

            for (int i = 0; i < codes.Count; i++)
            {
                result += (frequences[i] * codes[i].Length);
            }
            return result;
        }

        static int GetUniformElongation(int codesCount) // удлинение при равномерном кодировании
        {
            int i = 0;
            while (Math.Pow(2, i) < codesCount)
            {
                i++;
            }
            return i;
        }
        public static double Readdouble()
        //ввод числа для красивых лаб
        {
            bool check = false;
            double doubleNum;
            do
            {
                // Попытка перевести строку в число
                check = Double.TryParse(Console.ReadLine(), out doubleNum);
                // Если попытка неудачная:
                if (!check)
                    Console.WriteLine("Некорректный ввод. Попробуйте ещё раз");
            } while (!check);
            // Если попытка удачная:
            return doubleNum;
        }
        static void Main(string[] args)
        {
            List<double> frequencesList = new List<double>();

            Console.WriteLine("Введите частоту. \nКогда сумма достигнет единицу, ввод будет остановлен.");
            while (true)
            {
                double current = Readdouble();
                frequencesList.Add(current);
                double sum = GetListSum(frequencesList);
                if (sum > 1)
                {
                    Console.WriteLine("Сумма введенных частот больше 1! Введенный список очищен, повторите ввод.");
                    frequencesList = new List<double>(); // если введённые числа в сумме превысили единицу
                }
                if (sum == 1)
                {
                    Console.WriteLine("Ввод законче.");
                    break;
                }
            }

            Console.WriteLine("\nВведенная частота:");
            frequencesList.Sort();
            frequencesList.Reverse();
            foreach (var t in frequencesList)
                Console.WriteLine(t);

            List<BinaryTree> treeList = new List<BinaryTree>();
            for (int i = 0; i < frequencesList.Count; i++)
            {
                treeList.Add(new BinaryTree(frequencesList[i]));
            }

            while (treeList.Count > 1)
            {
                treeList.Sort();
                BinaryTree first = treeList[0];
                BinaryTree second = treeList[1];

                treeList[1] = new BinaryTree(first.Value + second.Value);
                treeList[1].Right = first;
                treeList[1].Left = second;
                treeList.RemoveAt(0);


            }

            List<string> codesList = Words.GetWords(treeList[0]);

            Console.WriteLine("\nРезультат: ");
            foreach (var t in codesList)
                Console.WriteLine(t);

            Console.WriteLine("Средняя длина кодового слова(Код Хаффмана): " + GetHuffmanElongation(frequencesList, codesList));

            Console.WriteLine("Длина кода(универсальный код): " + GetUniformElongation(codesList.Count));

            Console.ReadLine();
        }
    }
}