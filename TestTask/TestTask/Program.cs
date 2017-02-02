using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;

namespace TestTask
{
    class Program
    {
        static TwitterApp app { get; set; }

        public static void MessageToApp(string message) { app?.MessageToApp(message); }

        static void Main(string[] args)
        {
            app = new TwitterApp();

            app.Run();

            app = null;
        }
    }
}
