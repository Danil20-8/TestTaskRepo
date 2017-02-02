using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class UserInput : IEnumerable<string>
    {
        string startEnterMessage;

        public UserInput(string startEnterMessage = "Enter text:")
        {
            this.startEnterMessage = startEnterMessage;
        }

        public IEnumerator<string> GetEnumerator()
        {
            string input;
            for(;;)
            {
                Program.MessageToApp(startEnterMessage);
                Console.Write(">");
                if ((input = Console.ReadLine()) == null)
                    break;

                if (input.Length == 0)
                {
                    Program.MessageToApp("Please enter at least 1 symbol.");
                    continue;
                }

                yield return input;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
