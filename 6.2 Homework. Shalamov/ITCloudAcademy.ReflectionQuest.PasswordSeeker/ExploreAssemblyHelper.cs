using ITCloudAcademy.ReflectionQuest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class ExploreAssemblyHelper
    {
        private SortedDictionary<int?, string> keyValuePairs = new SortedDictionary<int?, string>();

        public string ExploreAssembly(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            Console.WriteLine(assembly.FullName);

            foreach (Type t in assembly.GetTypes())
                ExploreType(t);

            Console.ForegroundColor = ConsoleColor.White;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in keyValuePairs)
                stringBuilder.Append(item.Value);

            return stringBuilder.ToString();
        }

        private void ExploreType(Type type)
        {
            Console.WriteLine(Environment.NewLine + new string('-', 20));

            var needToExplore = ExploreAttributeIfTypeNeedToExplore(type.GetCustomAttribute<IgnoreMeAttribute>());

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(type.Name + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;

            if (needToExplore == false)
                return;

            ExploreProperties(type);
            ExploreFields(type);
            ExploreMethods(type);
        }

       

        private void ExploreProperties(Type type)
        {
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                int? key = ExploreAttributeSearchingForKey(property.GetCustomAttribute<PasswordIsHereAttribute>());

                string res = (string)property.GetValue(CreateInstance(type), null);
                Console.WriteLine($"{property.Name} [{res}]\n");
                if (key != null)
                    SavePartOfKey(key, res);
            }
        }

        private void ExploreMethods(Type type)
        {
            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in methods)
            {
                int? key = ExploreAttributeSearchingForKey(method.GetCustomAttribute<PasswordIsHereAttribute>());

                if (key != null)
                {
                    string res = (string)method.Invoke(CreateInstance(type), null);

                    Console.WriteLine($"{method.Name} [{res}]\n");

                    SavePartOfKey(key, res);
                }
            }
        }

        private void ExploreFields(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                int? key = ExploreAttributeSearchingForKey(field.GetCustomAttribute<PasswordIsHereAttribute>());

                string res = (string)field.GetValue(CreateInstance(type));
                Console.WriteLine($"{field.Name} [{res}]\n");
                if (key != null)
                    SavePartOfKey(key, res);
            }
        }


        private int? ExploreAttributeSearchingForKey(PasswordIsHereAttribute attribute)
        {
            if (attribute == null)
                return null;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[PasswordIsHere(ChunkNo = {attribute.ChunkNo})]");
            Console.ForegroundColor = ConsoleColor.White;
            return attribute.ChunkNo;
        }

        private bool ExploreAttributeIfTypeNeedToExplore(IgnoreMeAttribute attribute)
        {
            if (attribute == null)
                return true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[IgnoreMeAttribute]");
            Console.ForegroundColor = ConsoleColor.White;
            return false;
        }

        
        private void SavePartOfKey(int? index, string key)
        {
            keyValuePairs.Add(index, key);
        }

        private object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
