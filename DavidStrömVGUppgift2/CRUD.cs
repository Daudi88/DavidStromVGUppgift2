using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DavidStrömVGUppgift2
{
    static class CRUD
    {
        static string path = Path.Combine(Environment.CurrentDirectory, "members.txt");
        static List<string> lines = new List<string>();

        //Här är en metod som lägger till alla gruppmedlemmar i en lista.
        public static List<Member> Read()
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
                    AddMember(members, firstName, lastName, height, age, hobby, favoriteFood, favoriteColor,
                        motivation, homeTown, birthplace, siblings, gender);
                }
            }
            return members;
        }

        //Här är en metod som lägger till en gruppmedlem i listan group.
        public static void AddMember(
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

        //Här är en metod som lägger till en gruppmedlem.
        public static void Create(List<Member> members)
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

            string firstName = Assign("\n\tFörnamn: ");
            if (!Validate(firstName))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string lastName = Assign("\tEfternamn: ");
            if (!Validate(lastName))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(Assign("\tLängd: "), out int height))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(Assign("\tÅlder: "), out int age))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string hobby = Assign("\tHobby: ");
            if (!Validate(hobby))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string favoriteFood = Assign("\tFavoriträtt: ");
            if (!Validate(favoriteFood))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string favoriteColor = Assign("\tFavoritfärg: ");
            if (!Validate(favoriteColor))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string motivation = Assign("\tMotivation till programmering: ");
            if (!Validate(motivation))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string homeTown = Assign("\tHemort: ");
            if (!Validate(homeTown))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string birthplace = Assign("\tFödelseort: ");
            if (!Validate(birthplace))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            if (!int.TryParse(Assign("\tSyskon: "), out int siblings))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            string gender = Assign("\tKön: ");
            if (!Validate(gender))
            {
                Print.TextInRed("\tÅtgärden avbryts...");
                return;
            }

            firstName = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
            lastName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();
            hobby = char.ToUpper(hobby[0]) + hobby.Substring(1).ToLower();
            favoriteFood = char.ToUpper(favoriteFood[0]) + favoriteFood.Substring(1).ToLower();
            favoriteColor = char.ToUpper(favoriteColor[0]) + favoriteColor.Substring(1).ToLower();
            motivation = char.ToUpper(motivation[0]) + motivation.Substring(1).ToLower();
            homeTown = char.ToUpper(homeTown[0]) + homeTown.Substring(1).ToLower();
            birthplace = char.ToUpper(birthplace[0]) + birthplace.Substring(1).ToLower();
            gender = char.ToUpper(gender[0]) + gender.Substring(1).ToLower();

            AddMember(members, firstName, lastName, height, age, hobby, favoriteFood, favoriteColor,
                motivation, homeTown, birthplace, siblings, gender);

            string line = $"{firstName}, {lastName}, {height}, {age}, {hobby}, {favoriteFood}, {favoriteColor}, " +
                $"{motivation}, {homeTown}, {birthplace}, {siblings}, {gender}";

            lines.Add(line);
            lines.Sort();
            File.WriteAllLines(path, lines);

            Print.TextInGreen($"\t{firstName} {lastName} är nu tillagd.");
        }

        public static void Edit(List<Member> members)
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
                Print.ListOfMembers(members);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du ändra på? ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    int member = choice - 1;
                    Print.TextInGreen($"\tDu har valt att ändra på {members[member].ToString()}.");
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
                                case 1:
                                    string firstName = Assign("\tFörnamn: ");
                                    if (!Validate(firstName))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].FirstName = char.ToUpper(firstName[0]) 
                                            + firstName.Substring(1).ToLower();
                                    break;
                                case 2:
                                    string lastName = Assign("\n\tEfternamn: ");
                                    if (!Validate(lastName))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].LastName = char.ToUpper(lastName[0]) 
                                            + lastName.Substring(1).ToLower();
                                    break;
                                case 3:
                                    if (!int.TryParse(Assign("\tLängd: "), out int height))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].Height = height;
                                    break;
                                case 4:
                                    if (!int.TryParse(Assign("\tÅlder: "), out int age))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].Age = age;
                                    break;
                                case 5:
                                    string hobby = Assign("\tHobby: ");
                                    if (!Validate(hobby))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].Hobby = char.ToUpper(hobby[0]) 
                                            + hobby.Substring(1).ToLower();
                                    break;
                                case 6:
                                    string favoriteFood = Assign("\tFavoriträtt: ");
                                    if (!Validate(favoriteFood))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].FavoriteFood = char.ToUpper(favoriteFood[0]) 
                                            + favoriteFood.Substring(1).ToLower();
                                    break;
                                case 7:
                                    string favoriteColor = Assign("\tFavoritfärg: ");
                                    if (!Validate(favoriteColor))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].FavoriteColor = char.ToUpper(favoriteColor[0]) 
                                            + favoriteColor.Substring(1).ToLower();
                                    break;
                                case 8:
                                    string motivation = Assign("\tMotivation till programmering: ");
                                    if (!Validate(motivation))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].Motivation = char.ToUpper(motivation[0]) 
                                            + motivation.Substring(1).ToLower();
                                    break;
                                case 9:
                                    string homeTown = Assign("\tHemort: ");
                                    if (!Validate(homeTown))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].HomeTown = char.ToUpper(homeTown[0]) 
                                            + homeTown.Substring(1).ToLower();
                                    break;
                                case 10:
                                    string birthplace = Assign("\tFödelseort: ");
                                    if (!Validate(birthplace))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].Birthplace = char.ToUpper(birthplace[0])
                                            + birthplace.Substring(1).ToLower();
                                    break;
                                case 11:
                                    if (!int.TryParse(Assign("\tSyskon: "), out int siblings))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].Siblings = siblings;
                                    break;
                                case 12:
                                    string gender = Assign("\tKön: ");
                                    if (!Validate(gender))
                                    {
                                        Print.TextInRed("\tÅtgärden avbryts...");
                                        exit = true;
                                    }
                                    else
                                        members[member].Gender = char.ToUpper(gender[0])
                                            + gender.Substring(1).ToLower();
                                    break;
                                default:
                                    Print.TextInRed($"\n\tDu måste ange en siffra mellan 1 och 12...");
                                    break;
                            };
                        }
                        else
                            break;
                    } while (!exit);
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
                    break;
            } while (true);
        }

        //Här är en metod som låter användaren ta bort en gruppmedlem.
        public static void Delete(List<Member> members)
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
                Print.ListOfMembers(members);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du ta bort? ");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= members.Count)
                {
                    int member = choice - 1;
                    Console.Write($"\tVill du verkligen ta bort {members[member].ToString()}? Y/N ");
                    string decision = Console.ReadLine();
                    if (decision.ToLower().StartsWith('y'))
                    {
                        Print.TextInGreen($"\t{members[member].ToString()} är nu borttagen.");
                        members.RemoveAt(member);
                        lines.RemoveAt(member);
                        File.WriteAllLines(path, lines);
                        break;
                    }
                    else
                        Print.TextInRed($"\t{members[member].ToString()} är inte borttagen.");
                    
                }
                //Om användaren matar in bokstäver eller bara trycker Enter 
                //kommer UserChoice returnera 0 och då bryter vi oss ut ur loopen.
                else if (choice == 0)
                {
                    Print.TextInRed("\tÅtgärden avbryts...");
                    break;
                }
                //Om användaren matar in en siffra som ligger utanför ramen av 
                //listan skrivs ett felmeddelande ut
                else
                {
                    Print.TextInRed($"\n\tDu måste ange en siffra mellan 1 och {members.Count}...");
                }
            } while (true);
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
    }
}
