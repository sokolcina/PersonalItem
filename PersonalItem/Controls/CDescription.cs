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
    public partial class CDescription : UserControl,IVisibileEdit
    {
        public string Description { set { textBox1.Text = value; } get { return textBox1.Text; } }
        public IClass Owner { set; get; }
        public CDescription()
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
