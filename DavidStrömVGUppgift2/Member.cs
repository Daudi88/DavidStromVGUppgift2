using System;
using System.Collections.Generic;
using System.Text;

namespace DavidStrömVGUppgift2
{
    //Här är klassen Member som innehåller olika fält som varje gruppmedlem har.
    class Member
    {
        //Här är privata fält som hjälper till att beskriva varje gruppmedlem.
        private string name;
        private int height;
        private int age;
        private string hobby;
        private string favoriteFood;
        private string favoriteColor;
        private string motivation;
        private string homeTown;
        private string birthplace;
        private int siblings;
        private string pronoun;

        //Här är en tom konstruktor (även om den inte används som programmet 
        //ser ut just nu kan det alltid vara bra att ha för framtiden).
        public Member()
        {

        }

        //Här är en konstruktor som låter oss skapa upp en ny gruppmedlem och 
        //tilldela ett värde till varje privat fält på en och samma gång.
        public Member(string name, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings, string pronoun)
        {
            this.name = name;
            this.height = height;
            this.age = age;
            this.hobby = hobby;
            this.favoriteFood = favoriteFood;
            this.favoriteColor = favoriteColor;
            this.motivation = motivation;
            this.homeTown = homeTown;
            this.birthplace = birthplace;
            this.siblings = siblings;
            this.pronoun = pronoun;
        }

        //Här är properties för de privata fälten name och favoriteColor som 
        //låter oss läsa gruppmedlemmens namn och favoritfärg.
        public string Name { get => name; }
        public string FavoriteColor { get => favoriteColor; }

        //Här är en metod som beskriver vem gruppmedlemmen är och skriver ut 
        //detta till skärmen.
        public void Describe()
        {
            Console.WriteLine($"Detta är {name}.");
            Console.WriteLine($"{name} är {age} år gammal, {height} cm lång och " +
                $"{pronoun} har {hobby} som hobby.");
            Console.WriteLine($"När det kommer till favoriter har {name} {favoriteFood} " +
                $"som favoriträtt och favoritfärgen är {favoriteColor}.");
            Console.WriteLine($"Motivationen till programering för {Name} är " +
                $"{motivation}.");
            if (homeTown.EndsWith('ö'))
                Console.WriteLine($"{name} föddes i {birthplace} och nu bor {pronoun} " +
                $"på {homeTown}.");
            else
                Console.WriteLine($"{name} föddes i {birthplace} och nu bor {pronoun} " +
                    $"i {homeTown}.");
            Console.WriteLine($"Sista informationen om {name} är att {pronoun} har" +
                $" {siblings} syskon.");
            Console.Write("\nTryck på valfri tangent för att fortsätta...");
        }
    }
}
