using System;
using System.Collections.Generic;
using System.IO;

namespace DavidStrömVGUppgift2
{
    class Logic
    {
        static bool exit = false;

        //Detta är en metod som hanterar inloggningen till programmet.
        public static void Login()
        {
            Console.Title = "Bästkusten";
            int ctr = 0;
            do
            {
                //Här körs en loop där varje tangenttryck registreras och läggs
                //till i strängen password, men det som syns är bara asterisker.
                string userInput = "";
                Console.WriteLine(@"         ____  _   _     _   _              _");
                Console.WriteLine(@"        |  _ \(_) (_)   | | | |            | |");
                Console.WriteLine(@"        | |_) | __ _ ___| |_| | ___   _ ___| |_ ___ _ __");
                Console.WriteLine(@"        |  _ < / _` / __| __| |/ / | | / __| __/ _ \ '_ \");
                Console.WriteLine(@"        | |_) | (_| \__ \ |_|   <| |_| \__ \ ||  __/ | | |");
                Console.WriteLine(@"        |____/ \__,_|___/\__|_|\_\\__,_|___/\__\___|_| |_|");
                Console.WriteLine("\n\tVälkommen till Bästkusten!");
                Console.Write("\tAnge lösenordet: ");
                ConsoleKey key;
                do
                {
                    //Varje knapptryck sparas i keyInfo men syns inte på skärmen.
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    key = keyInfo.Key;
                    if (key == ConsoleKey.Backspace && userInput.Length > 0)
                    {
                        //Om man trycker Backspace raderas asterisken från skärmen
                        //och tecknet som tidigare sparats i password tas bort.
                        Console.Write("\b \b");
                        userInput = userInput[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        //Här skrivs en asterisk ut till skärmen och knapptrycket
                        //sparas till password.
                        Console.Write("*");
                        userInput += keyInfo.KeyChar;
                    }
                    //loopen körs så länge man inte trycker på Enter.
                } while (key != ConsoleKey.Enter);

                string password = "Bästkusten";
                if (userInput == password)
                {
                    Print.TextInGreen("\n\tKorrekt! Du angav rätt kod!");
                    Run();
                }
                else if (userInput == "")
                {
                    Print.TextInGreen("Tips, lösenordet är basgruppens namn");
                    Console.Clear();
                }
                else
                {
                    ctr++;
                    if (ctr > 2)
                    {
                        Print.TextInRed("\n\tDu har matat in fel lösenord för många gånger.\n");
                        ExitProgram();
                    }
                    else
                        Print.TextInRed("\n\tFel kod! Försök igen...");
                    Console.Clear();
                }
            } while (!exit);
        }

        //Här är en metod som styr hela programmet när man väl loggat in.
        //Metoden körs tills användaren väljer att avsluta.
        static void Run()
        {
            do
            {
                List<Member> members = CRUD.ReadFromTextFile();
                Print.MainMenu();
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        Print.ShowMembers(members);
                        break;
                    case 2:
                        Print.MemberDetails(members);
                        break;
                    case 3:
                        CRUD.CreateNewMember(members);
                        break;
                    case 4:
                        CRUD.EditMember(members);
                        break;
                    case 5:
                        CRUD.DeleteMember(members);
                        break;
                    case 6:
                        ExitProgram();
                        break;
                    default:
                        Print.TextInRed("\tDu måste vålja mellan 1-4...");
                        break;
                }
            } while (!exit);
        }

        //Här är en metod som avslutar programmet.
        static void ExitProgram()
        {
            //Här sätts exit till true och ett avslutande meddelande skrivs ut
            //med lite visuell effekt som får det att se ut som att datorn tänker.
            exit = true;
            Print.TextInGreen("\tProgrammet avslutas");
            for (int i = 0; i < 3; i++)
            {
                Print.TextInGreen(".");
            }
        }
    }
}
