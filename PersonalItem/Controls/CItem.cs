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
    public partial class CItems : UserControl
    {
        public CItems()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FBook fBook = new FBook();
            Main.main.Hide();
            fBook.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FSong fSong = new FSong();
            Main.main.Hide();
            fSong.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FMovie fMovie = new FMovie();
            Main.main.Hide();
            fMovie.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            FSeries fSeries = new FSeries();
            Main.main.Hide();
            fSeries.Show();
        }
    }
}
