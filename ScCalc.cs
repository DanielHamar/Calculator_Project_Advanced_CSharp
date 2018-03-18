// Daniel Hamar August 6, 2017
// CIS317 Unit 2: Calculator Project

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_V2
{
    public partial class ScCalc : Form
    {
        decimal memory = 0; // used for memory buttons
        decimal workingMemory = 0; // used to stor numbers before operations are conducted on them
        string opr = ""; // operator string
        bool resetFlag = true; // checks if display needs cleared after number or decimal is hit
        bool allowDecPoint = true; // number can only contain 1 decimal

        public ScCalc()
        {
            InitializeComponent();

            // event handlers for number buttons
            btnZero.Click += new EventHandler(ButtonClickHandler);
            btnOne.Click += new EventHandler(ButtonClickHandler);
            btnTwo.Click += new EventHandler(ButtonClickHandler);
            btnThree.Click += new EventHandler(ButtonClickHandler);
            btnFour.Click += new EventHandler(ButtonClickHandler);
            btnFive.Click += new EventHandler(ButtonClickHandler);
            btnSix.Click += new EventHandler(ButtonClickHandler);
            btnSeven.Click += new EventHandler(ButtonClickHandler);
            btnEight.Click += new EventHandler(ButtonClickHandler);
            btnNine.Click += new EventHandler(ButtonClickHandler);

            // event handlers for non-number buttons
            btnBack.Click += new EventHandler(ButtonClickHandler);
            btnC.Click += new EventHandler(ButtonClickHandler);
            btnCE.Click += new EventHandler(ButtonClickHandler);
            btnDecimal.Click += new EventHandler(ButtonClickHandler);
            btnDivide.Click += new EventHandler(ButtonClickHandler);
            btnEquals.Click += new EventHandler(ButtonClickHandler);
            btnMC.Click += new EventHandler(ButtonClickHandler);
            btnMMinus.Click += new EventHandler(ButtonClickHandler);
            btnMPlus.Click += new EventHandler(ButtonClickHandler);
            btnMR.Click += new EventHandler(ButtonClickHandler);
            btnMS.Click += new EventHandler(ButtonClickHandler);
            btnMultiply.Click += new EventHandler(ButtonClickHandler);
            btnPercent.Click += new EventHandler(ButtonClickHandler);
            btnPlus.Click += new EventHandler(ButtonClickHandler);
            btnPlusMinus.Click += new EventHandler(ButtonClickHandler);
            btnReciprocal.Click += new EventHandler(ButtonClickHandler);
            btnSqrt.Click += new EventHandler(ButtonClickHandler);
            btnMinus.Click += new EventHandler(ButtonClickHandler);
        }
        
        private void ButtonClickHandler(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if ((char.IsDigit(btn.Text, 0) && btn.Text.Length == 1) || btn.Text == ".")
            {
                if (resetFlag)
                {
                    txtDisplay.Clear();
                    resetFlag = false;
                    allowDecPoint = true;
                }
                if (btn.Text == "." && allowDecPoint)
                {
                    txtDisplay.Text += btn.Text;
                    allowDecPoint = false;
                }
                else if (btn.Text != ".")
                {
                    txtDisplay.Text += btn.Text;
                }
            }
            // two number operations
            else if (btn.Text == "*" || btn.Text == "/" || btn.Text == "+" || btn.Text == "-")
            {
                // set the operator and the working memory and clear the display
                opr = btn.Text;
                workingMemory = decimal.Parse(txtDisplay.Text);
                txtDisplay.Clear();
                resetFlag = true;
            }
            else if (btn.Text == "=")
            {
                // set the display value to second value for arithmetic function
                decimal secondValue = decimal.Parse(txtDisplay.Text);
                // switch statement to do the math
                switch(opr)
                {
                    case "+":
                        {
                            txtDisplay.Text = (workingMemory + secondValue).ToString();
                            break;
                        }
                    case "-":
                        {
                            txtDisplay.Text = (workingMemory - secondValue).ToString();
                            break;
                        }
                    case "*":
                        {
                            txtDisplay.Text = (workingMemory * secondValue).ToString();
                            break;
                        }
                    case "/":
                        {
                            txtDisplay.Text = (workingMemory / secondValue).ToString();
                            break;
                        }
                }
                resetFlag = true;
            }
            // one number operations
            else if (btn.Text == "±")
            {
                // get the value of display, make it negative, and reset it
                txtDisplay.Text = (-decimal.Parse(txtDisplay.Text)).ToString();
            }
            else if (btn.Text == "√")
            {
                // use Math.Sqrt - returns double; convert to decimal and display
                decimal currVal = decimal.Parse(txtDisplay.Text);
                currVal = (decimal)Math.Sqrt((double)currVal);
                txtDisplay.Text = currVal.ToString();
                resetFlag = true;
            }
            else if (btn.Text == "%")
            {
                // take current value and divide by 100 for %
                decimal currVal = decimal.Parse(txtDisplay.Text);
                currVal = currVal / 100;
                txtDisplay.Text = currVal.ToString();
                resetFlag = true;
            }
            else if (btn.Text == "1/x")
            {
                // divide 1 by the current value
                decimal currVal = decimal.Parse(txtDisplay.Text);
                if (currVal != 0)
                {
                    currVal = 1 / currVal;
                    txtDisplay.Text = currVal.ToString();
                }
                else
                {
                    txtDisplay.Text = "0";
                }
                resetFlag = true;
            }
            else if (btn.Text == "←")
            {
                if (txtDisplay.TextLength > 0 && !resetFlag)
                {
                    // remove the last character from the display
                    txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.TextLength - 1);
                }
            }
            // Memory Operations
            else if (btn.Text == "MC")
            {
                // Clear the memory
                memory = 0;
                resetFlag = true;
            }
            else if (btn.Text == "MR")
            {
                // Recall the memory value to the display
                txtDisplay.Text = memory.ToString();
                resetFlag = true;
            }
            else if (btn.Text == "MS")
            {
                // remove the value from the display and enter it into memory
                memory = decimal.Parse(txtDisplay.Text);
                txtDisplay.Clear();
                resetFlag = true;
            }
            else if (btn.Text == "M+")
            {
                // add from the value stored in memory
                memory = memory + decimal.Parse(txtDisplay.Text);
                resetFlag = true;
            }
            else if (btn.Text == "M-")
            {
                // subtract from the value stored in memory
                memory = memory - decimal.Parse(txtDisplay.Text);
                resetFlag = true;
            }
            else if (btn.Text == "C")
            {
                // clear the working memory, the operation, and the display
                workingMemory = 0;
                opr = "";
                txtDisplay.Text = "0";
                resetFlag = true;
            }
            else if (btn.Text == "CE")
            {
                // clear the display only
                txtDisplay.Text = "0";
                resetFlag = true;
            }
        }
    }
}
