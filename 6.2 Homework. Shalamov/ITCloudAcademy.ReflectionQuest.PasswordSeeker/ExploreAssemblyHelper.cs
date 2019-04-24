using ITCloudAcademy.ReflectionQuest.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class ExploreAssemblyHelper
    {
        private SortedDictionary<int, string> keyValuePairs = new SortedDictionary<int, string>();
        private InstanceManager instanceManager = new InstanceManager();

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
            Console.WriteLine(instanceManager.Count);
            return stringBuilder.ToString();
        }

        private void ExploreType(Type type)
        {
            Console.WriteLine(Environment.NewLine + new string('-', 20));

            var needToExplore = ExploreAttributeIfTypeNeedToExplore(type);

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
                int? key = ExploreAttributeSearchingForKey(property);

                string res = (string)property.GetValue(instanceManager.GetInstance(type), null);
                Console.WriteLine($"{property.Name} [{res}]\n");
                if (key != null)
                    SavePartOfKey((int)key, res);
            }
        }

        private void ExploreMethods(Type type)
        {
            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in methods)
            {
                int? key = ExploreAttributeSearchingForKey(method);

                if (key != null)
                {
                    string res = (string)method.Invoke(instanceManager.GetInstance(type), null);

                    Console.WriteLine($"{method.Name} [{res}]\n");

                    SavePartOfKey((int)key, res);
                }
            }
        }

        private void ExploreFields(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                int? key = ExploreAttributeSearchingForKey(field);

                string res = (string)field.GetValue(instanceManager.GetInstance(type));
                Console.WriteLine($"{field.Name} [{res}]\n");
                if (key != null)
                    SavePartOfKey((int)key, res);
            }
        }

        private int? ExploreAttributeSearchingForKey(MemberInfo member)
        {
            PasswordIsHereAttribute attribute = member.GetCustomAttribute<PasswordIsHereAttribute>();
            if (attribute == null)
                return null;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[PasswordIsHere(ChunkNo = {attribute.ChunkNo})]");;
            Console.ForegroundColor = ConsoleColor.White;
            return attribute.ChunkNo;
        }

        private bool ExploreAttributeIfTypeNeedToExplore(MemberInfo member)
        {
            IgnoreMeAttribute attribute = member.GetCustomAttribute<IgnoreMeAttribute>();
            if (attribute == null)
                return true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[IgnoreMeAttribute]");
            Console.ForegroundColor = ConsoleColor.White;
            return false;
        }

        private void SavePartOfKey(int index, string key)
        {
            keyValuePairs.Add(index, key);
        }  
    }
}
