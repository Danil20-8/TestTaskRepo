using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class UserInput : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            string input;
            while ((input = Console.ReadLine()) != null)
            {
                if(input.Length == 0)
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
