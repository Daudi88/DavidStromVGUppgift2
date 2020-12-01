using System;
using System.Collections.Generic;
using System.Text;

namespace DavidStrömVGUppgift2
{
    class Member
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public string Hobby { get; set; }
        public string FavoriteFood { get; set; }
        public string FavoriteColor { get; set; }
        public string Motivation { get; set; }
        public string HomeTown { get; set; }
        public string Birthplace { get; set; }
        public int Siblings { get; set; }
        public Member(string name, int height, int age, string hobby,
            string favoriteFood, string favoriteColor, string motivation,
            string homeTown, string birthplace, int siblings)
        {
            Name = name;
            Height = height;
            Age = age;
            Hobby = hobby;
            FavoriteFood = favoriteFood;
            FavoriteColor = favoriteColor;
            Motivation = motivation;
            HomeTown = homeTown;
            Birthplace = birthplace;
            Siblings = siblings;
        }

        public void Describe()
        {
            Console.WriteLine("{0} är {1} år gammal, {2} cm lång och har {3} som hobby.", Name, Age, Height, Hobby);
            Console.WriteLine("{0}s favoritkäk är {1} och favoritfärgen är {2}.", Name, FavoriteFood, FavoriteColor);
            Console.WriteLine("{0}s motivation till programering är {1}.", Name, Motivation);
            Console.WriteLine("{0} föddes i {1} och bor nu i {2}", Name, Birthplace, HomeTown);
            Console.WriteLine("{0} har {1} syskon.", Name, Siblings);
        }
    }
}
