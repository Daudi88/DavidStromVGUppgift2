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
        static readonly int timeToSleep = 2000;
        static bool exit = false;

        //Här är den första metoden som körs.
        static void Main(string[] args)
        {
            Login();
        }

        //Detta är en metod som hanterar inloggningen till programmet.
        static void Login()
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
                    WriteSomethingInGreen("\n\tKorrekt! Du angav rätt kod!");
                    Run();
                }
                else if (userInput == "")
                {
                    WriteSomethingInGreen("Tips, lösenordet är basgruppens namn");
                    Console.Clear();
                }
                else
                {
                    ctr++;
                    if (ctr > 2)
                    {
                        WriteSomethingInRed("\n\tDu har matat in fel lösenord för många gånger.\n");
                        ExitProgram();
                    }
                    else
                        WriteSomethingInRed("\n\tFel kod! Försök igen...");
                    Console.Clear();
                }
            } while (!exit);
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
                        ListMembers(); 
                        break;
                    case 2:
                        KnowMore();
                        break;
                    case 3:
                        CreateMember();
                        break;
                    case 4:
                        EditMember();
                        break;
                    case 5:
                        RemoveMember();
                        break;
                    case 6:
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
            if (lines.Count > 0)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] member = lines[i].Split(',');
                    string firstName = member[0].Trim();
                    string lastName = member[1].Trim();
                    int.TryParse(member[2], out int height);
                    int.TryParse(member[3], out int age);
                    string hobby = member[4].Trim();
                    string favoriteFood = member[5].Trim();
                    string favoriteColor = member[6].Trim();
                    string motivation = member[7].Trim();
                    string homeTown = member[8].Trim();
                    string birthplace = member[9].Trim();
                    int.TryParse(member[10], out int siblings);
                    string gender = member[11].Trim();
                    AddMember(firstName, lastName, height, age, hobby, favoriteFood, favoriteColor,
                        motivation, homeTown, birthplace, siblings, gender);
                }
            }
        }

        //Här är en metod som lägger till en gruppmedlem i listan group.
        static void AddMember(string firstName, string lastName, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings, string gender)
        {
            var member = new Member(firstName, lastName, height, age, hobby, favoriteFood, 
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
            Console.WriteLine("\t4. Ändra en person i gruppen.");
            Console.WriteLine("\t5. Ta bort en person ur gruppen.");
            Console.WriteLine("\t6. Avsluta programmet.");
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
                Console.Write($"{group[i].FirstName} {group[i].LastName}");

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
                if (ctr > 3)
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
                Console.WriteLine($"\t{ctr++}. {member.FirstName} {member.LastName}");  
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
                    WriteSomethingInGreen($"\tDu vill veta mer om {group[choice - 1].FirstName} {group[choice - 1].LastName}.");
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
                    WriteSomethingInRed($"\n\tDu måste ange en siffra mellan 1 och {group.Count}...");
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
            Member member = new Member();
            string firstName = Assign("\n\tFörnamn: ");
            if (!Validate(firstName))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string lastName = Assign("\tEfternamn: ");
            if (!Validate(lastName))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(Assign("\tLängd: "), out int height))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(Assign("\tÅlder: "), out int age))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string hobby = Assign("\tHobby: ");
            if (!Validate(hobby))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string favoriteFood = Assign("\tFavoriträtt: ");
            if (!Validate(favoriteFood))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string favoriteColor = Assign("\tFavoritfärg: ");
            if (!Validate(favoriteColor))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string motivation = Assign("\tMotivation till programmering: ");
            if (!Validate(motivation))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string homeTown = Assign("\tHemort: ");
            if (!Validate(homeTown))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string birthplace = Assign("\tFödelseort: ");
            if (!Validate(birthplace))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(Assign("\tSyskon: "), out int siblings))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            string gender = Assign("\tKön: ");
            if (!Validate(gender))
            {
                WriteSomethingInRed("\tÅtgärden avbryts...");
                return;
            }

            firstName = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
            lastName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();
            hobby = hobby.ToLower();
            favoriteFood = favoriteFood.ToLower();
            favoriteColor = favoriteColor.ToLower();
            motivation = motivation.ToLower();
            homeTown = char.ToUpper(homeTown[0]) + homeTown.Substring(1).ToLower();
            birthplace = char.ToUpper(birthplace[0]) + birthplace.Substring(1).ToLower();
            gender = gender.ToLower();

            AddMember(firstName, lastName, height, age, hobby, favoriteFood, favoriteColor, 
                motivation, homeTown, birthplace, siblings, gender);
            
            string line = $"{firstName}, {lastName}, {height}, {age}, {hobby}, {favoriteFood}, {favoriteColor}, " +
                $"{motivation}, {homeTown}, {birthplace}, {siblings}, {gender}";
            
            lines.Add(line);
            File.WriteAllLines(path, lines);
            
            WriteSomethingInGreen($"\t{firstName} {lastName} är nu tillagd.");
        }

        static string Assign(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static bool Validate(string str)
        {
            if (str == "")
                return false;
            return true;
        }

        static void EditMember()
        {
            do
            {
                Console.Clear();
                Console.WriteLine(@"         _   _            _");
                Console.WriteLine(@"        (_)_(_)          | |");
                Console.WriteLine(@"          / \   _ __   __| |_ __ __ _");
                Console.WriteLine(@"         / _ \ | '_ \ / _` | '__/ _` |");
                Console.WriteLine(@"        / ___ \| | | | (_| | | | (_| |");
                Console.WriteLine(@"       /_/   \_\_| |_|\__,_|_|  \__,_|");
                Console.WriteLine();
                ShowMembers(1);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du ändra på? ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    int member = choice - 1;
                    WriteSomethingInGreen($"\tDu har valt att ändra på {group[member].FirstName} {group[member].LastName}.");
                    bool exit = false;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"\n\t 1. Förnamn: {group[member].FirstName}");
                        Console.WriteLine($"\t 2. Efternamn: {group[member].LastName}");
                        Console.WriteLine($"\t 3. Längd: {group[member].Height}");
                        Console.WriteLine($"\t 4. Ålder: {group[member].Age}");
                        Console.WriteLine($"\t 5. Hobby: {group[member].Hobby}");
                        Console.WriteLine($"\t 6. Favoriträtt: {group[member].FavoriteFood}");
                        Console.WriteLine($"\t 7. Favoritfärg: {group[member].FavoriteColor}");
                        Console.WriteLine($"\t 8. Motivation till programmering: {group[member].Motivation}");
                        Console.WriteLine($"\t 9. Hemort: {group[member].HomeTown}");
                        Console.WriteLine($"\t10. Födelseort: {group[member].Birthplace}");
                        Console.WriteLine($"\t11. Syskon: {group[member].Siblings}");
                        Console.WriteLine($"\t12. Kön: {group[member].Gender}");
                        Console.WriteLine("\n\tTryck bara Enter för att avbryta och gå tillbaka...");
                        Console.Write($"\tVad vill du ändra på? ");
                        if (int.TryParse(Console.ReadLine(), out choice))
                        {
                            Console.WriteLine();
                            switch (choice)
                            {
                                case 1:
                                    string firstName = Assign("\tFörnamn: ");
                                    if (!Validate(firstName))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].FirstName = firstName;
                                    break;
                                case 2:
                                    string lastName = Assign("\n\tEfternamn: ");
                                    if (!Validate(lastName))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].LastName = lastName;
                                    break;
                                case 3:
                                    if (!int.TryParse(Assign("\tLängd: "), out int height))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].Height = height;
                                    break;
                                case 4:
                                    if (!int.TryParse(Assign("\tÅlder: "), out int age))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].Age = age;
                                    break;
                                case 5:
                                    string hobby = Assign("\tHobby: ");
                                    if (!Validate(hobby))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].Hobby = hobby;
                                    break;
                                case 6:
                                    string favoriteFood = Assign("\tFavoriträtt: ");
                                    if (!Validate(favoriteFood))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].FavoriteFood = favoriteFood;
                                    break;
                                case 7:
                                    string favoriteColor = Assign("\tFavoritfärg: ");
                                    if (!Validate(favoriteColor))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].FavoriteColor = favoriteColor;
                                    break;
                                case 8:
                                    string motivation = Assign("\tMotivation till programmering: ");
                                    if (!Validate(motivation))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].Motivation = motivation;
                                    break;
                                case 9:
                                    string homeTown = Assign("\tHemort: ");
                                    if (!Validate(homeTown))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].HomeTown = homeTown;
                                    break;
                                case 10:
                                    string birthplace = Assign("\tFödelseort: ");
                                    if (!Validate(birthplace))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].Birthplace = birthplace;
                                    break;
                                case 11:
                                    if (!int.TryParse(Assign("\tSyskon: "), out int siblings))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].Siblings = siblings;
                                    break;
                                case 12:
                                    string gender = Assign("\tKön: ");
                                    if (!Validate(gender))
                                    {
                                        WriteSomethingInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        group[member].Gender = gender;
                                    break;
                                default:
                                    WriteSomethingInRed($"\n\tDu måste ange en siffra mellan 1 och 12...");
                                    break;
                            };
                        }
                        else
                            break;
                    } while (!exit);
                    string line = $"{group[member].FirstName}, {group[member].LastName}, " +
                            $"{group[member].Height}, {group[member].Age}, {group[member].Hobby}, " +
                            $"{group[member].FavoriteFood}, {group[member].FavoriteColor}, " +
                            $"{group[member].Motivation}, {group[member].HomeTown}, " +
                            $"{group[member].Birthplace}, {group[member].Siblings}, " +
                            $"{group[member].Gender}";
                    lines[member] = line;
                    File.WriteAllLines(path, lines);
                }
                else
                    break;
            } while (true);
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
                    WriteSomethingInGreen($"\t{group[choice].FirstName} {group[choice].LastName} är nu borttagen.");
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
            Thread.Sleep(timeToSleep / 2);
        }

        //Här är en metod som skriver utt ett meddelande i röd text till skärmen.
        static void WriteSomethingInRed(string message)
        {
            //Först sätts textfärgen till röd. Sedan skrivs meddelandet ut.
            //Efter det sätts textfärgen tillbaks till vit igen. 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(timeToSleep);
        }
    }
}
