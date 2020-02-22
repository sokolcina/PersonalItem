using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;


namespace PersonalItem
{
   
    public class User : Item
    {
        //properties
        #region
        [BsonId]
        public Guid ID { set; get; }
        public int Id { set; get; }
        public string Name { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Number { set; get; }
        public byte[] Picture { set; get; }

        public List<Guid> Friends { set; get; }
        #endregion
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                User u = obj as User;
                if(Main.sql)
                return Id == u.Id;
                else
                return  ID==u.ID;
            }
        } 
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public User ()
        {
           
        }

        public User(User u)
        {
            ID = u.ID;
            Id = u.Id;
            Name = u.Name;
            Number = u.Number;
            Email = u.Email;
            Picture = u.Picture;
            Username = u.Username;
            Password = u.Password;
            ID = u.ID;
            if (u.Friends!=null)
                Friends = new List<Guid>(u.Friends);
            else
                Friends = null;
        }

        public void Delete1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "delete from users where id_user=?id_user;";

            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();

            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?id_user", Id));
            cmd.ExecuteNonQuery();
            connection.Connection.Close();
        }

        public void Insert1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "insert into users (`Id_user`, `Name`, `Username`, `Email`, `Password`, `Picture`, `Number`) " +
                " values (null,?name,?username,?email,?password,?picture,?number)";
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?id",Id));
            cmd.Parameters.Add(new MySqlParameter("?name", Name));
            cmd.Parameters.Add(new MySqlParameter("?username", Username));
            cmd.Parameters.Add(new MySqlParameter("?email", Email));
            cmd.Parameters.Add(new MySqlParameter("?password", Password));
            cmd.Parameters.Add(new MySqlParameter("?number", Number));

            if (Picture != null)
            { 
                cmd.Parameters.Add("?picture", MySqlDbType.LongBlob);
                cmd.Parameters["?picture"].Value = Picture;
            }
            else
            {
                ImageConverter conv = new ImageConverter();
                byte[] bytes = (byte[])conv.ConvertTo(Properties.Resources.default_user, typeof(byte[]));
                cmd.Parameters.Add("?picture", MySqlDbType.LongBlob);
                cmd.Parameters["?picture"].Value = bytes;
            }

            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }

            cmd.ExecuteNonQuery();
            connection.Connection.Close();
        }

        public void Update1()
        {
            ConnectionMysql connection = Main.con as ConnectionMysql;
            string query = "update users set name=?name,username=?username,email=?email,password=?password, " +
                " picture=?picture,number=?number where id_user=?id;";
            if (connection.Connection.State != ConnectionState.Open)
            {
                connection.Connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection.Connection);
            cmd.Parameters.Add(new MySqlParameter("?id", Id));
            cmd.Parameters.Add(new MySqlParameter("?name", Name));
            cmd.Parameters.Add(new MySqlParameter("?username", Username));
            cmd.Parameters.Add(new MySqlParameter("?email", Email));
            cmd.Parameters.Add(new MySqlParameter("?password", Password));
            cmd.Parameters.Add(new MySqlParameter("?number", Number));
            cmd.Parameters.Add("?picture", MySqlDbType.LongBlob);
            cmd.Parameters["?picture"].Value = Picture;
          

            cmd.ExecuteNonQuery();

            connection.Connection.Close();
        }

        public void Insert2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<User>("User");
            if(Picture==null)
            {
                ImageConverter conv = new ImageConverter();
                byte[] bytes = (byte[])conv.ConvertTo(Properties.Resources.default_user, typeof(byte[]));
                Picture = bytes;
            }
            col.InsertOne(this);
        }

        public void Update2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<User>("User");
            var res = col.ReplaceOne(
                new BsonDocument("_id", ID),
                this,
                new UpdateOptions { IsUpsert = true });
        }

        public void Delete2()
        {
            ConnectionMongoDB connection = Main.con as ConnectionMongoDB;
            var col = connection.Database.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("Id", ID);
            col.DeleteOne(filter);
        }
    }

   

}
