using System;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class InstanceManager
    {
        private object instance;

        public object GetInstance(Type type)
        {
            if (instance == null || instance.GetType() != type)
                instance = Activator.CreateInstance(type);

            return instance;
        }
    }
}
