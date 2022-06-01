using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class BookInfoStruct
    {
        [DisplayName("ИД книги")]
        public int idBook { get; set; }

        [DisplayName("Название книги")]
        public String Name { get; set; }
        [DisplayName("File Name")]
        public String AuthorFIO { get; set; }
        [DisplayName("File Name")]
        public int Amount_of_days { get; set; }
        [DisplayName("File Name")]
        public DateTime Date_of_issue { get; set; }
        [DisplayName("File Name")]
        public DateTime Return_date { get; set; }
    }
}
