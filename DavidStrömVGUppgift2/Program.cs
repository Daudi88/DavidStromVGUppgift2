using System;
using System.Collections.Generic;
using System.Threading;

namespace DavidStrömVGUppgift2
{
    class Program
    {
        //Här är de globala variablerna som ska vara tillgängliga för alla
        //metoder i progranmmet. På det här sättet slipper man skicka med 
        //dem som argument vid varje metodanrop.
        static List<Member> group = new List<Member>();
        static int time = 2000;
        static bool exit = false;

        //Här är den första metoden som körs.
        static void Main(string[] args)
        {
            //Här anropas metoden Start som sätter igång hela programmet.
            Start();
        }

        //Här är en metod som sätter igång hela programmet.
        static void Start()
        {
            PopulateGroup();
            Login();
            Run();
        }

        //Här är en metod som lägger till alla gruppmedlemmar i listan group.
        static void PopulateGroup()
        {
            AddMember("Elin", 170, 31, "hästar", "sushi", "röd", "personliga utveckling", "Knivsta", "Karlskoga", 2, "hon");
            AddMember("Cecilia", 163, 29, "The Sims", "risotto", "gul", "kreativitet", "Norrköping", "Norrköping", 1, "hon");
            AddMember("Jeremy", 181, 19, "gaming", "älggryta", "teal", "att få ett jobb", "Djurö", "Köln", 1, "han");
            AddMember("Sanjin", 179, 30, "fotboll", "pizza", "blå", "att få ett jobb", "Norrköping", "Mostar", 2, "han");
            AddMember("Oscar", 185, 26, "fotboll", "lasagne", "blå", "att få ett jobb", "Stockholm", "Stockholm", 1, "han");
            AddMember("Johan", 194, 34, "gaming", "tacos", "blå", "en trygg framtid", "Mariefred", "Mariefred", 2, "han");
            AddMember("David", 183, 32, "BJJ", "tacos", "blå", "problemlösning", "Norrtälje", "Göteborg", 1, "han");
            AddMember("Ivo", 174, 42, "fotografering", "scampi", "svart", "kreativitet", "Uppsala", "Split", 1, "han"); 
        }

        //Här är en metod som lägger till en gruppmedlem i listan group.
        static void AddMember(string name, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings, string pronoun)
        {
            var member = new Member(name, height, age, hobby, favoriteFood, 
                favoriteColor, motivation, homeTown, birthplace, siblings, pronoun);
            group.Add(member);
        }

        //Detta är en metod som hanterar inloggningen till programmet.
        static void Login()
        {
            do
            {
                //Här körs en loop där varje tangenttryck registreras och läggs
                //till i strängen password, men det som syns är bara asterisker.
                string password = "";
                Console.WriteLine("Välkommen till programmet!");
                Console.Write("Ange lösenordet: ");
                ConsoleKey key;
                do
                {
                    //Varje knapptryck sparas i keyInfo men syns inte på skärmen.
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    key = keyInfo.Key;
                    if (key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        //Om man trycker Backspace raderas asterisken från skärmen
                        //och tecknet som tidigare sparats i password tas bort.
                        Console.Write("\b \b");
                        password = password[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        //Här skrivs en asterisk ut till skärmen och knapptrycket
                        //sparas till password.
                        Console.Write("*");
                        password += keyInfo.KeyChar;
                    }
                //loopen körs så länge man inte trycker på Enter.
                } while (key != ConsoleKey.Enter);

                if (password == "Bästkusten")
                {
                    WriteSomethingInGreen("\nKorrekt! Du angav rätt kod!");
                    break;
                }
                else
                {
                    WriteSomethingInRed("\nFel kod! Försök igen...");
                    Console.Clear();
                }
            } while (true);
        }

        //Här är en metod som skriver ut menyn till skärmen.
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine("1. Lista alla deltagare i gruppen.");
            Console.WriteLine("2. Veta mer om en specifik gruppmedlem.");
            Console.WriteLine("3. Ta bort en person ur gruppen.");
            Console.WriteLine("4. Avsluta programmet.");
        }
        
        //Här är en metod som styr hela programmet när man väl loggat in.
        //Metoden körs tills användaren väljer att avsluta.
        static void Run()
        {
            do
            {
                Menu();
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        WriteSomethingInGreen("Du valde att lista alla deltagare i gruppen.");
                        ListMembers(); 
                        break;
                    case 2:
                        WriteSomethingInGreen("Du valde att veta mer om en specifik gruppmedlem.");
                        KnowMore();
                        break;
                    case 3:
                        WriteSomethingInGreen("Du valde att ta bort en gruppmedlem.");
                        RemoveMember();
                        break;
                    case 4:
                        WriteSomethingInGreen("Du valde att avsluta programmet.");
                        ExitProgram();
                        break;
                    default:
                        WriteSomethingInRed("Du måste vålja mellan 1-4.");
                        break;
                }
            } while (!exit);
        }

        //Här är en metod som skriver ut alla gruppmedlemmar till skärmen.
        static void ListMembers()
        {
            Console.Clear();
            Console.WriteLine("Detta är alla medlemmar i Bästkusten:\n");
            ShowMembers();
            Console.Write("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey(true);
        }

        //Här är en metod som skriver ut alla gruppmedlemmar till skärmen
        //separerade av ett kommatecken.
        static void ShowMembers()
        {
            int ctr = 0;
            //Här används en for-loop istället för en foreach för att kunna
            //kontrollera när &-teckenet ska skrivas ut.
            for (int i = 0; i < group.Count; i++)
            {
                Console.Write(group[i].Name);

                //Här körs en if-sats för att se till att namnen skrivs ut på 
                //ett snyggt sätt och att ett &-tecken skrivs ut innan sista namnet.
                if (i + 2 == group.Count)
                    Console.Write(" & ");
                else if (i + 1 == group.Count)
                    Console.WriteLine(".");
                else
                    Console.Write(", ");
                ctr++;

                //Här bryter vi till en ny rad efter att halva gruppens
                //medlemmar har skrivits ut.
                if (ctr > group.Count / 2)
                {
                    Console.WriteLine();
                    ctr = 0;
                }
            }
        }

        //Här är en överladdning av metoden innan som också skriver ut alla
        //gruppmedlemmar till skärmen, men här listade med siffror på olika rader. 
        static void ShowMembers(int ctr)
        {
            foreach (var member in group)
            {
                Console.Write($"{ctr++}. {member.Name}");  
            }
        }

        //Här är en metod som låter användaren få mer detaljer om en specifik
        //gruppmedlem.
        static void KnowMore()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Vilken gruppmedlem vill du veta mer om?\n");
                ShowMembers(1);
                Console.Write("\nTryck bara Enter om du vill avbryta och återgå till menyn...");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= group.Count)
                {
                    WriteSomethingInGreen($"Du vill veta mer om {group[choice - 1].Name}.");
                    Console.Clear();
                    group[choice - 1].Describe();
                    Console.ReadKey(true);
                    KnowMore();
                }
                //Om användaren matar in bokstäver eller bara trycker Enter 
                //kommer UserChoice returnera 0 och då bryter vi oss ut ur loopen.
                else if (choice == 0)
                    break;
                //Om användaren matar in en siffra som ligger utanför ramen av 
                //listan skrivs ett felmeddelande ut
                else
                {
                    WriteSomethingInRed($"\nDu måste ange en siffra mellan 1 och {group.Count}.");
                }
                
            } while (true);
        }

        //Här är en metod som låter användaren ta bort en gruppmedlem.
        static void RemoveMember()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Vilken gruppmedlem vill du ta bort?\n");
                ShowMembers(1);
                Console.Write("\nTryck bara Enter om du vill avbryta och återgå till menyn...");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= group.Count)
                {
                    WriteSomethingInGreen(string.Format("{0} är nu borttagen.", group[choice - 1].Name));
                    group.RemoveAt(choice - 1);
                    break;
                }
                //Om användaren matar in bokstäver eller bara trycker Enter 
                //kommer UserChoice returnera 0 och då bryter vi oss ut ur loopen.
                else if (choice == 0)
                    break;
                //Om användaren matar in en siffra som ligger utanför ramen av 
                //listan skrivs ett felmeddelande ut
                else
                {
                    WriteSomethingInRed($"\nDu måste ange en siffra mellan 1 och {group.Count}.");
                }
            } while (true);
            
        }

        //Här är en metod som avslutar programmet.
        static void ExitProgram()
        {
            //Här sätts exit till true och ett avslutande meddelande skrivs ut
            //med lite visuell effekt som får det att se ut som att datorn tänker.
            exit = true;
            WriteSomethingInGreen("\nProgrammet avslutas");
            for (int i = 0; i < 3; i++)
            {
                WriteSomethingInGreen(".");
            }
        }

        //Här är en metod som skriver utt ett meddelande i grön text till skärmen.
        static void WriteSomethingInGreen(string message)
        {
            //Först sätts textfärgen till grön. Sedan skrivs meddelandet ut.
            //Efter det sätts textfärgen tillbaks till vit igen.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(time / 2);
        }

        //Här är en metod som skriver utt ett meddelande i röd text till skärmen.
        static void WriteSomethingInRed(string message)
        {
            //Först sätts textfärgen till röd. Sedan skrivs meddelandet ut.
            //Efter det sätts textfärgen tillbaks till vit igen. 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(time);
        }
    }
}
