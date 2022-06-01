using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.DataClasses
{
    public class MessageType
    {
        public int id;
        public string type;
        public string text;

        public MessageType(int id, string type, string text)
        {
            this.id = id;
            this.type = type;
            this.text = text;
        }

        public MessageType()
        {

        }
    }
}
