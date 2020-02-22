using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalItem
{

    

    public partial class FLogin : Form,IClass
    {

        public static Main main;
        public static FLogin login;
        // properties
        #region
        public string LoginUser { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public string LoginPassword { set { textBox2.Text = value; } get { return textBox2.Text; } }

        public string CreateUsername { set { textBox3.Text = value; } get { return textBox3.Text; } }
        public string CreateName { set { textBox4.Text = value; } get { return textBox4.Text; } }
        public string CreatePassword { set { textBox5.Text = value; } get { return textBox5.Text; } }
        public string CreateEmail { set { textBox6.Text = value; } get { return textBox6.Text; } }
        public string CreateNumber { set { textBox7.Text = value; } get { return textBox7.Text; } }
        #endregion 
        public FLogin()
        {
            InitializeComponent(); 
            
        }
        private void ResetOwnProperties ()
        {
            LoginUser = "";
            LoginPassword = "";
            CreateName = "";
            CreateNumber = "";
            CreateEmail = "";
            CreatePassword = "";
            CreateUsername = "";
        }
        private void Login()
        {
            Main.lUser.Username = LoginUser;
            Main.lUser.Email = LoginUser;
            Main.lUser.Password = LoginPassword;
            if(Main.sql && LoginUser.Equals("admin") && LoginPassword.Equals("admin"))
            {
                var admin = new FAdmin();
                Hide();
                admin.Show();
            }
            else
            if (!Main.con.Login())
                MessageBox.Show("Invalid data.");
            else
            {
                Hide();
                main.Show();
               
            }
        }
        //log in
        private void Button5_Click(object sender, EventArgs e)
        {
            Login();
        }
       

        //create acc
        private void Button1_Click(object sender, EventArgs e)
        {
            if (CreateName != "" && CreatePassword != "" && CreateUsername != "" && CreateEmail != "")
            {
                if (!CreateEmail.Contains("@") && !CreateEmail.Contains("."))
                {
                    MessageBox.Show("Wrong e-mail.");
                    ResetOwnProperties();
                }
                else
                {
                    Main.con.Insert(this);
                    MessageBox.Show("You are created your account, now you can log in.");
                    ResetOwnProperties();
                }

            }
            else
            {
                MessageBox.Show("You must insert Username, Name, E-mail and Password.");
                ResetOwnProperties();
            }
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            main = new Main();
            login = this;
        }
        public void ResetMain()
        {
            main = new Main();
            ResetOwnProperties();
        }

        private void FLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();
        }

        public Item GetInstance()
        {
            return new User
            {
                Name = CreateName,
                Username = CreateUsername,
                Email = CreateEmail,
                Number = CreateNumber,
                Password =CreatePassword
            };
        }

        public void SetInstance(Item i)
        {
            throw new NotImplementedException();
        }
    }
}
