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
            // 
            // -p triangle 

            List<string> patterns = new List<string>(){ "square", "rectangle", "triangle", "circle", "diagonal", "line", "random" };

            switch (command)
            {
                case "help":
                    Console.WriteLine(@"    1. fill help - Gives information about starting a fill session");
                    Console.WriteLine(@"    2. run fill - Starts new session");
                    Console.WriteLine(@"    3. finish fill - Ends current session");
                    Console.WriteLine(@"    4. clear - Clears the console");
                    Console.WriteLine(@"    5. reset - Resets local user settings");
                    Console.WriteLine(@"    6. about - Gives information about the project");
                    Console.WriteLine(@"    7. about auth - Gives information about the author");
                    break;
                case "fill help":
                    Console.WriteLine(@"    To start a new ""contribution filling"" session use ""run fill""");
                    Console.WriteLine(@"    After starting a new session start typing the dates which you are willing to fill in the GitHub calendar.");
                    Console.WriteLine(@"    Press enter after each date!");
                    Console.WriteLine(@"    Valid date formats: MM/DD/YYYY");
                    Console.WriteLine(@"    After you are done typing the dates, use ""finish fill"" to end the current session");
                    break;
                case "run fill":
                    Console.WriteLine(@"Starting session...");
                    Util.RestartConsole(out username, out email);
                    Console.WriteLine(@"Enter dates...");
                    DatesInsertion();
                    break;
                case "clear":
                    Util.RestartConsole(out username, out email);
                    break;
                case "reset":
                    Console.WriteLine("Resetting...");
                    System.Threading.Thread.Sleep(1000);
                    Util.ResetUserSettings();
                    Console.WriteLine("Restarting Console...");
                    System.Threading.Thread.Sleep(1000);
                    Util.RestartConsole(out username, out email);
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
                    if (command == "") break;
                    if (DateTime.TryParse(command + " 18:00:00", out date))
                    {
                        dates.Add(date);
                    }
                    else
                    {
                        if (command.Split('/').Length == 4) // IF THE INPUT DATE CONTAINS FOUR SLASHES "/" : EXAMPLE 07/03/2018/7 THE NUMBER AFTER THE YEAR INDICATES HOW MANY DAYS AFTER THIS DATE SHOULD BE FILLED UP
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
                        //else if (command.Contains(patterns.Select(x => x))
                        //{
                        //    string pattern = command.Substring(command.LastIndexOf('/'));
                        //    command = command.Substring(command.LastIndexOf('/'));
                        //    DateTime.TryParse(command, out date);
                        //    Util.ProcessPattern(pattern, date, ref dates);
                        //}
                    }
                }
                Console.Write("Contributions per day: ");
                int amount = int.Parse(Console.ReadLine());
                Commit(amount);
                Console.WriteLine(@"Ending session...");
                Util.RestartConsole(out username, out email);
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
