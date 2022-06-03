using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class MessageInfo
    {
        [DisplayName("ФИО читателя")]
        public String ReaderFIO { get; set; }
        [DisplayName("Дата рождения")]
        public String Date_of_birth { get; set; }
        [DisplayName("Эл.почта")]
        public String Email { get; set; }
        [DisplayName("Название книги")]
        public String Name { get; set; }
        [DisplayName("Автор книги")]
        public String AuthorFIO { get; set; }
        [DisplayName("Жанр книги")]
        public String Genre { get; set; }
        [DisplayName("Тип сообщения")]
        public String MessageType { get; set; }
        [DisplayName("Дата возврата")]
        public DateTime Return_date { get; set; }
        [DisplayName("Дата отправки")]
        public DateTime Departure_date { get; set; }

    }
}
