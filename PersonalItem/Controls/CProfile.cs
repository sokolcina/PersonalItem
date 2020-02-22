using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PersonalItem
{
    public partial class CProfile : UserControl,IClass,IVisibileEdit
    {
        //properties
        #region
        public Guid ID { set; get; }

        public List<Guid> Friends { set; get; }
        public int UserId { get; set; }

        public string Username { set; get; }

        public string Password { set; get; }
        public string Email { set { label1.Text = value; } get { return label1.Text; } }
        public string UserName { set { label2.Text = value; } get { return label2.Text; } }
        public string Number { set { label3.Text = value; } get { return label3.Text; } }
        public Image Picture { set { pictureBox1.Image = value; } get { return pictureBox1.Image; } }
       
        public string AddFriend { set { button1.Text = value; } get { return button1.Text; } }
        #endregion

        public CProfile()
        {
            InitializeComponent();
        }
        
        
        public void SetVisibleEdit()
        {
            // login user
            if(Main.lUser.Equals(GetInstance()))
            {
                panel1.Visible = true;
                button1.Visible = false;
                label1.Visible = true;
                label3.Visible = true;
            }// friend
            else if (Main.con.IsFriend(this))
            {
                button1.Visible = true;
                AddFriend = "Unfriend";
                panel1.Visible = true;
                label1.Visible = true;
                label3.Visible = true;
            }
            else // not friend
            {
                panel1.Visible = false;
                label1.Visible = false;
                label3.Visible = false;
                button1.Visible = true;
                AddFriend = "Add friend";
            }
        }

        public Item GetInstance()
        {
            List<Guid> fr;
            if (Friends != null)
                fr = new List<Guid>(Friends);
            else fr = null;
            ImageConverter conv = new ImageConverter();
            byte[] bytes = (byte[])conv.ConvertTo(Picture, typeof(byte[]));
            return new User
            {
                Id = UserId,
                Name = UserName,
                Number = Number,
                Username = Username,
                Password = Password,
                Email = Email,
                Picture = bytes,
                ID = ID,
                Friends = fr,
            };
        }

        public void SetInstance(Item i)
        {
            User u = i as User;
            MemoryStream ms = new MemoryStream(u.Picture);
            UserId = u.Id;
            UserName = u.Name;
            Number = u.Number;
            Email = u.Email;
            Username = u.Username;
            Password = u.Password;
            Picture = Image.FromStream(ms);
            ID = u.ID;
            if(u.Friends!=null)
            Friends = new List<Guid>(u.Friends);
        }
        public void UpdateBooks()
        {
            List<UserControl> controls = new List<UserControl>();
            Main.main.ClearPanel();
            if (Main.lUser.Equals(this.GetInstance()))
            {
                List<Book> books = Main.con.GetBooks(Main.lUser);
                if(books.Count>0)
                foreach (Book b in books)
                {
                    CBooks cBooks = new CBooks();
                    cBooks.SetInstance(b);
                    cBooks.SetVisibleEdit();
                    controls.Add(cBooks);
                    Main.panel.Controls.Add(cBooks);
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "You don't have books.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
                Main.main.AddToStack(controls);
            }
            else
            {
                List<Book> books = Main.con.GetBooks(Main.tmpUser);
                if(books.Count>0)
                foreach (Book b in books)
                {
                    if (!b.Private)
                    {
                        CBooks cBooks = new CBooks();
                        cBooks.SetInstance(b);
                        controls.Add(cBooks);
                        Main.panel.Controls.Add(cBooks);
                    }
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "This user don't have books.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
                Main.main.AddToStack(controls);
            }
       
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            UpdateBooks();
        }

        public void UpdateSongs()
        {
            List<UserControl> controls = new List<UserControl>();
            Main.main.ClearPanel();
            if (Main.lUser.Equals(this.GetInstance()))
            {
                List<Song> songs = Main.con.GetSongs(Main.lUser);
                if(songs.Count>0)
                foreach (Song s in songs)
                {
                    CSong cSong = new CSong();
                    cSong.SetInstance(s);
                    cSong.SetVisibleEdit();
                    controls.Add(cSong);
                    Main.panel.Controls.Add(cSong);
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "You don't have songs.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
            }
            else
            {
                List<Song> songs = Main.con.GetSongs(Main.tmpUser);
               if(songs.Count>0)
                foreach (Song s in songs)
                {
                    if(!s.Private)
                    { 
                        CSong cSong = new CSong();
                        cSong.SetInstance(s);
                        controls.Add(cSong);
                        Main.panel.Controls.Add(cSong);
                    }
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "This user don't have songs.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
            }
            Main.main.AddToStack(controls);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            UpdateSongs();
        }

        public void UpdateMovies()
        {
            List<UserControl> controls = new List<UserControl>();
            Main.main.ClearPanel();
            if (Main.lUser.Equals(this.GetInstance()))
            {
                List<Movie> movies = Main.con.GetMovies(Main.lUser);
                if(movies.Count>0)
                foreach (Movie m in movies)
                {
                    CMovie cMovie = new CMovie();
                    cMovie.SetInstance(m);
                    cMovie.SetVisibleEdit();
                    controls.Add(cMovie);
                    Main.panel.Controls.Add(cMovie);
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "You don't have movies.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
            }
            else
            {
                List<Movie> movies = Main.con.GetMovies(Main.tmpUser);
                if(movies.Count>0)
                foreach (Movie m in movies)
                {
                    if(!m.Private)
                    {
                        CMovie cMovie = new CMovie();
                        cMovie.SetInstance(m);
                        controls.Add(cMovie);
                        Main.panel.Controls.Add(cMovie);
                    }
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "This user don't have movies.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
            }
            Main.main.AddToStack(controls);
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            UpdateMovies();
        }

        public void UpdateSeries()
        {
            List<UserControl> controls = new List<UserControl>();
            Main.main.ClearPanel();
            if (Main.lUser.Equals(this.GetInstance()))
            {
                List<Series> series = Main.con.GetSeries(Main.lUser);
                if(series.Count>0)
                foreach (Series s in series)
                {
                    CSeries cSeries = new CSeries();
                    cSeries.SetInstance(s);
                    cSeries.SetVisibleEdit();
                    controls.Add(cSeries);
                    Main.panel.Controls.Add(cSeries);
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "You don't have series.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
            }
            else
            {
                List<Series> series = Main.con.GetSeries(Main.tmpUser);
                if(series.Count>0)
                foreach (Series s in series)
                {
                    if (!s.Private)
                    { 
                        CSeries cSeries = new CSeries();
                        cSeries.SetInstance(s);
                        controls.Add(cSeries);
                        Main.panel.Controls.Add(cSeries);
                    }
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "This user don't have series.";
                    controls.Add(d);
                    Main.panel.Controls.Add(d);
                }
            }
            Main.main.AddToStack(controls);
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            UpdateSeries();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Main.con.IsFriend(this))
            {
                // delete friend
                Main.con.UnfriendUser(this);
                SetVisibleEdit();
                var friends = Main.friends;
                if (friends.Count>0) 
                for(int i=0;i<friends.Count;i++)
                {
                    CUser user = friends[i] as CUser;
                    if (user.GetInstance().Equals(GetInstance()))
                    {
                      Main.friends.Remove(user); 
                    }
                }
            }
            else // add friend
            {
                Main.con.AddFriend(this);
                CUser user = new CUser();
                user.SetInstance(GetInstance());
                Main.friends.Add(user);
              
                SetVisibleEdit();
            }
        }
    }
}
