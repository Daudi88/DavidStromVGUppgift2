using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DavidStrömVGUppgift2
{
    class Program
    {
        //Här är de globala variablerna som ska vara tillgängliga för alla
        //metoder i progranmmet. På det här sättet slipper man skicka med 
        //dem som argument vid varje metodanrop.
        static string filePath = @"C:\Code\.NET20D\Objektorienterad programmering i C#\Inlämningsuppgifter\DavidStrömVGUppgift2\members.txt";
        static List<string> lines = new List<string>();
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
            Login();
            Run();
        }

        //Detta är en metod som hanterar inloggningen till programmet.
        static void Login()
        {
            int ctr = 0;
            do
            {
                //Här körs en loop där varje tangenttryck registreras och läggs
                //till i strängen password, men det som syns är bara asterisker.
                string userInput = "";
                Console.WriteLine("Välkommen till programmet!");
                Console.Write("Ange lösenordet: ");
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
                    WriteSomethingInGreen("\nKorrekt! Du angav rätt kod!");
                    break;
                }
                else
                {
                    ctr++;
                    if (ctr > 2)
                    {
                        WriteSomethingInRed("\nDu har matat in fel lösenord för många gånger." +
                            "\nDu måste vänta lite innan du försöker igen.");
                        Thread.Sleep(time * 5);
                        ctr = 0;
                    }
                    else
                        WriteSomethingInRed("\nFel kod! Försök igen...");
                    Console.Clear();
                }
            } while (true);
        }

        //Här är en metod som styr hela programmet när man väl loggat in.
        //Metoden körs tills användaren väljer att avsluta.
        static void Run()
        {
            PopulateGroup();
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
                        WriteSomethingInGreen("Du valde att lägga till en gruppmedlem.");
                        CreateMember();
                        break;
                    case 4:
                        WriteSomethingInGreen("Du valde att ta bort en gruppmedlem.");
                        RemoveMember();
                        break;
                    case 5:
                        WriteSomethingInGreen("Du valde att avsluta programmet.");
                        ExitProgram();
                        break;
                    default:
                        WriteSomethingInRed("Du måste vålja mellan 1-4.");
                        break;
                }
            } while (!exit);
        }

        //Här är en metod som lägger till alla gruppmedlemmar i listan group.
        static void PopulateGroup()
        {
            
            lines = File.ReadAllLines(filePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                string[] member = lines[i].Split(',');
                string name = member[0];
                int.TryParse(member[1], out int height);
                int.TryParse(member[2], out int age);
                string hobby = member[3];
                string favoriteFood = member[4];
                string favoriteColor = member[5];
                string motivation = member[6];
                string homeTown = member[7];
                string birthplace = member[8];
                int.TryParse(member[9], out int siblings);
                string gender = member[10];
                AddMember(name, height, age, hobby, favoriteFood, favoriteColor,
                    motivation, homeTown, birthplace, siblings, gender);
            }
        }

        //Här är en metod som lägger till en gruppmedlem i listan group.
        static void AddMember(string name, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings, string gender)
        {
            var member = new Member(name, height, age, hobby, favoriteFood, 
                favoriteColor, motivation, homeTown, birthplace, siblings, gender);
            group.Add(member);
        }

        //Här är en metod som skriver ut menyn till skärmen.
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine("1. Lista alla deltagare i gruppen.");
            Console.WriteLine("2. Veta mer om en specifik gruppmedlem.");
            Console.WriteLine("3. Lägg till en person i gruppen.");
            Console.WriteLine("4. Ta bort en person ur gruppen.");
            Console.WriteLine("5. Avsluta programmet.");
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
                Console.WriteLine($"{ctr++}. {member.Name}");  
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
                Console.WriteLine("\nTryck bara Enter om du vill avbryta och återgå till menyn...");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= group.Count)
                {
                    WriteSomethingInGreen($"Du vill veta mer om {group[choice - 1].Name}.");
                    Console.Clear();
                    group[choice - 1].Describe();
                    Console.ReadKey(true);
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

        
        //Här är en metod som lägger till en gruppmedlem.
        static void CreateMember()
        {
            Console.Clear();
            Console.WriteLine("Du kommer nu få fylla i lite olika detaljer om " +
                "den nya gruppmedlemmen.");
            Console.Write("Namn: ");
            string name = Console.ReadLine();
            Console.Write("Längd: ");
            int.TryParse(Console.ReadLine(), out int height);
            Console.Write("Ålder: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Hobby: ");
            string hobby = Console.ReadLine();
            Console.Write("Favoriträtt: ");
            string favoriteFood = Console.ReadLine();
            Console.Write("Favoritfärg: ");
            string favoriteColor = Console.ReadLine();
            Console.Write("Motivation till programmering: ");
            string motivation = Console.ReadLine();
            Console.Write("Hemort: ");
            string homeTown = Console.ReadLine();
            Console.Write("Födelseort: ");
            string birthplace = Console.ReadLine();
            Console.Write("Syskon: ");
            int.TryParse(Console.ReadLine(), out int siblings);
            Console.Write("Kön: ");
            string gender = Console.ReadLine();

            name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
            hobby = hobby.ToLower();
            favoriteFood = favoriteFood.ToLower();
            favoriteColor = favoriteColor.ToLower();
            motivation = motivation.ToLower();
            homeTown = char.ToUpper(homeTown[0]) + homeTown.Substring(1).ToLower();
            birthplace = char.ToUpper(birthplace[0]) + birthplace.Substring(1).ToLower();
            gender = gender.ToLower();

            AddMember(name, height, age, hobby, favoriteFood, favoriteColor, 
                motivation, homeTown, birthplace, siblings, gender);
            string line = $"{name}, {height}, {age}, {hobby}, {favoriteFood}, {favoriteColor}, " +
                $"{motivation}, {homeTown}, {birthplace}, {siblings}, {gender}";
            lines.Add(line);
            File.WriteAllLines(filePath, lines);
            WriteSomethingInGreen($"{name} är nu tillagd.");
            Thread.Sleep(time / 2);
        }

        //Här är en metod som låter användaren ta bort en gruppmedlem.
        static void RemoveMember()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Vilken gruppmedlem vill du ta bort?\n");
                ShowMembers(1);
                Console.WriteLine("\nTryck bara Enter om du vill avbryta och återgå till menyn...");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= group.Count)
                {
                    choice--;
                    WriteSomethingInGreen(string.Format("{0} är nu borttagen.", group[choice].Name));
                    group.RemoveAt(choice);
                    lines.RemoveAt(choice);
                    File.WriteAllLines(filePath, lines);
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
