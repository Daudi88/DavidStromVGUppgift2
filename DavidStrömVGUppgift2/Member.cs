using System;

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
        private string gender;

        //Här är en tom konstruktor (även om den inte används som programmet 
        //ser ut just nu kan det alltid vara bra att ha för framtiden).
        public Member()
        {

        }

        //Här är en konstruktor som låter oss skapa upp en ny gruppmedlem och 
        //tilldela ett värde till varje privat fält på en och samma gång.
        public Member(string name, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings, string gender)
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
            this.gender = gender;
        }

        //Här är en property för det privata fältet name som 
        //låter oss läsa gruppmedlemmens namn.
        public string Name { get => name; }

        //Här är en metod som beskriver vem gruppmedlemmen är och skriver ut 
        //detta till skärmen.
        public void Describe()
        {
            string pronoun = "";
            if (gender == "man")
                pronoun = "han";
            else
                pronoun = "hon";

            Console.WriteLine($"\n\tDetta är {name}.\n");
            Console.WriteLine($"\t{name} är {age} år gammal, {height} cm lång och " +
                $"{pronoun} har {hobby} som hobby.");
            Console.WriteLine($"\tNär det kommer till favoriter har {name} {favoriteFood} " +
                $"som favoriträtt och favoritfärgen är {favoriteColor}.");
            Console.WriteLine($"\tMotivationen till programering för {Name} är " +
                $"{motivation}.");
            if (homeTown.EndsWith('ö'))
                Console.WriteLine($"\t{name} föddes i {birthplace} och nu bor {pronoun} " +
                $"på {homeTown}.");
            else
                Console.WriteLine($"\t{name} föddes i {birthplace} och nu bor {pronoun} " +
                    $"i {homeTown}.");
            Console.WriteLine($"\tSista informationen om {name} är att {pronoun} har" +
                $" {siblings} syskon.");
            Console.Write("\n\tTryck på valfri tangent för att fortsätta...");
        }
    }
}