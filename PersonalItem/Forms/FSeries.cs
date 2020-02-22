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
    public partial class FSeries : Form,IClass
    {
        public Guid ID { set; get; }
        public Guid IdUser { set; get; }
        public int SeriesId { get; set; }
        public string SeriesName { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public string Creators { set { textBox2.Text = value; } get { return textBox2.Text; } }
        public string Genre { set { textBox3.Text = value; } get { return textBox3.Text; } }
        public string SeasonWatched { set { textBox4.Text = value; } get { return textBox4.Text; } }
        public string Opinion { set { textBox5.Text = value; } get { return textBox5.Text; } }
        public string Description { set { textBox6.Text = value; } get { return textBox6.Text; } }
        public bool Watched { set { radioButton1.Checked = value; } get { return radioButton1.Checked; } }

        public bool Private { set { radioButton4.Checked = value; } get { return radioButton4.Checked; } }
        public FSeries()
        {
            InitializeComponent();
        }

        public Item GetInstance()
        {
            return new Series
            {
                Id = SeriesId,
                Name = SeriesName,
                Creators = Creators,
                Genre = Genre,
                Opinion = Opinion,
                Description = Description,
                SeasonWatched = Int32.Parse(SeasonWatched),
                Watched = Watched,
                Private = Private,
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

        private void FSeries_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.main.Show();
        }
    }
}
