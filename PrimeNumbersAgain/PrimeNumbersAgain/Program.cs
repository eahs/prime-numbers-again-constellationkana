using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PrimeNumbersAgain
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, prime;
            Stopwatch timer = new Stopwatch();

            PrintBanner();
            n = GetNumber();

            timer.Start();
            prime = FindNthPrime(n);
            timer.Stop();
            
            
            Console.WriteLine($"\nToo easy.. {prime} is the nth prime when n is {n}. I found that answer in {timer.Elapsed.TotalSeconds:F3} seconds."); //changed to elapsed total seconds in order to make seconds exact

            EvaluatePassingTime(timer.Elapsed.Seconds);
        }

        static int FindNthPrime(int n) //sieve of Eratosthenes implementation, everything else was too slow
        {
            int limit = (int)(n * (Math.Log(n) + Math.Log(Math.Log(n)))) + 3; // upper bound according to the prime number theorem
            // +3 ensures that for small n values we don't underestimate the limit, e.g. n=1 should give limit=3 to include the prime number 2
            // the upper bound is calculated by multiplying n by the natural logarithm of n plus the natural logarithm of the natural logarithm of n
            // this gives a good approximation of where the nth prime lies
            bool[] isPrime = new bool[limit + 1]; // if false, index is prime
            int count = 0;
            for (int i = 2; i <= limit; i++) // starts from the first prime then marks all multiples as composite (not prime)
            {
                if (!isPrime[i]) // not composite, then prime
                {
                    if (++count == n)
                        return i;

                    for (long j = (long)i * i; j <= limit; j += i) // mark multiples of i as composite
                    // this uses "long" because ints only go up to 2.1 billion.  So for example if i was 50000 it would exceed int limit
                        isPrime[j] = true;
                }
            }
            return -1; // this will only return if n is an invalid number (less than 1)
        }


        static int GetNumber()
        {
            int n = 0;
            while (true)
            {
                Console.Write("Which nth prime should I find?: ");
                
                string num = Console.ReadLine();
                if (Int32.TryParse(num, out n))
                {
                    return n;
                }

                Console.WriteLine($"{num} is not a valid number.  Please try again.\n");
            }
        }

        static void PrintBanner()
        {
            Console.WriteLine(".................................................");
            Console.WriteLine(".#####...#####...######..##...##..######...####..");
            Console.WriteLine(".##..##..##..##....##....###.###..##......##.....");
            Console.WriteLine(".#####...#####.....##....##.#.##..####.....####..");
            Console.WriteLine(".##......##..##....##....##...##..##..........##.");
            Console.WriteLine(".##......##..##..######..##...##..######...####..");
            Console.WriteLine(".................................................\n\n");
            Console.WriteLine("Nth Prime Solver O-Matic Online..\nGuaranteed to find primes up to 2 million in under 30 seconds!\n\n");
            
        }

        static void EvaluatePassingTime(int time)
        {
            Console.WriteLine("\n");
            Console.Write("Time Check: ");

            if (time <= 10)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Pass");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fail");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            
        }
    }
}
