using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deviders
{
    class Program
    {
        static List<int> list = new List<int>();
        static List<int> FindPrimes(int max)
        {
            if (max <= 1)
                return new List<int>();
            bool[] matrix = new bool[max+1];
            matrix.Initialize();
            matrix[0] = matrix[1] = true;
            for (int i = 2; i <= max; i++)
            {
                if (!matrix[i])
                {
                    for (int j = 2*i; j <= max; j += i)
                    {
                        matrix[j] = true;
                    }
                }
            }
            List<int> list = new List<int>();
            for(int i = 2; i <= max; i++)
            {
                if (!matrix[i])
                    list.Add(i);
            }
            return list;
        }

        static List<int> AllGroup(List<int> primes, int num)
        {
            list.Clear();
            List<int> deviders = new List<int>();
            int countOfRounds = 1, leftRounds = 0, devider;
            while(countOfRounds <= primes.Count)
            {
                int previos = -1;
                for(int i = 0; i < primes.Count; i++)
                {
                    if(primes[i] > previos)
                    {
                        devider = primes[i];
                        int n = AllGroup(primes, num, leftRounds,i+1,devider);
                        previos = primes[i];
                    }
                }
                countOfRounds++;
                leftRounds = countOfRounds - 1;
            }
            return list;
        }
        static int AllGroup(List<int> primesParam, int num, int leftRoundsParam, int minIndex, int devider)
        {
            if (leftRoundsParam > 0)
            {
                int deviderCopy,next;
                try {
                     next = primesParam[minIndex];
                }
                catch (Exception)
                {
                    return devider;
                }
                deviderCopy = devider * next; 
                int n = AllGroup(primesParam, num, --leftRoundsParam, minIndex + 1, deviderCopy);
                for (int i = minIndex + 1; i < primesParam.Count; i++)
                {
                    if (primesParam[i] > next)
                    {
                        deviderCopy = devider * primesParam[i];
                        n = AllGroup(primesParam, num, --leftRoundsParam, i + 1,deviderCopy);
                        next = primesParam[i];
                    }
                }
                return devider;
            }
            else
            {
                if (!list.Contains(devider))
                {
                    list.Add(devider);
                    list.Add(num / devider);
                }
                return devider;
            }
        }
        static int[] FindDeviders(int num)
        {
            if (list.Count < 1)
                return new int[1];
            int numCopy = num;
            int p = list[0];
            int counter = 1;
            List<int> primes = new List<int>();
            while (num > 1)
            {
                if(num % p == 0)
                {
                    primes.Add(p);
                    num /= p;
                }
                else
                {
                    p = list[counter++];
                }
            }
            List<int> deviders = AllGroup(primes, numCopy);
            deviders.Sort();
            return deviders.ToArray();
        }
        static void Main(string[] args)
        {
            Console.Write("Enter the number: ");
            int num = Convert.ToInt32(Console.ReadLine());
            list = FindPrimes(num);
            int[] dev = FindDeviders(num);
            Console.WriteLine("\nDeviders");
            foreach (int el in dev)
                Console.WriteLine(el);
            Console.ReadLine();
        }
    }
}
