using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Configuration;
using System.Reflection;

namespace TestTask
{
    class TwitterApp
    {
        const int twits_amount = 5;

        TwitterContext twitterCtx;

        XAuthAuthorizer auth;

        public TwitterApp()
        {
            var appSettings = ConfigurationManager.AppSettings;

            XAuthCredentials credentials = new XAuthCredentials();
            // retrieving data from App.Config
            foreach (var p in typeof(XAuthCredentials).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                p.SetValue(credentials, appSettings[p.Name]);

            auth = new XAuthAuthorizer
            {
                CredentialStore = credentials
            };
        }


        bool running = false;
        public void Run()
        {
            if (running)
                throw new Exception("TwitterApp is already running!");
            running = true;
            using (twitterCtx = new TwitterContext(auth))
            {
                var userInput = new UserInput("Введите Twitter логин:");
                for (;;)
                {
                    try
                    {
                        Run(userInput);
                        break;
                    }
                    catch (Exception e)
                    {
                        MessageToApp(e.Message);
                    }
                }
            }
            running = false;
        }

        void Run(UserInput userInput)
        {
            foreach (var userLogin in userInput)
            {
                string twits = GetTwits(userLogin, twits_amount);

                IEnumerable<char> chars;
                int amount = GetFrequntChars(twits, out chars);

                string message;
                if (amount > 0)
                {
                    if (chars.Count() > 1)
                    {
                        message = String.Format("{0} чаще всего пользуется буквами {1}: {2} раз за {3} твитов!", userLogin, chars.ToString("\"{0}\"", ", "), amount, twits_amount);
                    }
                    else
                    {
                        message = String.Format("{0} чаще всего пользуется буквой \"{1}\": {2} раз за {3} твитов!", userLogin, chars.First(), amount, twits_amount);
                    }
                }
                else
                {
                    message = String.Format("{0} ни разу не твитал!", userLogin);
                }
                SendMessage(message);
            }
        }

        string GetTwits(string userLogin, int amount)
        {
            return String.Join("", twitterCtx.Status.Where(s => s.Type == StatusType.User && s.ScreenName == userLogin && s.Count == amount).Select(s => s.Text));
        }

        void SendMessage(string message)
        {
            MessageToApp(message);
            MessageToTwitter(message);
        }

        static int GetFrequntChars(string text, out IEnumerable<char> chars)
        {
            if (text == null || text.Length == 0)
            {
                chars = new char[0];
                return 0;
            }

            var symbols = text
                .Where(c => c != ' ') // ' ' is not symbol. Right?
                .GroupBy(c => c)
                .Select(g => new Tuple<char, int>(g.Key, g.Count()))
                .WithMax(t => t.Item2);
            chars = symbols.Select(t => t.Item1);
            return symbols.First().Item2;
        }

        public void MessageToApp(string message)
        {
            Console.WriteLine(message);
        }
        void MessageToTwitter(string message)
        {
            twitterCtx.TweetAsync(message);
        }
    }
}
