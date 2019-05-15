namespace MyLibrary
{

    public static class LibraryHelper
    {
        public static void ShowLibraryInfo()
        {
            System.Console.WriteLine($"Current library: {GetAssemblyName()}");
        }

        public static void ShowLibraryInfo_Debug()
        {
            System.Console.WriteLine($"(this for DEBUG) Current library: {GetAssemblyName()}");
        }

        private static string GetAssemblyName() => System.Reflection.Assembly.GetExecutingAssembly().GetName().ToString();
    }
}
