using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DavidStrömVGUppgift2
{
    class Print
    {
        //Här är en metod som skriver ut menyn till skärmen.
        public static void Menu()
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
            Console.WriteLine("\t2. Visa detaljer om en specifik gruppmedlem.");
            Console.WriteLine("\t3. Lägg till en person i gruppen.");
            Console.WriteLine("\t4. Ändra en person i gruppen.");
            Console.WriteLine("\t5. Ta bort en person ur gruppen.");
            Console.WriteLine("\t6. Avsluta programmet.");
            Console.Write("\n\tVad vill du göra? ");

        }

        //Här är en metod som skriver ut alla gruppmedlemmar till skärmen.
        public static void Members(List<Member> members)
        {
            Console.Clear();
            Console.WriteLine(@"         __  __          _ _");
            Console.WriteLine(@"        |  \/  |        | | |");
            Console.WriteLine(@"        | \  / | ___  __| | | ___ _ __ ___  _ __ ___   __ _ _ __");
            Console.WriteLine(@"        | |\/| |/ _ \/ _` | |/ _ \ '_ ` _ \| '_ ` _ \ / _` | '__|");
            Console.WriteLine(@"        | |  | |  __/ (_| | |  __/ | | | | | | | | | | (_| | |");
            Console.WriteLine(@"        |_|  |_|\___|\__,_|_|\___|_| |_| |_|_| |_| |_|\__,_|_|");
            Console.WriteLine();
            int ctr = 0;
            Console.Write("\t");

            //Här används en for-loop istället för en foreach för att kunna
            //kontrollera när &-teckenet ska skrivas ut.
            for (int i = 0; i < members.Count; i++)
            {
                Console.Write(members[i].ToString());

                //Här körs en if-sats för att se till att namnen skrivs ut på 
                //ett snyggt sätt och att ett &-tecken skrivs ut innan sista namnet.
                if (i + 2 == members.Count)
                    Console.Write(" & ");
                else if (i + 1 == members.Count)
                    Console.WriteLine(".");
                else
                    Console.Write(", ");
                ctr++;

                //Här bryter vi till en ny rad efter att halva gruppens
                //medlemmar har skrivits ut.
                if (ctr > 4)
                {
                    Console.Write("\n\t");
                    ctr = 0;
                }
            }
            Console.Write("\n\tTryck på valfri tangent för att återgå till huvudmenyn...");
            Console.ReadKey(true);
        }

        //Här är en metod som låter användaren få mer detaljer om en specifik
        //gruppmedlem.
        public static void Details(List<Member> members)
        {
            do
            {
                Console.Clear();
                Console.WriteLine(@"         _____       _        _ _ ");
                Console.WriteLine(@"        |  __ \     | |      | (_)");
                Console.WriteLine(@"        | |  | | ___| |_ __ _| |_  ___ _ __");
                Console.WriteLine(@"        | |  | |/ _ \ __/ _` | | |/ _ \ '__|");
                Console.WriteLine(@"        | |__| |  __/ || (_| | | |  __/ |");
                Console.WriteLine(@"        |_____/ \___|\__\__,_|_| |\___|_|");
                Console.WriteLine(@"                              _/ |");
                Console.WriteLine(@"                             |__/");
                ListOfMembers(members);
                Console.WriteLine("\n\tTryck bara Enter för att avbryta och återgå till huvudmenyn...");
                Console.Write("\tVilken gruppmedlem vill du veta mer om? ");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice > 0 && choice <= members.Count)
                {
                    TextInGreen($"\tDu vill veta mer om {members[choice - 1].ToString()}.");
                    Console.Clear();
                    members[choice - 1].Describe();
                    Console.ReadKey(true);
                }
                //Om användaren matar in bokstäver eller bara trycker Enter 
                //kommer UserChoice returnera 0 och då bryter vi oss ut ur loopen.
                else if (choice == 0)
                    break;
                //Om användaren matar in en siffra som ligger utanför ramen av 
                //listan skrivs ett felmeddelande ut
                else
                    Print.TextInRed($"\n\tDu måste ange en siffra mellan 1 och {members.Count}...");
            } while (true);
        }

        public static void ListOfMembers(List<Member> members)
        {
            int ctr = 1;
            foreach (var member in members)
                Console.WriteLine($"\t{ctr++}. {member.ToString()}");
        }

        //Här är en metod som skriver utt ett meddelande i grön text till skärmen.
        public static void TextInGreen(string message)
        {
            //Först sätts textfärgen till grön. Sedan skrivs meddelandet ut.
            //Efter det sätts textfärgen tillbaks till vit igen.
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
            Console.CursorVisible = true;
        }

        //Här är en metod som skriver utt ett meddelande i röd text till skärmen.
        public static void TextInRed(string message)
        {
            //Först sätts textfärgen till röd. Sedan skrivs meddelandet ut.
            //Efter det sätts textfärgen tillbaks till vit igen.
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);
            Console.CursorVisible = true;
        }
    }
}
