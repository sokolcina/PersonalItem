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
    public partial class FAdmin : Form
    {
        public FAdmin()
        {
            InitializeComponent();
        }

        private void FAdmin_Load(object sender, EventArgs e)
        {
            var con = Main.con as ConnectionMysql;
            dataGridView1.DataSource = con.GetActivity();
        }

        private void FAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            FLogin.login.Show();
        }
    }
}
