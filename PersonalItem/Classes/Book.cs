using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MySql.Data.MySqlClient;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalItem
{
    public class Book : Item
    {
        // properties
        #region
        [BsonId]
        public Guid ID { get; set; }
        public int Id { get; set; }
        public string Name { set; get; }
        public string Author { set; get; }
        public string Genre { set; get; }
        public string Opinion { set; get; }
        public string Description { set; get; }
        public bool Read { set; get; }
        public bool Private { set; get; }

        public Guid IdUser { set; get; }
        #endregion
        public void Delete1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "delete from users_books where id_book=?id_book and id_user=?id_user;";
            
            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?id_book", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));
            cmd.ExecuteNonQuery();

            connection.Connection.Close();
        }

     

        public void Insert1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "select books.id_book from books where books.name like ?name;";
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?name",Name));
            cmd.Parameters.Add(new MySqlParameter("?author", Author));
            cmd.Parameters.Add(new MySqlParameter("?genre", Genre));
            cmd.Parameters.Add(new MySqlParameter("?opinion", Opinion));
            cmd.Parameters.Add(new MySqlParameter("?description", Description));
            cmd.Parameters.Add(new MySqlParameter("?read", Read));
            cmd.Parameters.Add(new MySqlParameter("?private", Private));
            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                cmd.CommandText = "insert into books values(null,?name,?author,?genre);";
                reader.Close();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select books.id_book from books where books.name like ?name;";
                reader = cmd.ExecuteReader();
            }
            reader.Read();
            Id = (int)reader["id_book"];
            reader.Close();
            cmd.Parameters.Add(new MySqlParameter("?id_book", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));
            cmd.CommandText = "insert into users_books values(?id_user,?id_book,?opinion,?description,?read,?private);";
            cmd.ExecuteNonQuery();

            connection.Connection.Close();
        }
        public void Update1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "update books set name=?name,author=?author,genre=?genre where id_book=?id_book;";
            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?name", Name));
            cmd.Parameters.Add(new MySqlParameter("?author", Author));
            cmd.Parameters.Add(new MySqlParameter("?genre", Genre));
            cmd.Parameters.Add(new MySqlParameter("?opinion", Opinion));
            cmd.Parameters.Add(new MySqlParameter("?description", Description));
            cmd.Parameters.Add(new MySqlParameter("?read", Read));
            cmd.Parameters.Add(new MySqlParameter("?private", Private));
            cmd.Parameters.Add(new MySqlParameter("?id_book", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));


            cmd.ExecuteNonQuery();
            cmd.CommandText = "update users_books set opinion=?opinion,description=?description,`read`=?read,`private`=?private " +
                "where id_book=?id_book and id_user=?id_user;";
            cmd.ExecuteNonQuery();

            connection.Connection.Close();
        }

        public void Insert2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<Book>("Book");
            col.InsertOne(this);
        }

        public void Update2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<Book>("Book");
            //var filter = Builders<Book>.Filter.Eq("Id", ID);
            //var update = Builders<Book>.Update.
            var res = col.ReplaceOne(
                new BsonDocument("_id", ID),
                this,
                new UpdateOptions { IsUpsert = true });
        }

        public void Delete2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<Book>("Book");
            var filter = Builders<Book>.Filter.Eq("Id", ID);
            col.DeleteOne(filter);
        }
    }

   }

