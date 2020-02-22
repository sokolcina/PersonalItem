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
    public partial class CSong : UserControl,IClass,IVisibileEdit
    {
        //properties
        #region
        public Guid ID { set; get; }
        public Guid IdUser { set; get; }
        public int SongId { get; set; }
        public string SongName { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public string Singer { set { textBox2.Text = value; } get { return textBox2.Text; } }
        public string Genre { set { textBox3.Text = value; } get { return textBox3.Text; } }

        public bool Listened { set { radioButton1.Checked = value; radioButton2.Checked = !value; } get { return radioButton1.Checked; } }

        public bool Private { set { radioButton4.Checked = value; radioButton3.Checked = !value; } get { return radioButton4.Checked; } }

        public COpinion Opinion { set; get; }
        public CDescription Description { set; get; }
        #endregion
        public CSong()
        {
            InitializeComponent();
        }

        public Item GetInstance()
        {
            return new Song
            {
                Id = SongId,
                Name = SongName,
                Singer = Singer,
                Description = Description.Description,
                Opinion = Opinion.Opinion,
                Genre = Genre,
                Listened = Listened,
                Private = Private,
                ID = ID,
                IdUser = IdUser
            };
        }
        public void SetInstance(Item i)
        {
            Song s = i as Song;
            SongId = s.Id;
            SongName = s.Name;
            Singer = s.Singer;
            Description = new CDescription();
            Description.Owner = this;
            Description.Description = s.Description;
            Opinion = new COpinion();
            Opinion.Owner = this;
            Opinion.Opinion = s.Opinion;
            Genre = s.Genre;
            Listened = s.Listened;
            Private = s.Private;
            ID = s.ID;
            IdUser = s.IdUser;
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
            Main.profile.UpdateSongs();
           
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Main.con.Delete(this);
            Main.profile.UpdateSongs();
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

  
    }
}
