using System;
using System.IO;
using System.Linq;

namespace gh_contributions_filler
{
    class Util
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static void RestartConsole(out string username, out string email)
        {
            

            if (Util.LoadUserSettings() == "")
            {
                Console.WriteLine(@"Configure your account settings");
                Console.Write(@"Username: ");
                username = Console.ReadLine();
                Console.Write(@"Email: ");
                email = Console.ReadLine();
                Console.WriteLine(@"Configuring settings...");
                Util.SaveUserSettings(username, email);
            }
            else
            {
                username = Util.LoadUserSettings().Split(Environment.NewLine)[0];
                email = Util.LoadUserSettings().Split(Environment.NewLine)[1];
            }
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

        public static void SaveUserSettings(string username, string email)
        {
            string path = @"gh-contributions-filler";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string content = username + Environment.NewLine + email;
            File.WriteAllText(Path.Combine(path, @"config.txt"), content);
        }

        public static string LoadUserSettings()
        {
            string path = @"gh-contributions-filler";
            return !Directory.Exists(path) ? "" : File.ReadAllText(Path.Combine(path, @"config.txt"));
        }

        public static void ResetUserSettings()
        {
            string path = @"gh-contributions-filler";
            File.WriteAllText(Path.Combine(path, @"config.txt"), null);
        }
    }
}
