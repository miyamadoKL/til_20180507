using System;
using System.Threading;
using System.Threading.Tasks;

namespace tasktest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing WaitAny Method.
            var tasksA = new Task[3];
            var rnd = new Random();
            for (int i = 0; i < 3; i++)
                tasksA[i] = Task.Run( () => Thread.Sleep(rnd.Next(500, 3000)));

            try {
                int index = Task.WaitAny(tasksA);
                Console.WriteLine("Task #{0} completed first.\n", tasksA[index].Id);
                Console.WriteLine("Status of all tasks:");
                foreach (var t in tasksA)
                    Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
            }
            catch (AggregateException) {
                Console.WriteLine("An exception occurred.");
            }

            // Testing WaitAll Method.
            var tasksB = new Task[9];
            for (int i = 0; i < 9; i++)
            {
                tasksB[i] = Task.Run(() => Thread.Sleep(2000));
            }
            try {
                Task.WaitAll(tasksB);
            } catch (AggregateException ae) {
                    Console.WriteLine("One or more exceptions occured.");
                    foreach (var ex in ae.Flatten().InnerExceptions)
                        Console.WriteLine(" {0}", ex.Message);
            }
            Console.WriteLine("\nStatus of all tasks:");
            foreach (var t in tasksB)
                Console.WriteLine(" Task #{0}: {1}", t.Id, t.Status);
        }
    }
}
