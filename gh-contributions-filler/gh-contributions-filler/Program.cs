using System;

namespace gh_contributions_filler
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = "";
            CommandManager cm;
            Console.BufferHeight = 30;
            Console.WriteLine(@"
   _____   _   _     _    _           _     
  / ____| (_) | |   | |  | |         | |    
 | |  __   _  | |_  | |__| |  _   _  | |__  
 | | |_ | | | | __| |  __  | | | | | | '_ \ 
 | |__| | | | | |_  | |  | | | |_| | | |_) |
  \_____| |_|  \__| |_|  |_|  \__,_| |_.__/ 

 CONTRIBUTIONS FILLER V1.0
");         
            Console.WriteLine(@"Type ""help"" for commands" + Environment.NewLine);
            while (true)
            {
                userInput = Console.ReadLine();
                cm = new CommandManager(userInput);
            }
        }
    }
}
