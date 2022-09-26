using CalculatorEngine;

namespace ECalProject
{
    using CalculatorEngine;
    public partial class Calculator : Form
    {
        private int openPars;
        private MathCalculation mathCalc = new MathCalculation();
         public Calculator()
        {
            InitializeComponent();
            this.algebraicExpressionLabel.Visible = false;
            this.algebraicExpressionLabel.Enabled = false;

            //Doesn't allow user to use algebraic buttons until outputbox has values
            this.disableAlgebraicButtons();

            // will remain false until valid expression is inserted
            this.equalsButton.Enabled = false;

            // will remain false until an open there is an open
            this.closeParButton.Enabled = false;

        }

        // enables and disables buttons on a given circumstance
        private void enableEqualsButton()
        {
            if (openPars == 0)
            {
                this.equalsButton.Enabled = true;
            }
        }
        private void enableAlgebraicButtons()
        {
            this.divButton.Enabled = true;
            this.multButton.Enabled = true;
            this.addButton.Enabled = true;
            this.subButton.Enabled = true;
            // enables equals if symbol is not algebraic
            this.enableEqualsButton();
            // cannot use parthesis while a number is next to is
            this.openParButton.Enabled = false;
        }

        private void disableAlgebraicButtons()
        {
            this.divButton.Enabled = false;
            this.multButton.Enabled = false;
            this.addButton.Enabled = false;
            this.subButton.Enabled = false;
            // cannot equate while last symbol is algebraic
            this.equalsButton.Enabled = false;
            // can add parethesis if using symbol
            this.openParButton.Enabled = true;
        }

        private void disableAllNumbers()
        {
            this.button0.Enabled = false;
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button5.Enabled = false;
            this.button6.Enabled = false;
            this.button7.Enabled = false;
            this.button8.Enabled = false;
            this.button9.Enabled = false;
            this.periodButton.Enabled = false;
        }

        private void enableAllNumbers()
        {
            this.button0.Enabled = true;
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
            this.button5.Enabled = true;
            this.button6.Enabled = true;
            this.button7.Enabled = true;
            this.button8.Enabled = true;
            this.button9.Enabled = true;
            this.periodButton.Enabled = true;
        }



        // Function takes numbers 1-9,0,.,^ and adds them to the text box
        private void numericButton_click(object sender, EventArgs e)
        {
            this.outputTextBox.Text += (sender as Button).Text;
            this.enableAlgebraicButtons();
        }

        // Appends operation to value in text box to label (+,-,*,/)
        private void algebricButton_click(object sender, EventArgs e)
        {
            // if buttons were disabled, they will be enabled here
            this.enableAllNumbers();

            this.algebraicExpressionLabel.Visible = true;

            // if you want to use equated value in a different calculation
            if (this.algebraicExpressionLabel.Text.Contains("="))
            {
                this.algebraicExpressionLabel.Text = string.Empty;
            }

            // Adds symbol to value
            this.algebraicExpressionLabel.Text += this.outputTextBox.Text + (sender as Button).Text;
            

            // clears input box
            this.outputTextBox.Text = string.Empty;
            // disables buttons so that another symbol cannot be added next to it
            this.disableAlgebraicButtons();
        }

        // Deletes last item inserted
        private void delButton_Click(object sender, EventArgs e)
        {
            string editTxt = this.outputTextBox.Text;
            // If text box is not empty
            if (editTxt.Length != 0)
                this.outputTextBox.Text = editTxt.Remove(editTxt.Length - 1, 1);
        }

        // Calculates value - if there is a value in outputbox, it will clear that value with calculation
        private void equalsButton_Click(object sender, EventArgs e)
        {
            // enables numbers if they aren't already
            this.enableAllNumbers();

            string exp = this.algebraicExpressionLabel.Text;

            // Checks to make sure the equation hasn't already been ran
            if (!exp.Contains("="))
            {
                // Calculate - value1 (operation) value2
                ExpressionTree calculate = new ExpressionTree(exp + outputTextBox.Text);
                // Output value back to label with operation appended
                this.algebraicExpressionLabel.Text += outputTextBox.Text + "=";
                string value = calculate.Evaluate().ToString();

                // if value was negative
                if (value.Contains("-"))
                {
                    this.disableAllNumbers();
                    value = "0-" + value.Replace("-","");
                }
                this.outputTextBox.Text = value;
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

        private void openParButton_Click(object sender, EventArgs e)
        {
            this.algebraicExpressionLabel.Visible = true;

            // There are this many open parethensis open
            this.openPars++;

            // if there is a value in the box, it will add it
            if (outputTextBox.Text.Length != 0)
                this.algebraicExpressionLabel.Text += outputTextBox.Text;

            // Will enable the close button, and disable the equals until close parethesis is met
            this.closeParButton.Enabled = true;
            this.equalsButton.Enabled = false;

            this.algebraicExpressionLabel.Text += "(";
            this.outputTextBox.Text = string.Empty;

            // disables algebric numbers so you can't equate a open parethesis
            this.disableAlgebraicButtons();
        }

        private void closeParButton_Click(object sender, EventArgs e)
        {
            this.openPars--;

            // if there is a value in the box, it will add it
            if (outputTextBox.Text.Length != 0)
                this.algebraicExpressionLabel.Text += outputTextBox.Text;

            // Will enable equals button, disable close brackets
            if (openPars == 0)
            {
                this.closeParButton.Enabled = false;
                this.equalsButton.Enabled = true;
            }

            this.algebraicExpressionLabel.Text += ")";

            // clears outputbox in case there was something in there
            this.outputTextBox.Text = string.Empty;
            // disables all numbers so that the user is forced to select another operation to add to the end parethesis
            this.disableAllNumbers();
        }

        private void sqrt_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.squareRoot(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void ln_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.ln(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void log_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.log(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void sine_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.sine(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void cosine_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.cosine(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void tangent_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.tangent(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void radian_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.radian(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void factorial_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.factorial(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void percent_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.percent(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void eConstant_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.eConstant(Convert.ToDouble(this.outputTextBox.Text)));
        }

        private void inverse_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text = Convert.ToString(mathCalc.inverseFunction(Convert.ToDouble(this.outputTextBox.Text)));
        }
    }
}

//if (this.algebraicExpressionLabel.Text != "" && !algebraicExpressionLabel.Text.Contains("="))
//{
//    Calculate - value1(operation) value2
//   ExpressionTree calculate = new ExpressionTree(this.algebraicExpressionLabel.Text + this.outputTextBox.Text);
//    Output value back to label with operation appended
//    this.algebraicExpressionLabel.Text = calculate.Evaluate().ToString() + (sender as Button).Text;
//}
//else
//{
//    this.algebraicExpressionLabel.Visible = true;
//    Checks to make sure number is valid to use operation on
//    if (outputTextBox.Text.Length != 0)
//        this.algebraicExpressionLabel.Text = this.outputTextBox.Text + (sender as Button).Text;
//}