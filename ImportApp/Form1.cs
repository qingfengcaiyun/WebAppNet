using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImportApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ImportCompany_Click(object sender, EventArgs e)
        {
            string filePath = SelectTextBox1.Text;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MemberLogic m = new MemberLogic();
            //DesignerLogic d = new DesignerLogic();
        }
    }
}
