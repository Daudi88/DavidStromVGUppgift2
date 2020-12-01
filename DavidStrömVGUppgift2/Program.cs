using System;
using System.Collections.Generic;
using System.Threading;

namespace DavidStrömVGUppgift2
{
    class Program
    {
        static List<Member> group = new List<Member>();
        static void Main(string[] args)
        {
            PopulateGroup("Elin", 170, 31, "hästar", "sushi", "röd", "personliga utveckling", "Knivsta", "Karlskoga", 2);
            PopulateGroup("Cecilia", 163, 29, "The Sims", "risotto", "gul", "kreativitet", "Norrköping", "Norrköping", 1);
            PopulateGroup("Jeremy", 181, 19, "gaming", "älggryta", "teal", "att få ett jobb", "Djurö", "Köln", 1);
            PopulateGroup("Sanjin", 179, 30, "fotboll", "pizza", "blå", "att få ett jobb", "Norrköping", "Mostar", 2);
            PopulateGroup("Oscar", 185, 26, "fotboll", "lasagne", "blå", "att få ett jobb", "Stockholm", "Stockholm", 1);
            PopulateGroup("Johan", 194, 34, "gaming", "tacos", "blå", "en trygg framtid", "Mariefred", "Mariefred", 2);
            PopulateGroup("David", 183, 32, "BJJ", "tacos", "blå", "problemlösning", "Norrtälje", "Göteborg", 1);

            //1.Programmet skall vid uppstart fråga användaren om en kod. Om 
            //denna kod är == med namnet på er basgrupp skall tillgång till 
            //resten av programmet ges.Annars skall ett passande meddelande skrivas ut.

            int time = 2000;
            Console.WriteLine("Välkommen till programmet!");
            do
            {
                Console.Write("Ange koden: ");
                string password = Console.ReadLine();
                if (password == "Bästkusten")
                {
                    Console.WriteLine("Korrekt! Du angav rätt kod!");
                    Thread.Sleep(time);
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Fel kod! Försök igen...");
                    Thread.Sleep(time);
                    Console.Clear();
                } 
            } while (true);

            //2.Användaren skall få olika val presenterat i form av en meny.
            //a.Lista alla deltagare i gruppen separerat med ,
            //b.Få ut 10 generella detaljer om varje medlem.Tex favoritmat eller band.
            //i.Varje deltagare skall ha en unik sträng som beskriver personens 
            //största driv till programmering.
            //c.Möjligheten att ta bort en person.
            bool exit = false;
            do
            {
                Menu();
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Här är alla medlemmar i Bästkusten:");
                        ShowMembers();
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Vilken gruppmedlem vill du veta mer om?");
                        ShowMembers(1);
                        choice = UserChoice();
                        Console.Clear();
                        group[choice].Describe();
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Vilken gruppmedlem vill du ta bort?");
                        ShowMembers(1);
                        choice = UserChoice();
                        Console.WriteLine("{0} är nu borttagen.", group[choice].Name);
                        group.RemoveAt(choice);
                        Thread.Sleep(time);
                        break;
                    case 4:
                        Console.WriteLine("Programmet avslutas.");
                        exit = true;
                        Thread.Sleep(time);
                        break;
                    default:
                        Console.WriteLine("Du måste vålja mellan 1-4.");
                        Thread.Sleep(time);
                        break;
                }
            } while (!exit);


            //3.Programmet skall versionshanteras och måste innehålla minst tre commits.
            //4.Koden skall innehålla relevanta kommentarer

            //5.Tillsammans med projektet skall en rapport lämnas in. Denna skall 
            //innehålla en beskrivande text på hur ni tog ovan problem och bröt 
            //ner det till mindre delar.Zippa ihop allt och döp mappen till ert 
            //namn samt betygen ni siktar på för denna inläming.Tex RobinKamoVGUppgift2
            //6.För betyget VG; Redogör mer ingående om programmet.Ex
            //a.Varför valde du att göra som du gjorde?
            //b.Kunde du gjort det på något annat sätt?
            //c.För och nackdelarna med tillvägagångssättet?
            //d.Varför valde du en for istället för en foreach?
            //e.Hur tänkte du vid namngivning?
        }

        static void PopulateGroup(string name, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings)
        {
            var member = new Member(name, height, age, hobby, favoriteFood, 
                favoriteColor, motivation, homeTown, birthplace, siblings);
            group.Add(member);
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
            choice--;
            return choice;
        }
    }
}
