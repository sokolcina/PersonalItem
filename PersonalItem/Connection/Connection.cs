using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalItem
{
    public abstract class AbConnection
    {
        public AbConnection()
        {
        }

      

        public abstract List<User> GetUsers();

        public abstract List<User> GetUsers(string s);
       
        public abstract List<Book> GetBooks(User user);

        public abstract List<Movie> GetMovies(User user);

        public abstract List<Series> GetSeries(User user);
        public abstract List<Song> GetSongs(User user);

        public abstract List<User> GetFriends();

        public abstract User GetUser(IClass i);

        public abstract bool IsFriend(IClass i);

        public abstract void AddFriend(IClass i);
        public abstract void UnfriendUser(IClass i);

        public abstract bool Login();

        public abstract void Insert(IClass i);

        public abstract void Delete(IClass i);

        public abstract void Update(IClass i);

        public abstract void UpdatePicture(Image image);
      
    }
}
