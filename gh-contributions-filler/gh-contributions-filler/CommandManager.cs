using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace gh_contributions_filler
{
    class CommandManager
    {
        public CommandManager(string command)
        {
            List<DateTime> dates = new List<DateTime>();
            switch (command)
            {
                case "help":
                    Console.WriteLine(@" fill help - Gives information about starting a fill session");
                    Console.WriteLine(@" run fill - Starts new session");
                    Console.WriteLine(@" end fill - Ends current session");
                    Console.WriteLine(@" clear - Clears the console");
                    Console.WriteLine(@" about - Gives information about the project");
                    Console.WriteLine(@" about auth - Gives information about the author");
                    break;
                case "fill help":
                    Console.WriteLine(@"To start a new ""contribution filling"" session use ""run fill""");
                    Console.WriteLine(@"After starting a new session start typing the dates which you are willing to fill in the GitHub calendar.");
                    Console.WriteLine(@"Press enter after each date!");
                    Console.WriteLine(@"Valid date formats: MM/DD/YYYY");
                    Console.WriteLine(@"After you are done typing the dates, use end fill to end the current session");
                    break;
                case "run fill":
                    Console.WriteLine(@"Enter dates...");
                    DatesInsertion();
                    break;
                case "clear":
                    RestartConsole();
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
            void DatesInsertion() 
            {
                while ((command = Console.ReadLine()) != "end fill")
                {
                    DateTime date = DateTime.Parse(command + " 18:00:00");
                    dates.Add(date);
                }
                Console.WriteLine(@"Ending session...");
                CommitDates();
                RestartConsole();
            }
            void CommitDates()
            {
                string folderName = Util.RandomString(10);
                Directory.CreateDirectory(folderName);

                Repository.Init(folderName);
                using (var repo = new Repository(folderName))
                {
                    foreach (var date in dates)
                    {
                        string content = $"I ❤ GitHub! {date.ToString()}";
                        File.AppendAllText(Path.Combine(repo.Info.WorkingDirectory, "README.md"), content);
                        repo.Index.Add("README.md");
                        Commands.Stage(repo, "*");
                        Signature author = new Signature("IMPZERO", "lyubo_2317@abv.bg", date);
                        Signature committer = author;

                        Commit commit = repo.Commit("Dummit!", author, committer);

                    }
                }
            }
            void RestartConsole()
            {
                Console.Clear();
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
            }
        }
    }
}
