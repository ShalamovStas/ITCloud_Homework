using System;
using System.IO;
using System.Reflection;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class Program
    {
        static void Main()
        {
            var path = @"E:\ITCloud\Сборки\6.2 Homework. Student\6.2 Homework. Student\ITCloudAcademy.ReflectionQuest.PasswordSeeker\ITCloudAcademy.ReflectionQuest.Password.dll";
            var explorer = new ExploreAssemblyHelper();
            explorer.ExploreAssembly(path);
            Console.WriteLine(explorer.Password);
            Console.ReadKey();
        }
    }
}
