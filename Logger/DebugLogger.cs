using System;

namespace UrsaEngine.Logging 
{
    public static class DebugLogger
    {
        public static void Log(string text)
        {
            string time = DateTime.Now.ToLongTimeString();
            Console.WriteLine(time + " " + text);
        }
        public static void LogArray(Array arr)
        {
            string str = "{ ";
            foreach(var a in arr)
            {
                str += a + ", ";
            }
            str += "}";
            Console.WriteLine(str);
        }
    }
}