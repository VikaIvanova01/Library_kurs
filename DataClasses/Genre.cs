using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class Genre
    {
        public int id;
        public string name_genre;

        public Genre(int id, string name_genre)
        {
            this.id = id;
            this.name_genre = name_genre;
        }

        public Genre()
        {

        }
    }
}
