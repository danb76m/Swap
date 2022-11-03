using System;
using System.Threading;
using System.Threading.Tasks;

namespace Swap
{
    class Program
    {

        private static readonly Random random = new Random();
        private static readonly int INC = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
             
            for (int x = 0; x < 100000000; x+=INC)
            {
                //Task.Factory.StartNew(() => test(x));
                test(x);
            }
            

            Console.ReadLine();
        }

        private static void test(int size)
        {
            int[] numbers = randomNumbers(size, 0, 100);
            int[] numbersClone = clone(numbers);

            long insert = insertSort(numbers);
            long bubble = bubbleSort(numbersClone);

            Console.WriteLine(size + " items: " + insert + "ms vs " + bubble + "ms (" + (bubble/(insert == 0 ? 0.00000001 : insert)) + ")");
        }

        public static long insertSort(int[] array)
        {
            long l = Environment.TickCount;
            for (int i = 1; i < array.Length; i++)
            {
                int x = i;
                while(x > 0 && array[x-1] > array[x])
                {
                    int temp = array[x-1];
                    //int temp2 = array[x];
                    array[x - 1] = array[x];
                    array[x] = temp;
                    x = x - 1;
                }
            }

            return Environment.TickCount - l;
        }

        public static long bubbleSort(int[] array)
        { // Bubble Sort
            long l = Environment.TickCount;

            Boolean solved = false;
            int len = array.Length - 1;

            while (!solved)
            {
                Boolean s = false;
                for (int i = 0; i < len; i++)
                {
                    int x = array[i];
                    int z = array[i + 1];

                    if (x > z)
                    {
                        array[i] = z;
                        array[i + 1] = x;
                        s = true;
                    }
                }

                if (!s)
                {
                    solved = true;
                }

                len--;
            }
            return Environment.TickCount - l;
        }

        public static int search(int[] array, int search) // binary search
        {
            bubbleSort(array);

            int lB = 0;
            int uB = array.Length;


            while (true)
            {
                if (uB < lB) return -1;
                int mid = lB + (uB - lB) / 2;

                if (array[mid] == search) return mid;
                if (array[mid] < search)
                {
                    lB = mid + 1;
                };
                if (array[mid] > search)
                {
                    uB = mid - 1;
                }
            }
        }

        private static int[] clone(int[] array)
        {
            int[] x = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                x[i] = array[i];
            }
            return x;
        }
        private static int[] randomNumbers(int size, int min, int max)
        {
            int[] array = new int[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(min, max);
            }
            return array;
        }


    }

}
