using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class Record
    {
        public int id;
        public DateTime date_issue;
        public DateTime date_return;
        public string type_record;
        public string days;
        public MessageType message_type;
        public Reader reader;
        public Book book;

        public Record(int id, DateTime date_issue, DateTime date_return, string type_record, string days, MessageType message_type, Reader reader, Book book)
        {
            this.id = id;
            this.date_issue = date_issue;
            this.date_return = date_return;
            this.type_record = type_record;
            this.days = days;
            this.message_type = message_type;
            this.reader = reader;
            this.book = book;
        }

        public Record()
        {

        }
    }
}
