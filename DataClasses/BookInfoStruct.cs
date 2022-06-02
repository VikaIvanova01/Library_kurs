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
        [DisplayName("Регистрационный номер книги")]
        public int idBook { get; set; }

        [DisplayName("Название книги")]
        public String Name { get; set; }
        [DisplayName("Автор книги")]
        public String AuthorFIO { get; set; }
        [DisplayName("Срок хранения")]
        public int Amount_of_days { get; set; }
        [DisplayName("Дата выдачи")]
        public DateTime Date_of_issue { get; set; }
        [DisplayName("Дата возврата")]
        public DateTime Return_date { get; set; }
    }
}
