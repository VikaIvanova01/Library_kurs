using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_kurs.Database
{
    public class DBClass
    {
        public static SqliteConnection conn;
        public static SqliteCommand cmd;
        public static SqliteDataReader reader;

        public static void connect()
        {
            string path = "Data Source=.\\DB\\library.db";
            conn = new SqliteConnection(path);
            conn.Open();
        }
        public static void close()
        {
            if (reader != null)
            {
                if (!reader.IsClosed)
                    reader.Close();

                if (!conn.State.ToString().Equals("Closed"))
                    conn.Close();
            }
        }

        public static List<DataClasses.Reader> GetReaders(bool withEmpty=false)
        {
            connect();

            cmd = new SqliteCommand(@"
SELECT  idReader, FIO
FROM Reader
ORDER BY FIO
", conn);

            var reader = cmd.ExecuteReader();
            var list = new List<DataClasses.Reader>();

            if (withEmpty)
            {
                list.Add(new DataClasses.Reader
                {
                    id = -1,
                    FIO = "---"
                });
            }

            while (reader.Read())
            {
                list.Add(new DataClasses.Reader
                {
                    id = int.Parse(reader.GetValue(0).ToString()),
                    FIO = reader.GetValue(1).ToString(),
                });
            }

            close();

            return list;
        }

        public static void ReturnBook(int readerId, int bookId)
        {
            connect();

            var now = DateTime.Now.ToString("dd.MM.yyyy");
            cmd = new SqliteCommand($@"
UPDATE Issue_Record
SET Type_record = 'закрыта', Return_date = '{now}'
WHERE Issue_Record.Book_idBook = {bookId} and Issue_Record.Reader_idReader = {readerId}
", conn);
            cmd.ExecuteNonQuery();

            close();
        }


        public static void GiveOutBook(int readerId, int bookId)
        {
            connect();

            var now = DateTime.Now.ToString("dd.MM.yyyy");
            var returnDate = DateTime.Now.AddDays(14).ToString("dd.MM.yyyy");

            cmd = new SqliteCommand($@"
INSERT INTO Issue_Record(Message_type_idMessage_type, Book_idBook, Reader_idReader, Date_of_issue, Return_date, Amount_of_days, Type_record)
VALUES(1, {bookId}, {readerId}, '{now}', '{returnDate}', 14, 'открыта')
", conn);
            cmd.ExecuteNonQuery();

            close();
        }

        public static List<DataClasses.Book> GetBooks(int readerId)
        {
            connect();
            cmd = new SqliteCommand($@"
SELECT Book.idBook, Book.Name, Author.FIO, Genre.Name_genre
FROM Book
LEFT JOIN Author ON Author.idAuthor = Book.Author_idAuthor
LEFT JOIN Genre  ON Genre.idGenre = Book.Genre_idGenre
WHERE Book.idBook not in (
	SELECT Issue_Record.Book_idBook 
	FROM Issue_Record 
	WHERE Issue_Record.Reader_idReader = {readerId} and Issue_Record.Type_record == 'открыта'
	)
", conn);
            var reader = cmd.ExecuteReader();
            var list = new List<DataClasses.Book>();

            while (reader.Read())
            {
                list.Add(new DataClasses.Book
                {
                    id = int.Parse(reader.GetValue(0).ToString()),
                    name = reader.GetValue(1).ToString(),
                    author = reader.GetValue(2).ToString(),
                    genre = reader.GetValue(3).ToString(),
                });
            }

            close();

            return list;
        }



        public static List<DataClasses.BookInfoStruct> GetBooksInfo(int readerId)
        {
            connect();
            cmd = new SqliteCommand($@"
SELECT Book.idBook, Name, Author.FIO, Amount_of_days,  Date_of_issue, Return_date 
FROM Issue_Record
LEFT JOIN Book ON  Book.idBook = Issue_Record.Book_idBook
LEFT JOIN Reader ON  Reader.idReader = Issue_Record.Reader_idReader
LEFT JOIN Author ON Author.idAuthor = Book.Author_idAuthor
WHERE Reader.idReader = {readerId} and Type_record = 'открыта'
", conn);
            var reader = cmd.ExecuteReader();
            var list = new List<DataClasses.BookInfoStruct>();

            while (reader.Read())
            {
                list.Add(new DataClasses.BookInfoStruct
                {
                    idBook = int.Parse(reader.GetValue(0).ToString()),
                    Name = reader.GetValue(1).ToString(),
                    AuthorFIO = reader.GetValue(2).ToString(),
                    Amount_of_days = int.Parse(reader.GetValue(3).ToString()),
                    Date_of_issue = DateTime.ParseExact(reader.GetValue(4).ToString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Return_date = DateTime.ParseExact(reader.GetValue(5).ToString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                });
            }

            close();

            return list;
        }

        public static void DepartureMessages()
        {
            connect();
            cmd = new SqliteCommand($@"
SELECT Issue_Record.idIssue_Record, Return_date
FROM Issue_Record
WHERE  Type_record = 'открыта'
", conn);

            var reader = cmd.ExecuteReader();
            var list = new List<DataClasses.IssueRecordInfo>();

            while (reader.Read())
            {
                list.Add(new DataClasses.IssueRecordInfo
                {
                    id = int.Parse(reader.GetValue(0).ToString()),
                    Return_date = DateTime.ParseExact(reader.GetValue(1).ToString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                });
            }

            var departureDate = DateTime.Now.ToString("dd.MM.yyyy");

            foreach(var issueRecord in list)
            {
                var days = (DateTime.Now - issueRecord.Return_date).TotalDays;
                var messageType = 0;
                if (days >= 7)
                    messageType = 4;
                else if (days >= 0)
                    messageType = 3;
                else if (days >= -3)
                    messageType = 2;

                if (messageType == 0)
                    continue;


                cmd = new SqliteCommand(@$"
SELECT count(*)
FROM Message
WHERE Issue_Record_idIssue_Record = {issueRecord.id} and Message_type_idMessage_type = {messageType}",
conn);
                if ((Int64)cmd.ExecuteScalar() > 0)
                    continue;

                cmd = new SqliteCommand($@"
INSERT INTO Message( Message_type_idMessage_type,  Issue_Record_idIssue_Record, Departure_date)
VALUES({messageType}, {issueRecord.id}, '{departureDate}')
", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<DataClasses.MessageInfo> GetMessageInfo(int reader_id=-1)
        {
            connect();
            cmd = new SqliteCommand($@"
SELECT Reader.FIO, Reader.Date_of_birth, Reader.Email, Book.Name, Author.FIO, Genre.Name_genre, Message_type.Mes_type, Departure_date, Return_date
FROM Message
LEFT JOIN Issue_Record ON Issue_Record.idIssue_Record = Message.Issue_Record_idIssue_Record
LEFT JOIN Book ON  Book.idBook = Issue_Record.Book_idBook
LEFT JOIN Reader ON  Reader.idReader = Issue_Record.Reader_idReader
LEFT JOIN Author ON Author.idAuthor = Book.Author_idAuthor
LEFT JOIN Genre ON Genre.idGenre = Book.Genre_idGenre
LEFT JOIN Message_type ON Message_type.idMessage_type=Message.Message_type_idMessage_type
WHERE ({reader_id} = -1 OR Issue_Record.Reader_idReader = {reader_id})
ORDER BY Message.Departure_date DESC
", conn);
            var reader = cmd.ExecuteReader();
            var list = new List<DataClasses.MessageInfo>();

            while (reader.Read())
            {
                list.Add(new DataClasses.MessageInfo
                {
                    ReaderFIO = reader.GetValue(0).ToString(),
                    Date_of_birth = reader.GetValue(1).ToString(),
                    Email = reader.GetValue(2).ToString(),
                    Name = reader.GetValue(3).ToString(),
                    AuthorFIO = reader.GetValue(4).ToString(),
                    Genre = reader.GetValue(5).ToString(),
                    MessageType = reader.GetValue(6).ToString(),
                    Departure_date = DateTime.ParseExact(reader.GetValue(7).ToString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Return_date = DateTime.ParseExact(reader.GetValue(8).ToString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                });
            }

            close();

            return list.OrderBy(x => x.Departure_date).Reverse().ToList();
        }
    }
}
