using System;

namespace DavidStrömVGUppgift2
{
    //Här är klassen Member som innehåller olika fält som varje gruppmedlem har.
    class Member
    {
        //Här är privata fält som hjälper till att beskriva varje gruppmedlem.
        private string firstName;
        private string lastName;
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
        public Member(string firstName, string lastName, int height, int age, string hobby, 
            string favoriteFood, string favoriteColor, string motivation, 
            string homeTown, string birthplace, int siblings, string gender)
        {
            this.firstName = firstName;
            this.lastName = lastName;
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
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Height { get => height; set => height = value; }
        public int Age { get => age; set => age = value; }
        public string Hobby { get => hobby; set => hobby = value; }
        public string FavoriteFood { get => favoriteFood; set => favoriteFood = value; }
        public string FavoriteColor { get => favoriteColor; set => favoriteColor = value; }
        public string Motivation { get => motivation; set => motivation = value; }
        public string HomeTown { get => homeTown; set => homeTown = value; }
        public string Birthplace { get => birthplace; set => birthplace = value; }
        public int Siblings { get => siblings; set => siblings = value; }
        public string Gender { get => gender; set => gender = value; }

        //Här är en metod som beskriver vem gruppmedlemmen är och skriver ut 
        //detta till skärmen.
        public void DescribeMember()
        {
            string pronoun = "";
            if (gender == "Man")
                pronoun = "han";
            else
                pronoun = "hon";

            Console.WriteLine($"\n\tDetta är {firstName} {lastName}.\n");
            Console.WriteLine($"\t{firstName} är {age} år gammal, {height} cm lång och " +
                $"{pronoun} har {siblings} syskon.");
            Console.WriteLine($"\tNär det kommer till favoriter har {firstName} {favoriteFood.ToLower()} " +
                $"som favoriträtt och favoritfärgen är {favoriteColor.ToLower()}.");
            Console.WriteLine($"\tMotivationen till programering för {firstName} är " +
                $"{motivation.ToLower()}.");
            if (homeTown.EndsWith('ö'))
                Console.WriteLine($"\t{firstName} föddes i {birthplace} och nu bor {pronoun} " +
                $"på {homeTown}.");
            else
                Console.WriteLine($"\t{firstName} föddes i {birthplace} och nu bor {pronoun} " +
                    $"i {homeTown}.");
            Console.WriteLine($"\tDet du kanske inte visste om {firstName} är att {pronoun} har" +
                $" {hobby.ToLower()} som hobby.");
            Console.Write("\n\tTryck på valfri tangent för att fortsätta...");
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}