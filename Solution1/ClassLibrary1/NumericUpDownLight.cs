using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public class NumericUpDownLight : Control
    {
        private int _value;
        
        private TextBox textBox = new TextBox();
        private Button buttonUp = new Button();
        private Button buttonDown = new Button();

        public NumericUpDownLight()
        {
            this.Width = 200;
            this.Height = 30;
            
            this._value = 0;

            this.buttonUp.Parent = this;
            this.buttonUp.Width = 30;
            this.buttonUp.Height = 15;
            this.buttonUp.Top = 1;
            this.buttonUp.Left = 169;
            this.buttonUp.Paint += new PaintEventHandler(this.buttonUp_Paint);
            this.buttonUp.Click += new EventHandler(this.IncrementValue);

            this.buttonDown.Parent = this;
            this.buttonDown.Width = 30;
            this.buttonDown.Height = 15;
            this.buttonDown.Top = 14;
            this.buttonDown.Left = 169;
            this.buttonDown.Paint += new PaintEventHandler(this.buttonDown_Paint);
            this.buttonDown.Click += new EventHandler(this.DecrementValue);

            this.textBox.Text = this._value.ToString();
            this.textBox.Font = new Font("Arial", 12, FontStyle.Regular);
            this.textBox.Width = 200;
            this.textBox.Height = 29;
            this.textBox.Multiline = true;
            this.textBox.ReadOnly = true;
            this.textBox.Parent = this;
            this.textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void buttonUp_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillPolygon(
                new SolidBrush(Color.Black), new Point[] 
                { 
                    new Point(11, 4), 
                    new Point(6, 9), 
                    new Point(16, 9) 
                }
            );
        }

        private void buttonDown_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillPolygon(
                new SolidBrush(Color.Black), new Point[]
                {
                    new Point(11, 6),
                    new Point(6, 1),
                    new Point(16, 1),
                }
            );
        }

        private void IncrementValue(object sender, EventArgs e)
        {
            this._value++;
            this.textBox.Text = this._value.ToString();
        }

        private void DecrementValue(object sender, EventArgs e)
        {
            this._value--;
            this.textBox.Text = this._value.ToString();
        }
    }
}
