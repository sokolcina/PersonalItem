using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace PersonalItem
{
    public class ConnectionMysql : AbConnection
    {
        private string Server { set; get; }
        private string Database { set; get; }

        private string DbUser { set; get; }

        private string Password { set; get; }
        public MySqlConnection Connection { get; set; }
        public ConnectionMysql()
        {
            Server = "localhost";
            Database = "Base_0";
            DbUser = "root";
            Password = "123";
            Connection = new MySqlConnection("datasource=" + Server + ";database=" + Database + ";port=3308;username=" + DbUser + ";password=" + Password);
        }

       

        public override List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM USERS WHERE id_user != ?id";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?id", Main.lUser.Id));
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                User u = new User();
                u.Id = (int)reader["id_user"];
                u.Username = (string)reader["username"];
                u.Name = (string)reader["name"];
                u.Email = (string)reader["email"];
                u.Password = (string)reader["password"];
                users.Add(u);
                if (reader["picture"] != System.DBNull.Value)
                {
                    byte[] img = (byte[])reader["picture"];
                    u.Picture = img;
                }
              
            }
            reader.Close();
            Connection.Close();
                return users;
        }

        public override List<User> GetUsers(string s)
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM USERS WHERE id_user != ?id and name like ?name;";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?id", Main.lUser.Id));
            cmd.Parameters.Add(new MySqlParameter("?name", s));
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                User u = new User();
                u.Id = (int)reader["id_user"];
                u.Username = (string)reader["username"];
                u.Name = (string)reader["name"];
                u.Email = (string)reader["email"];
                u.Password = (string)reader["password"];
                users.Add(u);
                if (reader["picture"] != System.DBNull.Value)
                {
                    byte[] img = (byte[])reader["picture"];
                    u.Picture = img;
                }
              
            }
            reader.Close();
            Connection.Close();
            return users;
        }
       

        public override void Insert(IClass i)
        {
            Item item = i.GetInstance();
            item.Insert1();
        }

        public override void Delete(IClass i)
        {
            Item item = i.GetInstance();
            item.Delete1();
        }

        public override void Update(IClass i)
        {
            Item item = i.GetInstance();
            item.Update1();
        }

        public override List<Book> GetBooks(User user)
        {
            string query = "SELECT * FROM books b, users_books ub where ub.id_user=@id and ub.id_book=b.id_book";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("@id", user.Id));
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Book> books = new List<Book>();
            while (reader.Read())
            {
                Book b = new Book();
                b.Id = Int32.Parse(reader["id_book"].ToString());
                b.Name = reader["name"].ToString();
                b.Author = reader["author"].ToString();
                b.Genre = reader["genre"].ToString();
                b.Description = reader["description"].ToString();
                b.Opinion = reader["opinion"].ToString();
                b.Read = (bool)reader["read"];
                b.Private = (bool)reader["private"];
                books.Add(b);
            }
            Connection.Close();

            return books;
        }

        public override List<Movie> GetMovies(User user)
        {
            string query = "SELECT * FROM movies m, users_movies um where um.id_user=@id and um.id_movie=m.id_movie";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("@id", user.Id));
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Movie> movies = new List<Movie>();
            while (reader.Read())
            {
                Movie m = new Movie();
                m.Id = Int32.Parse(reader["id_movie"].ToString());
                m.Name = reader["name"].ToString();
                m.Directors = reader["directors"].ToString();
                m.Writers = reader["writers"].ToString();
                m.Genre = reader["genre"].ToString();
                m.Description = reader["description"].ToString();
                m.Opinion = reader["opinion"].ToString();
                m.Watched = (bool)reader["watched"];
                m.Private = (bool)reader["private"];
                movies.Add(m);
            }
            Connection.Close();

            return movies;
        }

        public override List<Series> GetSeries(User user)
        {
            string query = "SELECT * FROM series s, users_series us where us.id_user=@id and us.id_series=s.id_series";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("@id",user.Id));
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Series> series = new List<Series>();
            while (reader.Read())
            {
                Series s = new Series();
                s.Id = (int)reader["id_series"];
                s.Name = reader["name"].ToString();
                s.Creators = (string)reader["creators"];
                s.SeasonWatched = Int32.Parse(reader["season_watched"].ToString());
                s.Genre = reader["genre"].ToString();
                s.Description = reader["description"].ToString();
                s.Opinion = reader["opinion"].ToString();
                s.Watched = (bool)reader["watched"];
                s.Private = (bool)reader["private"];
                series.Add(s);
            }
            Connection.Close();

            return series;
        }

        public override List<Song> GetSongs(User user)
        {
            string query = "SELECT * FROM songs s, users_songs us where us.id_user=@id and us.id_song=s.id_song";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("@id", user.Id));
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Song> songs = new List<Song>();
            while (reader.Read())
            {
                Song s = new Song();
                s.Id = Int32.Parse(reader["id_song"].ToString());
                s.Name = reader["name"].ToString();
                s.Singer = reader["singer"].ToString();
                s.Genre = reader["genre"].ToString();
                s.Description = reader["description"].ToString();
                s.Opinion = reader["opinion"].ToString();
                s.Listened = (bool)reader["listened"];
                s.Private = (bool)reader["private"];
                songs.Add(s);
            }
            Connection.Close();

            return songs;
        }

        public override List<User> GetFriends()
        {
            string query = "(select u.id_user, u.name, u.email, u.picture, u.number from friendship f, users u where f.id_user1 = @id and u.id_user = f.id_user2 and f.status like 'f') union " +
                " (select u.id_user, u.name, u.email, u.picture, u.number from friendship f, users u where f.id_user2 = @id and u.id_user = f.id_user1 and f.status like 'f');";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("@id", Main.lUser.Id));
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                User u = new User();
                u.Id = Int32.Parse(reader["id_user"].ToString());
                u.Name = reader["name"].ToString();
                u.Email = reader["email"].ToString();
                u.Number = reader["number"].ToString();
                if (reader["picture"] != System.DBNull.Value)
                {
                    byte[] img = (byte[])reader["picture"];

                    /*
                    MemoryStream ms = new MemoryStream(img);
                    Image.FromStream(ms) */
                   u.Picture = img;
                }
               

                users.Add(u);
            }
            Connection.Close();

            return users;
        }

        public override void AddFriend(IClass i)
        {
            User user = i.GetInstance() as User;
            string query = "select * from friendship where (id_user1=?id1 and id_user2=?id2) or (id_user1=?id2 and id_user2=?id1) ;";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?id1", Main.lUser.Id));
            cmd.Parameters.Add(new MySqlParameter("?id2", user.Id));
            
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                cmd.CommandText = "insert into friendship values(?id1,?id2,'f');";
                reader.Close();
                cmd.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            cmd.CommandText = "update friendship set status = 'f' where (id_user1=?id1 and id_user2=?id2) or "
                    +" (id_user1=?id2 and id_user2=?id1)";
                cmd.ExecuteNonQuery();
            }
          
            Connection.Close();
        }

        public override void UnfriendUser(IClass i)
        {
            User user = i.GetInstance() as User;
            string query = "update friendship set status = 'u' where (id_user1=?id1 and id_user2=?id2) or "
                        + " (id_user1=?id2 and id_user2=?id1)";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?id1", Main.lUser.Id));
            cmd.Parameters.Add(new MySqlParameter("?id2", user.Id));

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        public override void UpdatePicture(Image image)
        {
            ImageConverter conv = new ImageConverter();
            byte[] bytes = (byte[])conv.ConvertTo(image, typeof(byte[]));
            string query = "update users set picture = ?picture where id_user = ?id";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?id", Main.lUser.Id));
            cmd.Parameters.Add("?picture", MySqlDbType.LongBlob);
            cmd.Parameters["?picture"].Value = bytes;

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            Main.lUser.Picture = bytes;
            Main.profile.SetInstance(Main.lUser);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public override bool Login()
        {
            string query = "select * from users where (email=?email or username=?username) and password=?password";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?email", Main.lUser.Email));
            cmd.Parameters.Add(new MySqlParameter("?username", Main.lUser.Username));
            cmd.Parameters.Add(new MySqlParameter("?password", Main.lUser.Password));

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Main.lUser.Id = Int32.Parse(reader["id_user"].ToString());
                Main.lUser.Name = reader["name"].ToString();
                Main.lUser.Email = reader["email"].ToString();
                Main.lUser.Number = reader["number"].ToString();
                if (reader["picture"] != System.DBNull.Value)
                {
                    byte[] img = (byte[])reader["picture"];
                    Main.lUser.Picture =img;
                }
                

               
                reader.Close();
                Connection.Close();
                if (Main.lUser.Picture == null)
                    UpdatePicture(Properties.Resources.default_user);
                Main.main.CreateProfile();
                return true;
            }
            else
            {
                Connection.Close();
                return false;
            }
            
        }

        

        public override User GetUser(IClass i)
        {
            User user = i.GetInstance() as User;
            string query = "SELECT * FROM USERS where id_user= ?id";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?id", user.Id));
            User u = new User();
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            u.Id = (int)reader["id_user"];
            u.Username = reader["username"].ToString();
            u.Name = reader["name"].ToString();
            u.Email = reader["email"].ToString();
            u.Password = reader["password"].ToString();
            u.Number = reader["number"].ToString();
            if (reader["picture"] != System.DBNull.Value)
            {
                byte[] img = (byte[])reader["picture"];
                u.Picture = img;
            }
            reader.Close();
            Connection.Close();
            return u;
        }

        public override bool IsFriend(IClass i)
        {
            User user = i.GetInstance() as User;
            string query = "select * from friendship where ((id_user1=?id1 and id_user2=?id2) or (id_user1=?id2 and id_user2=?id1)) and" +
                " status like 'f' ;";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.Parameters.Add(new MySqlParameter("?id1", Main.lUser.Id));
            cmd.Parameters.Add(new MySqlParameter("?id2", user.Id));

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)

            {
                reader.Close();
                Connection.Close();
                return true;
            }
            else
            {
                reader.Close();
                Connection.Close();
                return false;
            }
        }

        public DataTable GetActivity ()
        {
            string query = "select u.Name, a.Activity, a.Books, a.Movies, a.Series, a.Songs from users u, users_activity a where u.id_user=a.id_user ";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(reader);
            reader.Close();
            Connection.Close();
            return data;
        }
      
    }
}
