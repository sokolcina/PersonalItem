using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalItem
{
    public class Movie : Item
    {
        //properties
        #region
        [BsonId]
        public Guid ID { set; get; }
        public int Id { get; set; }
        public string Name { set; get; }
        public string Writers { set; get; }
        public string Directors { set; get; }
        public string Genre { set; get; }
        public string Opinion { set; get; }
        public string Description { set; get; }
        public bool Watched { set; get; }
        public bool Private { set; get; }

        public Guid IdUser { set; get; }
        #endregion
        public void Delete1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "delete from users_movies where id_movie=?id_movie and id_user=?id_user;";

            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
               
            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?id_movie", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));
            cmd.ExecuteNonQuery();
            connection.Connection.Close();
        }

       

        public void Insert1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "select id_movie from movies where name like ?name;";
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?name", Name));
            cmd.Parameters.Add(new MySqlParameter("?writers", Writers));
            cmd.Parameters.Add(new MySqlParameter("?directors", Directors));
            cmd.Parameters.Add(new MySqlParameter("?genre", Genre));
            cmd.Parameters.Add(new MySqlParameter("?opinion", Opinion));
            cmd.Parameters.Add(new MySqlParameter("?description", Description));
            cmd.Parameters.Add(new MySqlParameter("?watched", Watched));
            cmd.Parameters.Add(new MySqlParameter("?private", Private));
            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                cmd.CommandText = "insert into movies values(null,?name,?directors,?writers,?genre);";
                reader.Close();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select id_movie from movies where name like ?name;";
                reader = cmd.ExecuteReader();
            }
            reader.Read();
            Id = (int)reader["id_movie"];
            reader.Close();
            cmd.Parameters.Add(new MySqlParameter("?id_movie", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));
            cmd.CommandText = "insert into users_movies values(?id_user,?id_movie,?opinion,?description,?watched,?private);";
            Console.WriteLine(cmd.ExecuteNonQuery());
            connection.Connection.Close();
        }

        public void Update1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "update movies set name=?name,author=?author,genre=?genre where id_movie=?id_movie;";
            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?name", Name));
            cmd.Parameters.Add(new MySqlParameter("?writers", Writers));
            cmd.Parameters.Add(new MySqlParameter("?directors", Directors));
            cmd.Parameters.Add(new MySqlParameter("?genre", Genre));
            cmd.Parameters.Add(new MySqlParameter("?opinion", Opinion));
            cmd.Parameters.Add(new MySqlParameter("?description", Description));
            cmd.Parameters.Add(new MySqlParameter("?watched", Watched));
            cmd.Parameters.Add(new MySqlParameter("?private", Private));
            cmd.Parameters.Add(new MySqlParameter("?id_book", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));


            cmd.ExecuteNonQuery();
            cmd.CommandText = "update users_movies set opinion=?opinion,description=?description,`watched`=?read,`private`=?private " +
                "where id_movie=?id_movie and id_user=?id_user;";
            cmd.ExecuteNonQuery();
            connection.Connection.Close();
        }


        public void Insert2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<Movie>("Movie");
            col.InsertOne(this);
        }

        public void Update2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<Movie>("Movie");
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
            var col = connection.Database.GetCollection<Movie>("Movie");
            var filter = Builders<Movie>.Filter.Eq("Id", ID);
            col.DeleteOne(filter);
        }
    }
}

