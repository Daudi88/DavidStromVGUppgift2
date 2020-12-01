using System;
using System.Collections.Generic;
using System.Threading;

namespace DavidStrömVGUppgift2
{
    class Program
    {
        static List<Member> group = new List<Member>();
        static int time = 2000;
        static void Main(string[] args)
        {
            Setup();
        }

        static void Setup()
        {
            PopulateGroup();
            Login();
            Menu();
            Run();
        }

        static void PopulateGroup()
        {
            AddMember("Elin", 170, 31, "hästar", "sushi", "röd", "personliga utveckling", "Knivsta", "Karlskoga", 2);
            AddMember("Cecilia", 163, 29, "The Sims", "risotto", "gul", "kreativitet", "Norrköping", "Norrköping", 1);
            AddMember("Jeremy", 181, 19, "gaming", "älggryta", "teal", "att få ett jobb", "Djurö", "Köln", 1);
            AddMember("Sanjin", 179, 30, "fotboll", "pizza", "blå", "att få ett jobb", "Norrköping", "Mostar", 2);
            AddMember("Oscar", 185, 26, "fotboll", "lasagne", "blå", "att få ett jobb", "Stockholm", "Stockholm", 1);
            AddMember("Johan", 194, 34, "gaming", "tacos", "blå", "en trygg framtid", "Mariefred", "Mariefred", 2);
            AddMember("David", 183, 32, "BJJ", "tacos", "blå", "problemlösning", "Norrtälje", "Göteborg", 1);
        }

        static void AddMember(string name, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings)
        {
            var member = new Member(name, height, age, hobby, favoriteFood, 
                favoriteColor, motivation, homeTown, birthplace, siblings);
            group.Add(member);
        }

        static void Login()
        {
            Console.WriteLine("Välkommen till programmet!");
            do
            {
                Console.Write("Ange koden: ");
                string password = Console.ReadLine();
                if (password == "Bästkusten")
                {
                    WriteSomethingInGreen("Korrekt! Du angav rätt kod!");
                    break;
                }
                else
                {
                    WriteSomethingInRed("Fel kod! Försök igen...");
                    Console.Clear();
                }
            } while (true);
        }

        static void Run()
        {
            bool exit = false;
            do
            {
                Menu();
                int choice = UserChoice();
                switch (choice)
                {
                    case 1:
                        NewScreen("Här är alla medlemmar i Bästkusten:");
                        ShowMembers();
                        Console.ReadKey(true);
                        break;
                    case 2:
                        NewScreen("Vilken gruppmedlem vill du veta mer om?");
                        ShowMembers(1);
                        choice = UserChoice();
                        Console.Clear();
                        group[choice - 1].Describe();
                        Console.ReadKey(true);
                        break;
                    case 3:
                        NewScreen("Vilken gruppmedlem vill du ta bort?");
                        ShowMembers(1);
                        choice = UserChoice();
                        WriteSomethingInRed(string.Format("{0} är nu borttagen.", group[choice - 1].Name));
                        group.RemoveAt(choice - 1);
                        Thread.Sleep(time);
                        break;
                    case 4:
                        WriteSomethingInRed("Programmet avslutas.");
                        exit = true;
                        break;
                    default:
                        WriteSomethingInRed("Du måste vålja mellan 1-4.");
                        break;
                }
            } while (!exit);
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine("1. Lista alla deltagare i gruppen.");
            Console.WriteLine("2. Veta med om en specifik gruppmedlem.");
            Console.WriteLine("3. Ta bort en person ur gruppen.");
            Console.WriteLine("4. Avsluta programmet.");
        }

        static void ShowMembers()
        {
            for (int i = 0; i < group.Count; i++)
            {
                if (i == 0)
                    Console.Write(group[i].Name);
                else if (i + 1 == group.Count)
                    Console.Write(" & " + group[i].Name + ".");
                else
                    Console.Write(", " + group[i].Name);
            }
        }

        static void ShowMembers(int ctr)
        {
            foreach (var member in group)
                Console.WriteLine("{0}. {1}", ctr++, member.Name);
        }

        static int UserChoice()
        {
            int.TryParse(Console.ReadLine(), out int choice);
            return choice;
        }

        static void WriteSomethingInGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Thread.Sleep(time);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void WriteSomethingInRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Thread.Sleep(time);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void NewScreen(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }
    }
}
