using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PersonalItem
{
    public partial class CSettings : UserControl, IVisibileEdit, IClass
    {
        #region
        public Guid ID { get; set; }

        public List<Guid> Friends {get; set; }
        public int UserId { set; get; }
        public string UserName { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Username { get { return textBox2.Text; } set { textBox2.Text = value; } }

        public string Email { get { return textBox3.Text; } set { textBox3.Text = value; } }

        public string Password { get { return textBox4.Text; } set { textBox4.Text = value; } }
        public string Number { get { return textBox5.Text; } set { textBox5.Text = value; } }

        public Image Picture { set { pictureBox1.Image = value; } get { return pictureBox1.Image; } }
        #endregion
        public CSettings()
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
                Username = Username,
                Email = Email,
                Number = Number,
                Picture = bytes,
                Password = Password,
                ID = ID,
                Friends = fr
            };
        }

        public void SetInstance(Item i)
        {
            User u = i as User;
            MemoryStream ms = new MemoryStream(u.Picture); ;
            Picture = Image.FromStream(ms);
            UserId = u.Id;
            UserName = u.Name;
            Username = u.Username;
            Email = u.Email;
            Number = u.Number;
            Password = u.Password;
            ID = u.ID;
            if (u.Friends != null)
                Friends = new List<Guid>(u.Friends);
            else Friends = null;
        }
        

        public void SetVisibleEdit()
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {

                    Image s = Bitmap.FromFile(file);
                    Picture = s;
                    
                    Main.con.UpdatePicture(s);
                    MessageBox.Show("Picture is changed.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Now, you can change your data.");
            SetVisibleEdit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (UserName != "" && Password != "" && Username != "" && Email != "")
            {
                if (!Email.Contains("@") && !Email.Contains("."))
                {
                    MessageBox.Show("Wrong e-mail.");
                }
                else
                {
                    Main.con.Update(this);
                    Main.lUser = GetInstance() as User;
                    MessageBox.Show("You are successufully changed your data.");
                }

            }
            else
            {
                MessageBox.Show("You must insert Username, Name, E-mail and Password.");
            }
          
        }
    }
}
