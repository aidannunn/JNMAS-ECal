namespace ECalProject
{
    using SpreadsheetEngine;
    public partial class Calculator : Form
    {
         public Calculator()
        {
            InitializeComponent();
            this.algebraicExpressionLabel.Visible = false;
            this.algebraicExpressionLabel.Enabled = false;
            // Didn't want to allow user input, liked the text box layout
            // this.outputTextBox.Enabled = false;
        }

        // Function takes numbers 1-9,0,. and adds them to the text box
        private void numericButton_click(object sender, EventArgs e)
        {
            this.outputTextBox.Text += (sender as Button).Text;
        }

        // Appends operation to value in text box to label (+,-,*,/)
        private void algebricButton_click(object sender, EventArgs e)
        {
            if(this.algebraicExpressionLabel.Text != "" && !algebraicExpressionLabel.Text.Contains("="))
            {
                // Calculate - value1 (operation) value2
                ExpressionTree calculate = new ExpressionTree(this.algebraicExpressionLabel.Text + this.outputTextBox.Text);
                // Output value back to label with operation appended
                this.algebraicExpressionLabel.Text = calculate.Evaluate().ToString() + (sender as Button).Text;
            }
            else
            {
                this.algebraicExpressionLabel.Visible = true;
                // Checks to make sure number is valid to use operation on
                if (outputTextBox.Text.Length != 0)
                    this.algebraicExpressionLabel.Text = this.outputTextBox.Text + (sender as Button).Text;
            }

            this.outputTextBox.Text = string.Empty;
        }
     
        // Deletes last item inserted
        private void delButton_Click(object sender, EventArgs e)
        {
            string editTxt = this.outputTextBox.Text;
            // If text box is not empty
            if (editTxt.Length != 0)
                this.outputTextBox.Text = editTxt.Remove(editTxt.Length - 1, 1);
        }

        // Calculates value
        private void equalsButton_Click(object sender, EventArgs e)
        {
            // Checks to make sure text is not empty
            if(this.outputTextBox.Text.Length != 0)
            {
                // Calculate - value1 (operation) value2
                ExpressionTree calculate = new ExpressionTree(this.algebraicExpressionLabel.Text + this.outputTextBox.Text);
                // Output value back to label with operation appended
                this.algebraicExpressionLabel.Text += outputTextBox.Text + (sender as Button).Text;
                this.outputTextBox.Text = calculate.Evaluate().ToString();
            }
        }

        // Clears the text box
        private void clearTextButton_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = string.Empty;
        }

        // Clears the text box and statement label
        private void clearAllButton_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = string.Empty;
            this.algebraicExpressionLabel.Visible = false;
            this.algebraicExpressionLabel.Text = "";
        }
    }
}
