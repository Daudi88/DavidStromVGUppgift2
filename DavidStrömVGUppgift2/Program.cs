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
        //Med hjälp av path kan man skriva till och läsa från en textfil.
        static string path = Path.Combine(Environment.CurrentDirectory, "members.txt");
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

        private static void Title()
        {
            Console.WriteLine(@"         ____  _   _     _   _              _");
            Console.WriteLine(@"        |  _ \(_) (_)   | | | |            | |");
            Console.WriteLine(@"        | |_) | __ _ ___| |_| | ___   _ ___| |_ ___ _ __");
            Console.WriteLine(@"        |  _ < / _` / __| __| |/ / | | / __| __/ _ \ '_ \");
            Console.WriteLine(@"        | |_) | (_| \__ \ |_|   <| |_| \__ \ ||  __/ | | |");
            Console.WriteLine(@"        |____/ \__,_|___/\__|_|\_\\__,_|___/\__\___|_| |_|");
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
                Title();
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
                    WriteSomethingInGreen("\n\tKorrekt! Du angav rätt kod!");
                    break;
                }
                else
                {
                    ctr++;
                    if (ctr > 2)
                    {
                        WriteSomethingInRed("\n\tDu har matat in fel lösenord för många gånger." +
                            "\n\tDu måste vänta lite innan du försöker igen.");
                        Thread.Sleep(time * 5);
                        ctr = 0;
                    }
                    else
                        WriteSomethingInRed("\n\tFel kod! Försök igen...");
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
                        WriteSomethingInGreen("\tDu valde att visa alla medlemmar i gruppen.");
                        ListMembers(); 
                        break;
                    case 2:
                        WriteSomethingInGreen("\tDu valde att veta mer om en specifik gruppmedlem.");
                        KnowMore();
                        break;
                    case 3:
                        WriteSomethingInGreen("\tDu valde att lägga till en gruppmedlem.");
                        CreateMember();
                        break;
                    case 4:
                        WriteSomethingInGreen("\tDu valde att ta bort en gruppmedlem.");
                        RemoveMember();
                        break;
                    case 5:
                        ExitProgram();
                        break;
                    default:
                        WriteSomethingInRed("\tDu måste vålja mellan 1-4...");
                        break;
                }
            } while (!exit);
        }

        //Här är en metod som lägger till alla gruppmedlemmar i listan group.
        static void PopulateGroup()
        {
            
            lines = File.ReadAllLines(path).ToList();
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
            Console.WriteLine(@"         _    _                       _");
            Console.WriteLine(@"        | |  | |                     | |");
            Console.WriteLine(@"        | |__| |_   ___   ___   _  __| |_ __ ___   ___ _ __  _   _");
            Console.WriteLine(@"        |  __  | | | \ \ / / | | |/ _` | '_ ` _ \ / _ \ '_ \| | | |");
            Console.WriteLine(@"        | |  | | |_| |\ V /| |_| | (_| | | | | | |  __/ | | | |_| |");
            Console.WriteLine(@"        |_|  |_|\__,_| \_/  \__,_|\__,_|_| |_| |_|\___|_| |_|\__, |");
            Console.WriteLine(@"                                                              __/ |");
            Console.WriteLine(@"        1. Visa medlemmarna i gruppen.                       |___/ ");
            Console.WriteLine("\t2. Veta mer om en specifik gruppmedlem.");
            Console.WriteLine("\t3. Lägg till en person i gruppen.");
            Console.WriteLine("\t4. Ta bort en person ur gruppen.");
            Console.WriteLine("\t5. Avsluta programmet.");
            Console.Write("\n\tVad vill du göra? ");

        }

        //Här är en metod som skriver ut alla gruppmedlemmar till skärmen.
        static void ListMembers()
        {
            Console.Clear();
            Console.WriteLine(@"         __  __          _ _");
            Console.WriteLine(@"        |  \/  |        | | |");
            Console.WriteLine(@"        | \  / | ___  __| | | ___ _ __ ___  _ __ ___   __ _ _ __");
            Console.WriteLine(@"        | |\/| |/ _ \/ _` | |/ _ \ '_ ` _ \| '_ ` _ \ / _` | '__|");
            Console.WriteLine(@"        | |  | |  __/ (_| | |  __/ | | | | | | | | | | (_| | |");
            Console.WriteLine(@"        |_|  |_|\___|\__,_|_|\___|_| |_| |_|_| |_| |_|\__,_|_|");
            Console.WriteLine();
            ShowMembers();
            Console.Write("\n\tTryck på valfri tangent för att återgå till huvudmenyn...");
            Console.ReadKey(true);
        }

        //Här är en metod som skriver ut alla gruppmedlemmar till skärmen
        //separerade av ett kommatecken.
        static void ShowMembers()
        {
            int ctr = 0;
            Console.Write("\t");

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
                if (ctr > 8)
                {
                    Console.Write("\n\t");
                    ctr = 0;
                }
            }
        }

        //Här är en överladdning av metoden innan som också skriver ut alla
        //gruppmedlemmar till skärmen, men här listade med siffror på olika rader. 
        static void ShowMembers(int ctr)
        {
            foreach (var member in group)
                Console.WriteLine($"\t{ctr++}. {member.Name}");  
        }

        //Här är en metod som låter användaren få mer detaljer om en specifik
        //gruppmedlem.
        static void KnowMore()
        {
            do
            {
                Console.Clear();
                Console.WriteLine(@"        __      __  _");
                Console.WriteLine(@"        \ \    / / | |");
                Console.WriteLine(@"         \ \  / /__| |_ __ _   _ __ ___   ___ _ __ ");
                Console.WriteLine(@"          \ \/ / _ \ __/ _` | | '_ ` _ \ / _ \ '__|");
                Console.WriteLine(@"           \  /  __/ || (_| | | | | | | |  __/ |");
                Console.WriteLine(@"            \/ \___|\__\__,_| |_| |_| |_|\___|_|");
                Console.WriteLine();
                ShowMembers(1);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du veta mer om? ");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= group.Count)
                {
                    WriteSomethingInGreen($"\tDu vill veta mer om {group[choice - 1].Name}.");
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
                    WriteSomethingInRed($"\n\tDu måste ange en siffra mellan 1 och {group.Count}...");
                }
                
            } while (true);
        }

        
        //Här är en metod som lägger till en gruppmedlem.
        static void CreateMember()
        {
            Console.Clear();
            Console.WriteLine(@"         _     _   _                     _   _ _ _ ");
            Console.WriteLine(@"        | |   (_) (_)                   | | (_) | |");
            Console.WriteLine(@"        | |     __ _  __ _  __ _  __ _  | |_ _| | |");
            Console.WriteLine(@"        | |    / _` |/ _` |/ _` |/ _` | | __| | | |");
            Console.WriteLine(@"        | |___| (_| | (_| | (_| | (_| | | |_| | | |");
            Console.WriteLine(@"        |______\__,_|\__, |\__, |\__,_|  \__|_|_|_|");
            Console.WriteLine(@"                      __/ | __/ |");
            Console.WriteLine(@"                     |___/ |___/");
            Console.WriteLine("\n\tDu kommer nu få fylla i lite detaljer om " +
                "den nya gruppmedlemmen.");
            Console.WriteLine("\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
            Console.Write("\n\tNamn: ");
            string name = Console.ReadLine();
            if (name == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tLängd: ");
            if (!int.TryParse(Console.ReadLine(), out int height))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tÅlder: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tHobby: ");
            string hobby = Console.ReadLine();
            if (hobby == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tFavoriträtt: ");
            string favoriteFood = Console.ReadLine();
            if (favoriteFood == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tFavoritfärg: ");
            string favoriteColor = Console.ReadLine();
            if (favoriteColor == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tMotivation till programmering: ");
            string motivation = Console.ReadLine();
            if (motivation == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tHemort: ");
            string homeTown = Console.ReadLine();
            if (homeTown == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tFödelseort: ");
            string birthplace = Console.ReadLine();
            if (birthplace == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tSyskon: ");
            if (!int.TryParse(Console.ReadLine(), out int siblings))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }
            Console.Write("\tKön: ");
            string gender = Console.ReadLine();
            if (gender == "")
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

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
            File.WriteAllLines(path, lines);
            WriteSomethingInGreen($"\t{name} är nu tillagd.");
            Thread.Sleep(time / 2);
        }

        //Här är en metod som låter användaren ta bort en gruppmedlem.
        static void RemoveMember()
        {
            do
            {
                Console.Clear();
                Console.WriteLine(@"         _______      _                _");
                Console.WriteLine(@"        |__   __|    | |              | |");
                Console.WriteLine(@"           | | __ _  | |__   ___  _ __| |_");
                Console.WriteLine(@"           | |/ _` | | '_ \ / _ \| '__| __|");
                Console.WriteLine(@"           | | (_| | | |_) | (_) | |  | |_");
                Console.WriteLine(@"           |_|\__,_| |_.__/ \___/|_|   \__|");
                Console.WriteLine();
                ShowMembers(1);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du ta bort? ");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= group.Count)
                {
                    choice--;
                    WriteSomethingInGreen(string.Format("\t{0} är nu borttagen.", group[choice].Name));
                    group.RemoveAt(choice);
                    lines.RemoveAt(choice);
                    File.WriteAllLines(path, lines);
                    break;
                }
                //Om användaren matar in bokstäver eller bara trycker Enter 
                //kommer UserChoice returnera 0 och då bryter vi oss ut ur loopen.
                else if (choice == 0)
                {
                    WriteSomethingInRed("\tÅtgärden avbryts...");
                    break;
                }
                //Om användaren matar in en siffra som ligger utanför ramen av 
                //listan skrivs ett felmeddelande ut
                else
                {
                    WriteSomethingInRed($"\n\tDu måste ange en siffra mellan 1 och {group.Count}...");
                }
            } while (true);

        }

        //Här är en metod som avslutar programmet.
        static void ExitProgram()
        {
            //Här sätts exit till true och ett avslutande meddelande skrivs ut
            //med lite visuell effekt som får det att se ut som att datorn tänker.
            exit = true;
            WriteSomethingInGreen("\tProgrammet avslutas");
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
