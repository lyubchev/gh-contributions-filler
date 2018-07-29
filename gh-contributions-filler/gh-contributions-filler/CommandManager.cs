using System;
using System.Collections.Generic;
using System.Text;

namespace gh_contributions_filler
{
    class CommandManager
    {
        public CommandManager(string command)
        {
            switch (command)
            {
                case "fill help":
                    Console.WriteLine(@"To start a new ""contribution filling"" session use ""run fill""");
                    Console.WriteLine(@"After starting a new session start typing the dates which you are willing to fill in the GitHub calendar.");
                    Console.WriteLine(@"Press enter after each date!");
                    Console.WriteLine(@"Valid date formats: MM/DD/YYYY, MM-DD-YYYY, MM.DD.YYYY, MM DD YYYY");
                    Console.WriteLine(@"After you are done typing the dates, use end fill to end the current session");
                    break;
                case "run fill":
                    Console.WriteLine(@"Enter dates...");
                    break;
                case "end fill":
                    Console.WriteLine(@"Ending session...");
                    break;
                case "clear":
                    Console.Clear();
                    Console.WriteLine(@"
   _____   _   _     _    _           _     
  / ____| (_) | |   | |  | |         | |    
 | |  __   _  | |_  | |__| |  _   _  | |__  
 | | |_ | | | | __| |  __  | | | | | | '_ \ 
 | |__| | | | | |_  | |  | | | |_| | | |_) |
  \_____| |_|  \__| |_|  |_|  \__,_| |_.__/ 

 Contributions FILLER V1.0
");
                    Console.WriteLine(@"Type ""help"" for commands" + Environment.NewLine);
                    break;
                case "about":
                    Console.WriteLine("GitHub Contributions Filler v1.0");
                    Console.WriteLine("Repo URL: https://github.com/IMPZERO/gh-contributions-filler");
                    break;
                case "about auth":
                    Console.WriteLine("Name: Lyubo Lyubchev");
                    Console.WriteLine("Website: soon");
                    Console.WriteLine("GitHub: https://github.com/IMPZERO");
                    break;
                default:
                    Console.WriteLine(@"Invalid command, use ""help"" for valid commands");
                    break;
            }
        }
    }
}
