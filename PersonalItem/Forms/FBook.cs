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
    public partial class FBook : Form,IClass
    {
        public Guid ID { set; get; }

        public Guid IdUser { set; get; }
        public int BookId { get; set; }
        public string BookName { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public string Author { set { textBox2.Text = value; } get { return textBox2.Text; } }
        public string Genre { set { textBox3.Text = value; } get { return textBox3.Text; } }
        public string Description { set { textBox6.Text = value; } get { return textBox6.Text; } }
        public string Opinion { set { textBox5.Text = value; } get { return textBox5.Text; } }
        public bool Read { set { radioButton1.Checked = value; } get { return radioButton1.Checked; } }
        public bool Private { set { radioButton4.Checked = value; } get { return radioButton4.Checked; } }

       
        public FBook()
        {
            InitializeComponent();
        }

        public Item GetInstance()
        {
            return new Book
            {
                Id = BookId,
                Name = BookName,
                Author = Author,
                Description = Description,
                Opinion = Opinion,
                Genre = Genre,
                Read = Read,
                Private = Private,
                ID = ID,
                IdUser = Main.lUser.ID
            };
        }

        public void SetInstance(Item i)
        {
            throw new NotImplementedException();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Main.con.Insert(this);
            Close();
            Main.main.Show();
            
        }

        private void FBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.main.Show();
        }
    }
}
