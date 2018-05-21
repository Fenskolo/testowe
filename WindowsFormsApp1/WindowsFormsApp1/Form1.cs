using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form2 f = new Form2();
            Task task = Task.Factory.StartNew(() =>
            {
                f.ShowDialog();
            });
            Class1 cl = new Class1(f);
        }
    }
}
