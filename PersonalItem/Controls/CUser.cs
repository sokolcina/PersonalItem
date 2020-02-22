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
    public partial class CUser : UserControl,IClass
    {
        // properties
        #region
        public Guid ID { set; get; }

        public List<Guid> Friends { set; get; }
        public int UserId { set; get; }

        public string Username { set; get; }
        public string Password { set; get; }
        public string UserName { get { return label4.Text; } set { label4.Text = value; } }
        public string Email { get { return label5.Text; } set { label5.Text = value; } }
        public string Number { get { return label6.Text; } set { label6.Text = value; } }

        public Image Picture { set { pictureBox1.Image = value; } get { return pictureBox1.Image; } }
        #endregion
        public CUser()
        {
            InitializeComponent();
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
                Email = Email,
                Number = Number,
                Picture = bytes,
                ID = ID,
                Friends = fr,
                Username = Username,
                Password = Password
            };
        }

        public void SetInstance(Item i)
        {
            
            User u = i as User;
            MemoryStream ms = new MemoryStream(u.Picture);
            UserId = u.Id;
            UserName = u.Name;
            Email = u.Email;
            Number = u.Number;
            Picture = Image.FromStream(ms);
            ID = u.ID;
            Password = u.Password;
            Username = u.Username;
            if (u.Friends != null)
                Friends = new List<Guid>(u.Friends);
            else Friends = null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Main.lItems = false;
            Main.tmpUser = Main.con.GetUser(this);
            CProfile cProfile = new CProfile();
            cProfile.SetInstance(Main.tmpUser);
            cProfile.SetVisibleEdit();

            Main.profile = cProfile;
            List<UserControl> controls = new List<UserControl>();
            controls.Add(cProfile);
            Main.main.AddToStack(controls);
            Main.main.ClearPanel();
            Main.panel.Controls.Add(Main.profile);
        }
    }
}
