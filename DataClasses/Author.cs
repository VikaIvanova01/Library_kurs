using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class Author
    {
        public int id;
        public string FIO;

        public Author(int id, string FIO)
        {
            this.id = id;
            this.FIO = FIO;
        }

        public Author()
        {

        }
    }
}
