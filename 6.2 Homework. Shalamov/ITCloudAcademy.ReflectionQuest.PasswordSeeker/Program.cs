using System;
using System.IO;
using System.Reflection;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class Program
    {
        static void Main()
        {
            var path = @"..\..\ITCloudAcademy.ReflectionQuest.Password.dll";

            ExploreAssemblyHelper explorer = new ExploreAssemblyHelper();
            string password = explorer.ExploreAssembly(path);
            Console.WriteLine(password);
           
            Console.ReadKey();
        }

    }
}




