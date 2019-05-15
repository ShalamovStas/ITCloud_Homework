using MyLibrary;
using System;

namespace GACAssemblyShalamov
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            LibraryHelper.ShowLibraryInfo_Debug();
#else
            LibraryHelper.ShowLibraryInfo();
#endif

            Console.ReadKey();

        }
    }
}
