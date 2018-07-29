using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        public static void RestartConsole()
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
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"gh-contributions-filler");
            return !Directory.Exists(path) ? "" : File.ReadAllText(Path.Combine(path, @"\config.txt"));
        }
    }
}
