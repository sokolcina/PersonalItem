using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Created by Stefan Sokolovic 2019

namespace PersonalItem
{
    
    public partial class Main : Form
    {
        public static AbConnection con;
        public static Main main; // instance of main class
        public static User lUser; // logged in user
        public static User tmpUser; // temporary user
        public static bool lItems=true; // logged in user items
        public static FlowLayoutPanel panel; // main panel
        public static Stack<List<UserControl>> stack; // stack for user controls
        public static List<UserControl> tmp; // temporary user control
        public static CProfile profile; // temporary profile
        public static CSettings settings;
        public static List<UserControl> friends;
        public static bool sql = true; // which database mysql or mongoDB
       // public static bool sql = false; // if you want user mongoDB

        // testing
        // username: pera
        // pw: pera
        // or
        // username: nina
        // pw: nina
        public Main()
        {
            InitializeComponent();
            if(sql)
            con = new ConnectionMysql();
            else
            con = new ConnectionMongoDB();
            panel = flowLayoutPanel1;
            main = this;
            lUser = new User();
            tmpUser = new User();
            stack = new Stack<List<UserControl>>();
            profile = new CProfile();
            settings = new CSettings();
            friends = new List<UserControl>();
        }

        private void SearchUsers()
        {
            if (textBox1.Text != "")
            {
                lItems = false;
                List<User> users = con.GetUsers(textBox1.Text);
                List<UserControl> controls = new List<UserControl>();
                ClearPanel();
                if(users.Count>0)
                foreach (var u in users)
                {
                    CUser cUser = new CUser();
                    cUser.SetInstance(u);
                    controls.Add(cUser);
                    panel.Controls.Add(cUser);
                }
                else
                {
                    var d = new CDefault();
                    d.Default = "We didn't find any user with that name.";
                    controls.Add(d);
                    panel.Controls.Add(d);
                }
                AddToStack(controls);
            }
            textBox1.Text = "";
        }

        private void CreateStringCollection()
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            List<User> users = con.GetUsers();
            foreach (var u in users)
                autoComplete.Add(u.Name);
            textBox1.AutoCompleteCustomSource = autoComplete;
        }
        public void CreateProfile()
        {
            profile.SetInstance(lUser);
            profile.SetVisibleEdit();
            
            List<UserControl> c = new List<UserControl>();
            c.Add(profile);
            tmp = c;
            panel.Controls.Add(profile);
        }

        public void CreateFriends()
        {
            List<User> users = con.GetFriends();
            
            ClearPanel();
            if (users.Count > 0)
            {
                friends = new List<UserControl>();
                foreach (var u in users)
                {

                    CUser cUser = new CUser();
                    cUser.SetInstance(u);
                    friends.Add(cUser);
                    panel.Controls.Add(cUser);
                }
                AddToStack(friends);
            }
            else
            {
                List<UserControl> controls = new List<UserControl>();
                var d = new CDefault();
                d.Default = "You don't have friends. Please add some.";
                controls.Add(d);
                panel.Controls.Add(d);
                AddToStack(controls);
            }
            
            
        }

        public void ClearPanel()
        {
            panel.Controls.Clear();
        }
       
        public void AddToStack(List<UserControl> controls)
        {

            stack.Push(new List<UserControl>(tmp));
            tmp = controls;
        }
       
        public void RemoveFromStack()
        {
            if(stack.Count>0)
            { 
                panel.Controls.Clear();
                tmp=stack.Pop();
                Console.WriteLine(tmp[0].GetType());
               if(friends.Count > 0 && tmp[0].GetType().Equals(friends[0].GetType()))
                {
                    foreach (var f in friends)
                        panel.Controls.Add(f);
                } 
               else
                if(tmp.Count>1)
                {
                    foreach (var c in tmp)
                        panel.Controls.Add(c);
                }
                else
                {
                    panel.Controls.Add(tmp[0]);
                }
               
            }

        }
      
        //insert item
        private void Button1_Click(object sender, EventArgs e)
        {
            
            ClearPanel();
            CItems i = new CItems();
            List<UserControl> controls = new List<UserControl>();
            controls.Add(i);
            AddToStack(controls);
            panel.Controls.Add(i);
            
        }
        //search
        private void Button6_Click(object sender, EventArgs e)
        {
            SearchUsers();
        }

        //my items
        private void Button5_Click(object sender, EventArgs e)
        {
            lItems = true;
            CProfile cProfile = new CProfile();
            cProfile.SetInstance(lUser);
            cProfile.SetVisibleEdit();

            profile = cProfile;
 
            List<UserControl> controls = new List<UserControl>();
            controls.Add(profile);
            AddToStack(controls);
            ClearPanel();
            panel.Controls.Add(profile);
        }
        //friends
        private void Button2_Click(object sender, EventArgs e)
        {
            CreateFriends();
        }
        //settings
        private void Button3_Click(object sender, EventArgs e)
        {
           
            settings.SetInstance(lUser);
            List<UserControl> controls = new List<UserControl>();
            controls.Add(settings);
            AddToStack(controls);
            ClearPanel();
            panel.Controls.Add(settings);
        }
        //log out
        private void Button7_Click(object sender, EventArgs e)
        {
            //Close();
            Hide();
            FLogin.login.ResetMain();
            FLogin.login.Show();
        }
        //back
        private void Button4_Click(object sender, EventArgs e)
        {
            RemoveFromStack();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            FLogin.login.Close();
        }

        

        private void Main_Load(object sender, EventArgs e)
        {
            CreateStringCollection();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchUsers();
        }

       
    }
}
