using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DavidStrömVGUppgift2
{
    static class CRUD
    {
        static string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\members.txt");
        static List<string> lines = new List<string>();

        //Här är en metod som läser in alla gruppmedlemmar från en textfil.
        public static List<Member> ReadFromTextFile()
        {
            lines = File.ReadAllLines(path).ToList();
            List<Member> members = new List<Member>();
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
                    AddMemberToList(members, firstName, lastName, height, age, hobby, favoriteFood, favoriteColor,
                        motivation, homeTown, birthplace, siblings, gender);
                }
            }
            return members;
        }

        //Här är en metod som lägger till en gruppmedlem i en lista.
        public static void AddMemberToList(
            List<Member> members,
            string firstName,
            string lastName,
            int height,
            int age,
            string hobby,
            string favoriteFood,
            string favoriteColor,
            string motivation,
            string homeTown,
            string birthplace,
            int siblings,
            string gender)
        {
            var member = new Member(firstName, lastName, height, age, hobby, favoriteFood,
                favoriteColor, motivation, homeTown, birthplace, siblings, gender);
            members.Add(member);
        }

        //Här är en metod som skapar en ny gruppmedlem.
        public static void CreateNewMember(List<Member> members)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"         _     _   _                     _   _ _ _ ");
            Console.WriteLine(@"        | |   (_) (_)                   | | (_) | |");
            Console.WriteLine(@"        | |     __ _  __ _  __ _  __ _  | |_ _| | |");
            Console.WriteLine(@"        | |    / _` |/ _` |/ _` |/ _` | | __| | | |");
            Console.WriteLine(@"        | |___| (_| | (_| | (_| | (_| | | |_| | | |");
            Console.WriteLine(@"        |______\__,_|\__, |\__, |\__,_|  \__|_|_|_|");
            Console.WriteLine(@"                      __/ | __/ |");
            Console.WriteLine(@"                     |___/ |___/");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tDu kommer nu få fylla i lite detaljer om " +
                "den nya gruppmedlemmen.");
            Console.WriteLine("\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");

            //Här får användaren svara på flera frågor om den nya gruppmedlemmen
            //men kan avbryta åtgärden när som helst.
            string firstName = AssignValue("\n\tFörnamn: ");
            if (!ValidateValue(firstName))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string lastName = AssignValue("\tEfternamn: ");
            if (!ValidateValue(lastName))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(AssignValue("\tLängd: "), out int height))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(AssignValue("\tÅlder: "), out int age))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string hobby = AssignValue("\tHobby: ");
            if (!ValidateValue(hobby))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string favoriteFood = AssignValue("\tFavoriträtt: ");
            if (!ValidateValue(favoriteFood))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string favoriteColor = AssignValue("\tFavoritfärg: ");
            if (!ValidateValue(favoriteColor))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string motivation = AssignValue("\tMotivation till programmering: ");
            if (!ValidateValue(motivation))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string homeTown = AssignValue("\tHemort: ");
            if (!ValidateValue(homeTown))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string birthplace = AssignValue("\tFödelseort: ");
            if (!ValidateValue(birthplace))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(AssignValue("\tSyskon: "), out int siblings))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string gender = AssignValue("\tKön: ");
            if (!ValidateValue(gender))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            //Svarar anvämndaren på alla frågor sparas de i olika variabler som sedan
            //skickas med som argument vid metodanropet av AddMemberToList.
            firstName = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
            lastName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();
            hobby = char.ToUpper(hobby[0]) + hobby.Substring(1).ToLower();
            favoriteFood = char.ToUpper(favoriteFood[0]) + favoriteFood.Substring(1).ToLower();
            favoriteColor = char.ToUpper(favoriteColor[0]) + favoriteColor.Substring(1).ToLower();
            motivation = char.ToUpper(motivation[0]) + motivation.Substring(1).ToLower();
            homeTown = char.ToUpper(homeTown[0]) + homeTown.Substring(1).ToLower();
            birthplace = char.ToUpper(birthplace[0]) + birthplace.Substring(1).ToLower();
            gender = char.ToUpper(gender[0]) + gender.Substring(1).ToLower();

            AddMemberToList(members, firstName, lastName, height, age, hobby, favoriteFood, favoriteColor,
                motivation, homeTown, birthplace, siblings, gender);

            //Variablerna sparas till en stärng som sedan läggs till i en 
            //lista och som sedan till textfilen.
            string line = $"{firstName}, {lastName}, {height}, {age}, {hobby}, {favoriteFood}, {favoriteColor}, " +
                $"{motivation}, {homeTown}, {birthplace}, {siblings}, {gender}";

            lines.Add(line);
            lines.Sort();
            File.WriteAllLines(path, lines);

            Print.TextInGreen($"\t{firstName} {lastName} är nu tillagd.");
        }

        //Här är en metod som ändrar på en gruppmedlem.
        public static void EditMember(List<Member> members)
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"         _   _            _");
                Console.WriteLine(@"        (_)_(_)          | |");
                Console.WriteLine(@"          / \   _ __   __| |_ __ __ _");
                Console.WriteLine(@"         / _ \ | '_ \ / _` | '__/ _` |");
                Console.WriteLine(@"        / ___ \| | | | (_| | | | (_| |");
                Console.WriteLine(@"       /_/   \_\_| |_|\__,_|_|  \__,_|");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Print.ListOfMembers(members);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du ändra på? ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    int member = choice - 1;
                    Print.TextInGreen($"\tDu har valt att ändra på {members[member]}.");
                    bool exit = false;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"\n\t 1. Förnamn: {members[member].FirstName}");
                        Console.WriteLine($"\t 2. Efternamn: {members[member].LastName}");
                        Console.WriteLine($"\t 3. Längd: {members[member].Height}");
                        Console.WriteLine($"\t 4. Ålder: {members[member].Age}");
                        Console.WriteLine($"\t 5. Hobby: {members[member].Hobby}");
                        Console.WriteLine($"\t 6. Favoriträtt: {members[member].FavoriteFood}");
                        Console.WriteLine($"\t 7. Favoritfärg: {members[member].FavoriteColor}");
                        Console.WriteLine($"\t 8. Motivation till programmering: {members[member].Motivation}");
                        Console.WriteLine($"\t 9. Hemort: {members[member].HomeTown}");
                        Console.WriteLine($"\t10. Födelseort: {members[member].Birthplace}");
                        Console.WriteLine($"\t11. Syskon: {members[member].Siblings}");
                        Console.WriteLine($"\t12. Kön: {members[member].Gender}");
                        Console.WriteLine("\n\tTryck bara Enter för att avbryta och gå tillbaka...");
                        Console.Write($"\tVad vill du ändra på? ");
                        if (int.TryParse(Console.ReadLine(), out choice))
                        {
                            Console.WriteLine();
                            switch (choice)
                            {
                                //Användaren får välja vilken information som ska ändras
                                //men kan när som helst avbryta åtgärden.
                                case 1:
                                    string firstName = AssignValue("\tFörnamn: ");
                                    if (!ValidateValue(firstName))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].FirstName = char.ToUpper(firstName[0]) 
                                            + firstName.Substring(1).ToLower();
                                    }
                                    break;
                                case 2:
                                    string lastName = AssignValue("\n\tEfternamn: ");
                                    if (!ValidateValue(lastName))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].LastName = char.ToUpper(lastName[0]) 
                                            + lastName.Substring(1).ToLower();
                                    }
                                    break;
                                case 3:
                                    if (!int.TryParse(AssignValue("\tLängd: "), out int height))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].Height = height;
                                    }
                                    break;
                                case 4:
                                    if (!int.TryParse(AssignValue("\tÅlder: "), out int age))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].Age = age;
                                    }
                                    break;
                                case 5:
                                    string hobby = AssignValue("\tHobby: ");
                                    if (!ValidateValue(hobby))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].Hobby = char.ToUpper(hobby[0]) 
                                            + hobby.Substring(1).ToLower();
                                    }
                                    break;
                                case 6:
                                    string favoriteFood = AssignValue("\tFavoriträtt: ");
                                    if (!ValidateValue(favoriteFood))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].FavoriteFood = char.ToUpper(favoriteFood[0]) 
                                            + favoriteFood.Substring(1).ToLower();
                                    }
                                    break;
                                case 7:
                                    string favoriteColor = AssignValue("\tFavoritfärg: ");
                                    if (!ValidateValue(favoriteColor))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].FavoriteColor = char.ToUpper(favoriteColor[0]) 
                                            + favoriteColor.Substring(1).ToLower();
                                    }
                                    break;
                                case 8:
                                    string motivation = AssignValue("\tMotivation till programmering: ");
                                    if (!ValidateValue(motivation))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].Motivation = char.ToUpper(motivation[0]) 
                                            + motivation.Substring(1).ToLower();
                                    }
                                    break;
                                case 9:
                                    string homeTown = AssignValue("\tHemort: ");
                                    if (!ValidateValue(homeTown))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].HomeTown = char.ToUpper(homeTown[0]) 
                                            + homeTown.Substring(1).ToLower();
                                    }
                                    break;
                                case 10:
                                    string birthplace = AssignValue("\tFödelseort: ");
                                    if (!ValidateValue(birthplace))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].Birthplace = char.ToUpper(birthplace[0])
                                            + birthplace.Substring(1).ToLower();
                                    }
                                    break;
                                case 11:
                                    if (!int.TryParse(AssignValue("\tSyskon: "), out int siblings))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].Siblings = siblings;
                                    }
                                    break;
                                case 12:
                                    string gender = AssignValue("\tKön: ");
                                    if (!ValidateValue(gender))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                    {
                                        members[member].Gender = char.ToUpper(gender[0])
                                            + gender.Substring(1).ToLower();
                                    }
                                    break;
                                default:
                                    Print.TextInRed($"\n\tDu måste ange en siffra mellan 1 och 12...");
                                    break;
                            };
                        }
                        else
                        {
                            break;
                        }

                    } while (!exit);
                    
                    //När användaren är klar med sina ändringar sparas dessa till textfilen.
                    string line = $"{members[member].FirstName}, {members[member].LastName}, " +
                            $"{members[member].Height}, {members[member].Age}, {members[member].Hobby}, " +
                            $"{members[member].FavoriteFood}, {members[member].FavoriteColor}, " +
                            $"{members[member].Motivation}, {members[member].HomeTown}, " +
                            $"{members[member].Birthplace}, {members[member].Siblings}, " +
                            $"{members[member].Gender}";
                    lines[member] = line;
                    File.WriteAllLines(path, lines);
                }
                else
                {
                    break;
                }

            } while (true);
        }

        //Här är en metod som tar bort en gruppmedlem.
        public static void DeleteMember(List<Member> members)
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"         _______      _                _");
                Console.WriteLine(@"        |__   __|    | |              | |");
                Console.WriteLine(@"           | | __ _  | |__   ___  _ __| |_");
                Console.WriteLine(@"           | |/ _` | | '_ \ / _ \| '__| __|");
                Console.WriteLine(@"           | | (_| | | |_) | (_) | |  | |_");
                Console.WriteLine(@"           |_|\__,_| |_.__/ \___/|_|   \__|");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Print.ListOfMembers(members);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du ta bort? ");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= members.Count)
                {
                    int member = choice - 1;
                    Console.Write($"\tVill du verkligen ta bort {members[member]}? Y/N ");
                    string decision = Console.ReadLine();
                    if (decision.ToLower().StartsWith('y'))
                    {
                        Print.TextInGreen($"\t{members[member]} är nu borttagen.");
                        members.RemoveAt(member);
                        lines.RemoveAt(member);
                        File.WriteAllLines(path, lines);
                        break;
                    }
                    else
                    {
                        Print.TextInRed($"\t{members[member]} är inte borttagen.");
                    }
                    
                }
                else if (choice == 0)
                {
                    //Om användaren matar in bokstäver eller bara trycker Enter 
                    //kommer choice returnera 0 och då bryter vi oss ut ur loopen.
                    Print.TextInRed("\tÅtgärden avbryts...");
                    break;
                }
                else
                {
                    //Om användaren matar in en siffra som ligger utanför ramen av 
                    //listan skrivs ett felmeddelande ut.
                    Print.TextInRed($"\n\tDu måste ange en siffra mellan 1 och {members.Count}...");
                }

            } while (true);
        }

        //Här är en metod som skriver ut ett meddelande 
        //och returnerar svaret från användaren.
        static string AssignValue(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        //Här är en metod som kontrollerar en strängs längd.
        static bool ValidateValue(string str) => str.Length > 0;
    }
}
