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
    public partial class COpinion : UserControl,IVisibileEdit
    {
        public string Opinion { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public IClass Owner { get; set; }
        public COpinion()
        {
            InitializeComponent();
        }

        public void SetVisibleEdit()
        {
            button1.Visible = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Main.con.Update(Owner);
            Main.main.RemoveFromStack();
        }
    }
}
