using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class Message
    {
        public int id;
        public DateTime date;
        public MessageType message_type;
        public Record record;

        public Message(int id, DateTime date, MessageType message_type, Record record)
        {
            this.id = id;
            this.date = date;
            this.message_type = message_type;
            this.record = record;
        }

        public Message()
        {

        }
    }
}
