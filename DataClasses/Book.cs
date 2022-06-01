using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class Book
    {
        [DisplayName("ИД книги")]
        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
    }
}
