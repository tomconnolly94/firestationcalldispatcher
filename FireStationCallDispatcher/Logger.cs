using System;

namespace FireStationCallDispatcher
{
    public static class Logger
    {
        public static void InfoLog(string message)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            Console.WriteLine($"{timeStamp} - Info: {message}");
        }
        public static void ErrorLog(string message)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            Console.WriteLine($"{timeStamp} - ERROR: {message}");
        }
    }

}
