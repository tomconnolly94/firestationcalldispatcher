using System;
using System.Collections.Generic;
using System.Text;

namespace FireStationCallDispatcher
{
    public static class Logger
    {
        public static void InfoLog(string message)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            Console.WriteLine($"{timeStamp} - Info: {message}");
        }
    }

}
