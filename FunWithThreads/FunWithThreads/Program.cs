using System;

namespace FunWithThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            Engene engene = new Engene();
            engene.Run();
            Console.ReadKey();   
        }
    }
}
