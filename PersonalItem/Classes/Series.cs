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
    public class Series : Item
    {
        // properties
        #region
        [BsonId]
        public Guid ID { set; get; }
        public int Id { get; set; }
        public string Name { set; get; }
        public string Creators { set; get; }
        public int SeasonWatched { set; get; }
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
            string query = "delete from users_series where id_series=?id_series and id_user=?id_user;";

            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?id_series", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));
           
            cmd.ExecuteNonQuery();
            connection.Connection.Close();
        }

        public void Insert1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "select id_series from series where name like ?name;";
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?name", Name));
            cmd.Parameters.Add(new MySqlParameter("?creators", Creators));
            cmd.Parameters.Add(new MySqlParameter("?seasonwatched", SeasonWatched));
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
                cmd.CommandText = "insert into series values(null,?name,?creators,?genre);";
                reader.Close();
                Console.WriteLine(cmd.ExecuteNonQuery());
                cmd.CommandText = "select id_series from series where name like ?name;";
                reader = cmd.ExecuteReader();
            }
            reader.Read();
            Id = (int)reader["id_series"];
            reader.Close();
            cmd.Parameters.Add(new MySqlParameter("?id_series", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));
            cmd.CommandText = "insert into users_series values(?id_user,?id_series,?opinion,?description,?watched,?seasonwatched,?private);";

            cmd.ExecuteNonQuery();
            connection.Connection.Close();
        }

        public void Update1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "update series set name=?name,creators=?creators,genre=?genre where id_series=?id_series;";
            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?name", Name));
            cmd.Parameters.Add(new MySqlParameter("?creators", Creators));
            cmd.Parameters.Add(new MySqlParameter("?seasonwatched", SeasonWatched));
            cmd.Parameters.Add(new MySqlParameter("?genre", Genre));
            cmd.Parameters.Add(new MySqlParameter("?opinion", Opinion));
            cmd.Parameters.Add(new MySqlParameter("?description", Description));
            cmd.Parameters.Add(new MySqlParameter("?watched", Watched));
            cmd.Parameters.Add(new MySqlParameter("?private", Private));
            cmd.Parameters.Add(new MySqlParameter("?id_series", Id));
            cmd.Parameters.Add(new MySqlParameter("?id_user", Main.lUser.Id));


            cmd.ExecuteNonQuery();
            cmd.CommandText = "update users_series set opinion=?opinion,description=?description,`watched`=?read,`private`=?private," +
                "season_watched=seasonwatched where id_series=?id_series and id_user=?id_user;";

            cmd.ExecuteNonQuery();
            connection.Connection.Close();
        }

        public void Insert2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<Series>("Series");
            col.InsertOne(this);
        }

        public void Update2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<Series>("Series");
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
            var col = connection.Database.GetCollection<Series>("Series");
            var filter = Builders<Series>.Filter.Eq("Id", ID);
            col.DeleteOne(filter);
        }
    }
}

