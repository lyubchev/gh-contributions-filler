using System;
using LibGit2Sharp;

namespace gh_contributions_filler
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = "";
            Console.BufferHeight = 30;
            Console.WriteLine(@"
   _____   _   _     _    _           _     
  / ____| (_) | |   | |  | |         | |    
 | |  __   _  | |_  | |__| |  _   _  | |__  
 | | |_ | | | | __| |  __  | | | | | | '_ \ 
 | |__| | | | | |_  | |  | | | |_| | | |_) |
  \_____| |_|  \__| |_|  |_|  \__,_| |_.__/ 

 CALENDAR FILLER V1.0
");         
            Console.WriteLine(@"Type ""help"" for commands" + Environment.NewLine);
            userInput = Console.ReadLine();
            if(userInput == "help")
            {
                Console.WriteLine(@" fill help - Gives information about starting a fill session");
                Console.WriteLine(@" run fill - Starts new session");
                Console.WriteLine(@" end fill - Ends current session");
                Console.WriteLine(@" clear - Clears the console");
                Console.WriteLine(@" about - Gives information about the project");
                Console.WriteLine(@" about auth - Gives information about the author");
            }
        }
    }
}
