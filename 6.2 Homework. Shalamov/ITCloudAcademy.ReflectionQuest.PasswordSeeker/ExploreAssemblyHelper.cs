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
        private SortedDictionary<int, string> keyValuePairs;
        private string tempKey;
        private int tempIndex;

        public string Password{ get; private set; }

        public ExploreAssemblyHelper()
        {
            keyValuePairs = new SortedDictionary<int, string>();
            tempKey = null;
            tempIndex = -1;
        }


        public void ExploreAssembly(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            Console.WriteLine(assembly.FullName);

            foreach (Type t in assembly.GetTypes())
                ExploreType(t);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in keyValuePairs)
            {
                stringBuilder.Append(item.Value);
            }
            Password = stringBuilder.ToString();
        }

        private void ExploreType(Type type)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Environment.NewLine + new string('-', 20));

            object instance = Activator.CreateInstance(type);
            ExploreAttributes(type.GetCustomAttributes(), out bool needToExplore, out bool keyWasFound);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(type.Name + Environment.NewLine);

            if (needToExplore == false)
                return;

            ExploreProperties(instance);
            ExploreFields(instance);
            ExploreMethods(instance);
        }

        private void ExploreProperties(object instance)
        {
            PropertyInfo[] properties = instance.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                ExploreAttributes(property.GetCustomAttributes(), out bool needToExplore, out bool keyWasFound);

                object res = property.GetValue(instance, null);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{property.Name} [{res}]\n");
                if (keyWasFound)
                    SavePartOfKey(key: (string)res);
            }
        }

        private void ExploreMethods(object instance)
        {
            MethodInfo[] methods = instance.GetType().GetMethods();

            foreach (MethodInfo method in methods)
            {
                ExploreAttributes(method.GetCustomAttributes(), out bool needToExplore, out bool keyWasFound);

                object res = null;
                if (keyWasFound)
                {
                    res = method.Invoke(instance, null);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{method.Name} [{res}]\n");
                    if (keyWasFound)
                        SavePartOfKey(key: (string)res);
                }
            }
        }

        

        private void ExploreFields(object instance)
        {
            FieldInfo[] fields = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                ExploreAttributes(field.GetCustomAttributes(), out bool needToExplore, out bool keyWasFound);

                var res = field.GetValue(instance);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{field.Name} [{res}]\n");
                if (keyWasFound)
                    SavePartOfKey(key: (string)res);

            }
        }


        private void ExploreAttributes(IEnumerable<Attribute> attributes, out bool needToExplore, out bool keyWasFound)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            keyWasFound = false;
            needToExplore = false;
            if (attributes.Count() == 0)
            {
                needToExplore = true;
                return;
            }

            PasswordIsHereAttribute attributeWithPassword = attributes.First() as PasswordIsHereAttribute;
            if (attributeWithPassword != null)
            {
                Console.WriteLine($"[PasswordIsHere(ChunkNo = {attributeWithPassword.ChunkNo})]");
                SavePartOfKey(index: attributeWithPassword.ChunkNo);
                needToExplore = true;
                keyWasFound = true;
                return;
            }
            IgnoreMeAttribute attributeIgnoreMe = attributes.First() as IgnoreMeAttribute;

            if (attributeIgnoreMe != null)
                Console.WriteLine($"[IgnoreMeAttribute]");
        }


        private void SavePartOfKey(int index = -1, string key = null)
        {
            if (index != -1)
                tempIndex = index;
            if (key != null)
                tempKey = key;

            if (tempIndex != -1 && tempKey != null)
            {
                keyValuePairs.Add(tempIndex, tempKey);
                tempIndex = -1;
                tempKey = null;
            }
        }
    }
}
