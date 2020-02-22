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

    public partial class CMovie : UserControl,IClass,IVisibileEdit
    {
        //properties
        #region
        public Guid ID { set; get; }

        public Guid IdUser { set; get; }
        public int MovieId { get; set; }
        public string MovieName { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public string Directors { set { textBox2.Text = value; } get { return textBox2.Text; } }
        public string Writers { set { textBox3.Text = value; } get { return textBox3.Text; } }
        public string Genre { set { textBox4.Text = value; } get { return textBox4.Text; } }

        public bool Watched { set { radioButton1.Checked = value; radioButton2.Checked = !value; } get { return radioButton1.Checked; } }

        public bool Private { set { radioButton4.Checked = value; radioButton3.Checked = !value; } get { return radioButton4.Checked; } }

        public COpinion Opinion { set; get; }
        public CDescription Description { set; get; }
        #endregion
        public CMovie()
        {
            InitializeComponent();
        }

        public Item GetInstance()
        {
            return new Movie
            {
                Id = MovieId,
                Name = MovieName,
                Directors = Directors,
                Writers = Writers,
                Genre = Genre,
                Opinion = Opinion.Opinion,
                Description = Description.Description,
                Watched = Watched,
                Private = Private,
                ID=ID,
                IdUser=IdUser
            };
        }

        public void SetInstance(Item i)
        {
            Movie m = i as Movie;
            MovieId = m.Id;
            MovieName = m.Name;
            Directors = m.Directors;
            Description = new CDescription();
            Description.Owner = this;
            Description.Description = m.Description;
            Opinion = new COpinion();
            Opinion.Owner = this;
            Opinion.Opinion = m.Opinion;
            Genre = m.Genre;
            Watched = m.Watched;
            Private = m.Private;
            ID = m.ID;
            IdUser = m.IdUser;
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Main.main.ClearPanel();
            Main.panel.Controls.Add(Opinion);
            List<UserControl> controls = new List<UserControl>();
            controls.Add(Opinion);
            Main.main.AddToStack(controls);
        }

        private void Label7_Click(object sender, EventArgs e)
        {
            Main.main.ClearPanel();
            Main.panel.Controls.Add(Description);
            List<UserControl> controls = new List<UserControl>();
            controls.Add(Description);
            Main.main.AddToStack(controls);
        }
        public void SetVisibleEdit()
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            groupBox1.Enabled = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            label8.Visible = true;
            groupBox2.Visible = true;
            Description.SetVisibleEdit();
            Opinion.SetVisibleEdit();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Main.con.Update(this);
            Main.profile.UpdateMovies();

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

            Main.con.Delete(this);
            Main.profile.UpdateMovies();

        }
    }
}
