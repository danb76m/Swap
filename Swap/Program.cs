using System;
using System.Threading;
using System.Threading.Tasks;

namespace Swap
{
    class Program
    {

        private static readonly Random random = new Random();
        private static readonly int INC = 10000;
        private static readonly int MAX = 10000000;

        private static readonly int[] NUMBERS = randomNumbers(MAX, 0, 1000);
        private static readonly int[] LIST = randomNumbers(20, 0, 100);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

                         for (int x = 0; x < MAX; x+=INC)
            {

                int[] arr = new int[x];
                for (int i = 0; i < x; i++)
                {
                    arr[i] = NUMBERS[i];
                }
                //Task.Factory.StartNew(() => test(x));
                test(arr);
            }
            
           
        }

        private static String format(int[] array)
        {
            return "[" + String.Join(", ", array) + "]";
        }

        private static void test(int[] numbers)
        {
            
            //int[] numbers = randomNumbers(size, 0, 100);
            int[] numbersClone = clone(numbers);
            int[] mergeClone = clone(numbers);

            long merge = mergeSortTest(mergeClone, new int[mergeClone.Length], 0, mergeClone.Length - 1);
            long insert = 0; //insertSort(numbers);
            long bubble = 0; //bubbleSort(numbersClone);
            

            Console.WriteLine(numbers.Length + " items: " + insert + "ms vs " + bubble + "ms vs " + merge + "ms");
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


        // Recursion
        public static int countdown(int number)
        {
            Thread.Sleep(1000);
            Console.WriteLine(number + " second" + (number == 1 ? "" : "s") + " remain");
            if (number-- == 0) return number;
            return countdown(number);
        }

        // GCD Recursion
        public static int gcd(int num1, int num2)
        {
            if (num2 == 0) return num1;
            Console.WriteLine(num1 + "," + num2);
            return gcd(num2, num1 % num2);
        }

        // GCD Iterative
        public static int gcdIterative(int num1, int num2)
        {
            while(num2 != 0)
            {
                Console.WriteLine(num1 + "," + num2);
                int temp = num2;
                num2 = num1 % temp;
                num1 = temp;
            }
            return num1;
        }

        // Merge Sort (Recursive)
        public static long mergeSortTest(int[] unsorted, int[] temp, int leftStart, int rightEnd)
        {
            long l = Environment.TickCount;
            mergeSort(unsorted, temp, leftStart, rightEnd);
            return Environment.TickCount - l;
        }

            public static void mergeSort(int[] unsorted, int[] temp, int leftStart, int rightEnd)
        {
            if (leftStart >= rightEnd) return; // Stops recursive calls because the lists are off size 1
            int mid = (leftStart + rightEnd) / 2; // Get middle
            mergeSort(unsorted, temp, leftStart, mid);
            mergeSort(unsorted, temp, mid + 1, rightEnd);

            mergeHalves(unsorted, temp, leftStart, rightEnd);
        }

        public static void mergeHalves(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            int leftEnd = (rightEnd + leftStart) / 2;
            int rightStart = leftEnd + 1;
            int size = rightEnd - leftStart + 1;

            // these hold the index of the start points on the right and left list, index is the index we are at in our temp array that we are copying our numbers tos
            int left = leftStart;
            int right = rightStart;
            int index = leftStart;
            while(left <= leftEnd && right <= rightEnd) // Looping over the arrays until one of the arrays is empty so we can join that onto the new temp list
            {
                if (array[left] <= array[right])
                {
                    temp[index] = array[left];
                    left++;
                } else
                {
                    temp[index] = array[right];
                    right++;
                }
                index++;
            }

            // Copying remaining list
            Array.Copy(array, left, temp, index, leftEnd - left + 1);
            Array.Copy(array, right, temp, index, rightEnd - right + 1);
            Array.Copy(temp, leftStart, array, leftStart, size);
            //Console.WriteLine(format(array));
        }

    }

}
