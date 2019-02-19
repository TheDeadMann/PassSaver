﻿using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MirzaCryptoHelpers.Extensions;

namespace PassSaver
{
    class Program
    {
        // Made By TheDeadMan

        private static Random rnd = new Random();

        private static string PrintArrString(string[] a)
        {
            string endResult = "";
            for (int i = 0; i < a.Length; i++)
            {
                endResult += a[i];
            }

            return endResult;
        }

        private static void Intro()
        {
            Console.Title = string.Format("PassSaver | {0}", Variables.Version);
            Console.WriteLine(Variables.Logo);
            Console.WriteLine("\nMade by TheDeadMan");
            Thread.Sleep(2500);
            Console.Clear();
        }

        private static void Error()
        {
            Console.Clear();
            Console.WriteLine(Variables.error);
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Please use the program correctly.");
            Thread.Sleep(750);
            Console.Clear();
            Console.WriteLine("Please use the program correctly..");
            Thread.Sleep(750);
            Console.Clear();
            Console.WriteLine("Please use the program correctly...");
            Thread.Sleep(750);
            Console.Clear();
            Program.Welcome();
        }

        private static void Done()
        {
            Console.Clear();
            Console.WriteLine(Variables.done);
            Thread.Sleep(1500);

            Program.Welcome();
        }

        private static void Welcome()
        {
            Console.Clear();
            Console.WriteLine("1) Make a new password");
            Console.WriteLine("2) Get old passwords");
            Console.WriteLine("3) Exit");

            Program.Choice();
        }

        private static void Choice()
        {
            var choice = Console.ReadKey();

            if (choice.Key == ConsoleKey.NumPad1 || choice.Key == ConsoleKey.D1)
            {
                Program.MakePassword();
            }

            else if (choice.Key == ConsoleKey.NumPad2 || choice.Key == ConsoleKey.D2)
            {
                Program.ShowPassword();
            }

            else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D3)
            {
                Environment.Exit(0);
            }

            else
            {
                Program.Error();
            }
        }

        private static void ShowPassword()
        {
            Console.Clear();
            if (Variables.fileArray.Length == 0)
            {
                Console.WriteLine("You dont have any passwords.");
                Thread.Sleep(750);
                Console.Clear();
                Console.WriteLine("You dont have any passwords..");
                Thread.Sleep(750);
                Console.Clear();
                Console.WriteLine("You dont have any passwords...");
                Thread.Sleep(750);
                Console.Clear();

                Program.Welcome();
            }

            else
            {
                Console.WriteLine("What password would you want to see?\n");
                Console.WriteLine("0) Back To Main Menu");

                for (int i = 0; i < Variables.fileArray.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {Path.GetFileNameWithoutExtension(Variables.fileArray[i])}");
                }

                int passwordChoiceInt = 0;
                var passwordChoice = Console.ReadKey();
                Console.Clear();

                if (char.IsDigit(passwordChoice.KeyChar) && passwordChoice.Key != ConsoleKey.D0 && passwordChoice.Key != ConsoleKey.NumPad0)
                {
                    passwordChoiceInt = int.Parse(passwordChoice.KeyChar.ToString());
                }

                else if (passwordChoice.Key == ConsoleKey.D0 || passwordChoice.Key == ConsoleKey.NumPad0)
                {
                    Program.Welcome();
                }

                else
                {
                    Program.Error();
                }

                var passwordToShow = File.ReadAllText($@"..\..\PS\{Path.GetFileNameWithoutExtension(Variables.fileArray[passwordChoiceInt - 1])}.txt");
                var decryptedPassword = passwordToShow.FromBinary();
                Console.WriteLine(decryptedPassword);

                Console.WriteLine("\n1) Copy to clipboard");
                Console.WriteLine("2) Delete the password");
                Console.WriteLine("3) Main Menu");
                var fileChoice = Console.ReadKey();

                if (fileChoice.Key == ConsoleKey.D1 || fileChoice.Key == ConsoleKey.NumPad1)
                {
                    Thread thread = new Thread(() => Clipboard.SetText(decryptedPassword));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    Program.Done();
                }

                else if (fileChoice.Key == ConsoleKey.D2 || fileChoice.Key == ConsoleKey.NumPad2)
                {
                    File.Delete($@"..\..\PS\{Path.GetFileNameWithoutExtension(Variables.fileArray[passwordChoiceInt - 1])}.txt");
                    Program.Done();
                }

                else if (fileChoice.Key == ConsoleKey.D3 || fileChoice.Key == ConsoleKey.NumPad3)
                    Program.Welcome();

                else
                    Program.Error();
            }

        }

        private static void MakePassword()
        {
            Console.Clear();

            Console.WriteLine("How long do you want your password to be?\n(REMEMBER: the longer your password, the safer it is to use)");

            int length = int.Parse(Console.ReadLine());
            string[] Password = new string[length];

            for (int i = 0; i < length; i++)
            {
                int z = rnd.Next(Variables.chars.Length);
                string x = Variables.chars[z].ToString();

                Password[i] = x;
            }

            Console.Clear();
            Console.Write($"Your password is: {PrintArrString(Password)}");
            Console.WriteLine("\nDo you want to use this password? (y/n)");
            var yesno = Console.ReadKey();

            if (yesno.Key == ConsoleKey.Y)
            {
                Console.Clear();
                Console.WriteLine("What are you using the password for?");
                string title = Console.ReadLine();
                
                var encryptedPassword = PrintArrString(Password).ToBinary();
                File.WriteAllText($@"..\..\PS\{title.ToLower()}.txt", encryptedPassword);

                Program.Done();
            }

            else if (yesno.Key == ConsoleKey.N) Program.Welcome();

            else Program.Error();
        }

        static void Main(string[] args)
        {
            Program.Intro();
            Program.Welcome();
        }
    }
}
