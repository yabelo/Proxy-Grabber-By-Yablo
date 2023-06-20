using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp4 {

    internal class Program {

        static void Main(string[] args) {

            WelcomeMessage();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[I]nput File");

            while (true) {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                char answer = char.ToUpper(keyInfo.KeyChar);

                if (answer == 'I') {
                    InputFile();
                    return;
                }
            }
        }

        public static void WelcomeMessage() {
            Console.Title = "Proxy Grabber By Yablo";

            string asciiArt = @"  _____                        _____           _     _               
 |  __ \                      / ____|         | |   | |              
 | |__) | __ _____  ___   _  | |  __ _ __ __ _| |__ | |__   ___ _ __ 
 |  ___/ '__/ _ \ \/ / | | | | | |_ | '__/ _` | '_ \| '_ \ / _ \ '__|
 | |   | | | (_) >  <| |_| | | |__| | | | (_| | |_) | |_) |  __/ |   
 |_|   |_|  \___/_/\_\\__, |  \_____|_|  \__,_|_.__/|_.__/ \___|_|   
                       __/ |                                         
  ____         __     |___/_     _                                   
 |  _ \        \ \   / /  | |   | |                                  
 | |_) |_   _   \ \_/ /_ _| |__ | | ___                              
 |  _ <| | | |   \   / _` | '_ \| |/ _ \                             
 | |_) | |_| |    | | (_| | |_) | | (_) |                            
 |____/ \__, |    |_|\__,_|_.__/|_|\___/                             
         __/ |                                                       
        |___/                                                         ";

            Console.WriteLine(asciiArt);

            Thread.Sleep(1000);
            Console.WriteLine("\n\n\n\n\n");
            Console.Write("Loading: ");

            Thread.Sleep(1000);

            for (int i = 0; i < 5; i++) {

                switch (i) {
                    case 0:
                        Console.Write("Y");
                        break;
                    case 1:
                        Console.Write("a");
                        break;
                    case 2:
                        Console.Write("b");
                        break;
                    case 3:
                        Console.Write("l");
                        break;
                    case 4:
                        Console.Write("o");
                        break;

                }
                Thread.Sleep(500);
            }
            Console.Clear();

        }


        public static void InputFile() {

            Console.WriteLine("Enter the input file path:");
            string input = Console.ReadLine();
            input = input.Trim('"');
            string inputFilePath = input.Replace("\\", "\\\\");

            string[] inputLines = File.ReadAllLines(inputFilePath);

            MakeFunction(inputLines);
        }

        public static void MakeFunction(string[] inputLines) {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "scrapedProxy.txt";
            string filePath = Path.Combine(desktopPath, fileName);

            if (File.Exists(filePath)) { File.Delete(filePath); }

            try {
                List<string> modifiedLines = new List<string>();

                foreach (string line in inputLines) {
                    string ip = GetIp(line);
                    if (!string.IsNullOrEmpty(ip) && ip.Length > 9) {
                        modifiedLines.Add(ip);

                        using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8)) {
                            writer.WriteLine(ip);
                        }
                    }
                }

                Console.WriteLine("Saved into file named scrapedProxy.txt");
            } catch (Exception ex) {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }

        public static string GetIp(string longLine) {
            if (longLine == "" || longLine == null) {
                return null;
            }

            string ip = "";
            char[] chars = longLine.ToCharArray();

            if (!chars.Contains('.')) {
                return null;
            }

            for (int i = 0; i < chars.Length; i++) {
                if (chars[i] == ' ' || chars[i] == '\t') {
                    while (i + 1 < chars.Length && (chars[i + 1] == ' ' || chars[i + 1] == '\t'))
                        i++;

                    ip += ':';
                    int j = i + 1;
                    while (j < chars.Length && chars[j] != ' ' && chars[j] != '\t') {
                        if (Char.IsDigit(chars[j]) || chars[j] == '.')
                            ip += chars[j];
                        j++;
                    }
                    break;
                } else if (chars[i] == ':') {
                    while (i < chars.Length && chars[i] != ' ' && chars[i] != '\t') {
                        if (Char.IsDigit(chars[i]) || chars[i] == '.' || chars[i] == ':')
                            ip += chars[i];
                        i++;
                    }
                    break;
                } else if (Char.IsDigit(chars[i]) || chars[i] == '.') {
                    ip += chars[i];
                }
            }

            return ip;
        }


    }
}
