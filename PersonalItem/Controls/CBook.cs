using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalItem
{
    public partial class CBooks : UserControl,IClass,IVisibileEdit
    {
        // properties
        #region
        public Guid ID { set; get; }
        public Guid IdUser { set; get; }
        public int BookId { get; set; }
        public string BookName { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public string Author { set { textBox2.Text = value; } get { return textBox2.Text; } }
        public string Genre { set { textBox3.Text = value; } get { return textBox3.Text; } }
      
        public bool Read { set { radioButton1.Checked = value;  radioButton2.Checked = !value; } get { return radioButton1.Checked; } }

        public bool Private { set { radioButton3.Checked = value; radioButton4.Checked = !value; } get { return radioButton3.Checked; } }
       
        public COpinion Opinion { set; get; }
        public CDescription Description { set; get; }
        #endregion
        public CBooks()
        {
            InitializeComponent();
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Main.main.ClearPanel();
            Main.panel.Controls.Add(Opinion);
            List<UserControl> controls = new List<UserControl>();
            controls.Add(Opinion);
            Main.main.AddToStack(controls);
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Main.main.ClearPanel();
            Main.panel.Controls.Add(Description);
            List<UserControl> controls = new List<UserControl>();
            controls.Add(Description);
            Main.main.AddToStack(controls);
        }

       
        public Item GetInstance()
        {
            return new Book
            {
                Id = BookId,
                Name = BookName,
                Author = Author,
                Description = Description.Description,
                Opinion = Opinion.Opinion,
                Genre = Genre,
                Read = Read,
                Private = Private,
                ID = ID,
                IdUser=IdUser
            };
        }
        public void SetInstance(Item i)
        {
            Book b = i as Book;
            BookId = b.Id;
            BookName = b.Name;
            Author = b.Author;
            Description = new CDescription();
            Description.Owner = this;
            Description.Description = b.Description;
            Opinion = new COpinion();
            Opinion.Opinion = b.Opinion;
            Opinion.Owner = this;
            Genre = b.Genre;
            Read = b.Read;
            Private = b.Private;
            ID = b.ID;
            IdUser = b.IdUser;
        }

        public void SetVisibleEdit()
        {

            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            groupBox1.Enabled = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            label7.Visible = true;
            groupBox2.Visible = true;
            Description.SetVisibleEdit();
            Opinion.SetVisibleEdit();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Main.con.Update(this);
            Main.profile.UpdateBooks();
           
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
           
            Main.con.Delete(this);
            Main.profile.UpdateBooks();
            
        }
    }
}
