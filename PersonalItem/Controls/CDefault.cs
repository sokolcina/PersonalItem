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
    public partial class CDefault : UserControl
    {
        public string Default { set { label1.Text=value; } get { return label1.Text; } }
        public CDefault()
        {
            InitializeComponent();
        }
    }
}
