using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace gh_contributions_filler
{
    class CommandManager
    {
        public CommandManager(string command, string username, string email)
        {
            List<DateTime> dates = new List<DateTime>();
            switch (command)
            {
                case "help":
                    Console.WriteLine(@" fill help - Gives information about starting a fill session");
                    Console.WriteLine(@" run fill - Starts new session");
                    Console.WriteLine(@" finish fill - Ends current session");
                    Console.WriteLine(@" clear - Clears the console");
                    Console.WriteLine(@" about - Gives information about the project");
                    Console.WriteLine(@" about auth - Gives information about the author");
                    break;
                case "fill help":
                    Console.WriteLine(@"To start a new ""contribution filling"" session use ""run fill""");
                    Console.WriteLine(@"After starting a new session start typing the dates which you are willing to fill in the GitHub calendar.");
                    Console.WriteLine(@"Press enter after each date!");
                    Console.WriteLine(@"Valid date formats: MM/DD/YYYY");
                    Console.WriteLine(@"After you are done typing the dates, use ""finish fill"" to end the current session");
                    break;
                case "run fill":
                    Console.WriteLine(@"Starting session...");
                    Util.RestartConsole();
                    Console.WriteLine(@"Enter dates...");
                    DatesInsertion();
                    break;
                case "clear":
                    Util.RestartConsole();
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
                    Console.WriteLine(@"Invalid command, use ""help"" to see valid commands");
                    break;
            }
            void DatesInsertion()
            {
                while ((command = Console.ReadLine()) != "finish fill")
                {
                    DateTime date;
                    command = command.Trim().Replace(' ', '/');
                    if (DateTime.TryParse(command + " 18:00:00", out date))
                    {
                        dates.Add(date);
                    }
                    else
                    {
                        if (command.Split('/').Length == 4) // IF THE INPUT DATE CONTAINS FOR SLASHES "/" : EXAMPLE 07/03/2018/7 THE NUMBER AFTER THE YEAR INDICATES HOW MANY DAYS AFTER THIS DATE SHOULD BE FILLED UP
                        {
                            if (DateTime.TryParse(command.Substring(0, command.LastIndexOf('/')), out date)) // GETS THE NORMAL DATE
                            {
                                for (int i = 0; i < int.Parse(command.Split('/')[3]); i++)
                                {
                                    dates.Add(date.AddDays(i));
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid date!");
                                return;
                            }
                        }
                    }
                }
                Console.Write("Contributions per day: ");
                int amount = int.Parse(Console.ReadLine());
                Commit(amount);
                Console.WriteLine(@"Ending session...");
                Util.RestartConsole();
            }
            void Commit(int commitsPerDay)
            {
                string folderName = Util.RandomString(10);
                Directory.CreateDirectory(folderName);

                Repository.Init(folderName);
                using (var repo = new Repository(folderName))
                {
                    foreach (var date in dates)
                    {
                        for (int i = 0; i < commitsPerDay; i++)
                        {
                            string content = $"I ❤ GitHub! {date.ToString()}{Environment.NewLine}";
                            File.AppendAllText(Path.Combine(repo.Info.WorkingDirectory, "README.md"), content);
                            repo.Index.Add("README.md");
                            Commands.Stage(repo, "*");
                            Signature author = new Signature(username, email, date);
                            Signature committer = author;

                            Commit commit = repo.Commit("Dummit!", author, committer);
                        }
                    }
                }
            }
        }
    }
}
