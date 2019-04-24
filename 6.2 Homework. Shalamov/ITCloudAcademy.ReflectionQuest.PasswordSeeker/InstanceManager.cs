using System;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class InstanceManager
    {
        private object instance;
        public int Count { get; set; }

        public object GetInstance(Type type)
        {
            Count++;
            if (instance == null || instance.GetType() != type)
                instance = Activator.CreateInstance(type);
            
            return instance;
        }
    }
}
