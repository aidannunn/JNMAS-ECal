using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExpressionTreeApp;


namespace ECal_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void algebraicExpressionLabel_Click(object sender, EventArgs e)
        {

        }

        private void numericButton_click(object sender, EventArgs e)
        {
            outputTextBox.Text += (sender as Button).Text;
        }

        private void addButton_Click(object sender, EventArgs e)
        {

        }

        private void subButton_Click(object sender, EventArgs e)
        {

        }

        private void multButton_Click(object sender, EventArgs e)
        {

        }

        private void divButton_Click(object sender, EventArgs e)
        {

        }

        private void delButton_Click(object sender, EventArgs e)
        {
            string editTxt = outputTextBox.Text;
            // If text box is not empty
            if(editTxt.Length != 0)
                outputTextBox.Text = editTxt.Remove(editTxt.Length-1, 1);
        }
    }
}
