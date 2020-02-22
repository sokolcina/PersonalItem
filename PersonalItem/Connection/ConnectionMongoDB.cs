using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace PersonalItem
{
    class ConnectionMongoDB : AbConnection
    {
        public IMongoDatabase Database;
        private IMongoClient client;
        public ConnectionMongoDB()
        {
            client = new MongoClient();
            Database = client.GetDatabase("Base_1");
        }

        public override void Insert(IClass i)
        {
            Item item = i.GetInstance();
            item.Insert2();
        }

        public override void Delete(IClass i)
        {
            Item item = i.GetInstance();
            item.Delete2();
        }

        public override void Update(IClass i)
        {
            Item item = i.GetInstance();
            item.Update2();
        }
        public override void AddFriend(IClass i)
        {
            User u1 = Main.lUser;
            User u2 = i.GetInstance() as User;
            var col = Database.GetCollection<User>("User");
            if (u1.Friends == null)
                u1.Friends = new List<Guid>();
            else
                u1.Friends.Add(u2.ID);
            BsonArray array = new BsonArray();
            foreach (var u in u1.Friends)
                array.Add(u);
            UpdateDefinition<User> update = Builders<User>.Update.Set("Friends", array);
            var res = col.UpdateOne(u => u.ID.Equals(u1.ID), update);
      
            if (u2.Friends == null)
                u2.Friends = new List<Guid>();
            else
                u2.Friends.Add(u1.ID);
            array.Clear();
            foreach (var u in u1.Friends)
                array.Add(u);
            update = Builders<User>.Update.Set("Friends", array);
            res = col.UpdateOne(u => u.ID.Equals(u2.ID), update);

        }

       

        public override List<Book> GetBooks(User user)
        {
            var col = Database.GetCollection<Book>("Book");
            var builder = Builders<Book>.Filter;
            var filter = builder.Eq("IdUser", user.ID);
            return col.Find(filter).ToList(); ;
            
        }

        public override List<User> GetFriends()
        {
            List<User> users = new List<User>();
            var col = Database.GetCollection<User>("User");
            foreach (var id in Main.lUser.Friends)
                users.Add(col.Find(u => u.ID.Equals(id)).First());
            return users;
        }

        public override List<Movie> GetMovies(User user)
        {
            var col = Database.GetCollection<Movie>("Movie");
            var builder = Builders<Movie>.Filter;
            var filter = builder.Eq("IdUser", user.ID);
            return col.Find(filter).ToList(); ;
        }

        public override List<Series> GetSeries(User user)
        {
            var col = Database.GetCollection<Series>("Series");
            var builder = Builders<Series>.Filter;
            var filter = builder.Eq("IdUser", user.ID);
            return col.Find(filter).ToList(); ;
        }

        public override List<Song> GetSongs(User user)
        {
            var col = Database.GetCollection<Song>("Song");
            var builder = Builders<Song>.Filter;
            var filter = builder.Eq("IdUser", user.ID);
            return col.Find(filter).ToList(); ;
        }

        public override User GetUser(IClass i)
        {
            User user = i.GetInstance() as User;
            var col = Database.GetCollection<User>("User");
            return col.Find(u => u.ID == user.ID).First();
           
        }

        public override List<User> GetUsers()
        {
            User u = Main.lUser;
            var col = Database.GetCollection<User>("User");
            var builder = Builders<User>.Filter;
            var filter = builder.Ne("_id", u.ID);
            var res = col.Find(filter);
            return res.ToList();
        }

        public override List<User> GetUsers(string s)
        {
            User u = Main.lUser;
            var col = Database.GetCollection<User>("User");
            var builder = Builders<User>.Filter;
            var filter = builder.And(builder.Ne("_id", u.ID),builder.Eq("Name",s));
            var res = col.Find(filter);
            return res.ToList();
        }

      

        public override bool IsFriend(IClass i)
        {
            User u1 = Main.lUser;
            User u2 = i.GetInstance() as User;
            if (u1.Friends != null)
                return u1.Friends.Contains(u2.ID);
            else return false;
        }

        public override bool Login()
        {
            User u = Main.lUser;
            var col = Database.GetCollection<User>("User");
            var builder = Builders<User>.Filter;
            var filter = builder.And(builder.Eq("Password", u.Password), 
                builder.Or(builder.Eq("Username", u.Username), builder.Eq("Email",u.Email)));
           
            var res = col.Find(filter);
            if (res.CountDocuments()>0)
            {
                User user=res.First();
                Main.lUser = new User(user);
                if (Main.lUser.Picture == null)
                    UpdatePicture(Properties.Resources.default_user);
                Main.main.CreateProfile();
                return true;
            }
            else return false;
        }

        public override void UnfriendUser(IClass i)
        {
            User u1 = Main.lUser;
            User u2 = i.GetInstance() as User;
            var col = Database.GetCollection<User>("User");
            if (u1.Friends.Count>0)
                u1.Friends.Remove(u2.ID);
            BsonArray array = new BsonArray();
            foreach (var u in u1.Friends)
                array.Add(u);
            UpdateDefinition<User> update = Builders<User>.Update.Set("Friends", array);
            var res = col.UpdateOne(u => u.ID.Equals(u1.ID), update);

            if (u2.Friends.Count>0)
                u2.Friends.Remove(u1.ID);
            array.Clear();
            foreach (var u in u1.Friends)
                array.Add(u);
            update = Builders<User>.Update.Set("Friends", array);
            res = col.UpdateOne(u => u.ID.Equals(u2.ID), update);
        }

        public override void UpdatePicture(Image image)
        {
            ImageConverter conv = new ImageConverter();
            byte[] bytes = (byte[])conv.ConvertTo(image, typeof(byte[]));
            Main.lUser.Picture = bytes;
            Main.lUser.Update2();
        }
    }
}
