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
        [DisplayName("Регистрационный номер книги")]
        public int id { get; set; }
        [DisplayName("Название книги")]
        public string name { get; set; }
        [DisplayName("Автор книги")]
        public string author { get; set; }
        [DisplayName("Жанр книги")]
        public string genre { get; set; }
    }
}
