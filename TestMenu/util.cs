using System;

namespace TestMenu
{
    class Util
    {
        public static int GetConsoleNumberInput()
        {
            try
            {
                string input = Console.ReadLine();
                int parsedInput;
                int.TryParse(input, out parsedInput);
                return parsedInput;
            }
            catch(Exception e)
            {
                if (Program.DEBUG == true)
                    Console.WriteLine(e.ToString());
                return -1;
            }
        }

        public static float GetConsoleFloatInput()
        {
            try
            {
                string input = Console.ReadLine();
                float parsedInput;
                float.TryParse(input, out parsedInput);
                return parsedInput;
            }
            catch(Exception e)
            {
                if (Program.DEBUG == true)
                    Console.WriteLine(e.ToString());
                return -1;
            }
        }

        public static void PauseConsole()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
