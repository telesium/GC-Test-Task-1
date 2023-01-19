﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public class NumericUpDownLight : Control
    {
        private int _value = 0;
        private EventHandler _valueChanged = null;
        private HorizontalAlignment _textAlign = HorizontalAlignment.Left;
        private Color _mouseHoverBackColor = Color.Blue;
        
        private TextBox textBox = new TextBox();
        private Button buttonUp = new Button();
        private Button buttonDown = new Button();

        [Browsable(false)]
        public override string Text { get; set; }

        [
        Category("Data"),
        Description("Specifies the value of the control.")
        ]
        public int Value
        {

            get
            {
                return _value;
            }
            set
            {
                _value = value;
                this.textBox.Text = this._value.ToString();
            }
        }

        [
        Category("Property Changed"),
        
        
        Description("ValueChanged event triggers after the value was changed")
        ]
        public event EventHandler ValueChanged
        {

            add
            {
                _valueChanged = value;
            }
            remove
            {
                _valueChanged = null;
            }
        }

        [
        Category("Appearance"),
        Description("Specifies the alignment of the text")
        ]
        public HorizontalAlignment TextAlign
        {

            get
            {
                return _textAlign;
            }
            set
            {
                _textAlign = value;
                this.textBox.TextAlign = _textAlign;
            }
        }

        [
        Category("Appearance"),
        Description("Specifies the mouse hover back color of the buttons")
        ]
        public Color MouseHoverBackColor
        {

            get
            {
                return _mouseHoverBackColor;
            }
            set
            {
                _mouseHoverBackColor = value;
                this.buttonUp.FlatAppearance.MouseOverBackColor = _mouseHoverBackColor;
                this.buttonDown.FlatAppearance.MouseOverBackColor = _mouseHoverBackColor;
            }
        }

        public NumericUpDownLight()
        {
            this.Width = 200;
            this.Height = 30;
            
            this.buttonUp.Parent = this;
            this.buttonUp.Width = 30;
            this.buttonUp.Height = 15;
            this.buttonUp.Top = 1;
            this.buttonUp.Left = 169;
            this.buttonUp.Paint += new PaintEventHandler(this.buttonUp_Paint);
            this.buttonUp.Click += new EventHandler(this.IncrementValue);
            this.buttonUp.FlatStyle = FlatStyle.Flat;
            this.buttonUp.FlatAppearance.BorderColor = Color.Gray;
            this.buttonUp.FlatAppearance.BorderSize = 1;
            this.buttonUp.FlatAppearance.MouseOverBackColor = _mouseHoverBackColor;


            this.buttonDown.Parent = this;
            this.buttonDown.Width = 30;
            this.buttonDown.Height = 15;
            this.buttonDown.Top = 14;
            this.buttonDown.Left = 169;
            this.buttonDown.Paint += new PaintEventHandler(this.buttonDown_Paint);
            this.buttonDown.Click += new EventHandler(this.DecrementValue);
            this.buttonDown.FlatStyle = FlatStyle.Flat;
            this.buttonDown.FlatAppearance.BorderColor = Color.Gray;
            this.buttonDown.FlatAppearance.BorderSize = 1;
            this.buttonDown.FlatAppearance.MouseOverBackColor = _mouseHoverBackColor;

            this.textBox.Text = this._value.ToString();
            this.textBox.Font = new Font("Arial", 12, FontStyle.Regular);
            this.textBox.TextAlign = _textAlign;
            this.textBox.Width = 170;
            this.textBox.Height = 29;
            this.textBox.Multiline = true;
            this.textBox.ReadOnly = true;
            this.textBox.Parent = this;
            this.textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(
                new Pen(Color.Gray, 2),
                ClientRectangle);
        }

        private void buttonUp_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillPolygon(
                new SolidBrush(Color.Black), new Point[] 
                { 
                    new Point(11, 3), 
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
                    new Point(11, 9),
                    new Point(6, 4),
                    new Point(16, 4),
                }
            );
        }
        private void buttonUp_MouseHover(object sender, EventArgs e)
        {
            this.buttonUp.FlatAppearance.BorderColor = Color.Red;
            this.buttonUp.FlatAppearance.BorderSize = 2;
        }

        private void buttonUp_MouseLeave(object sender, EventArgs e)
        {
            this.buttonUp.FlatAppearance.BorderColor = Color.White;
            this.buttonUp.FlatAppearance.BorderSize = 2;
        }

        private void IncrementValue(object sender, EventArgs e)
        {
            this._value++;
            this.textBox.Text = this._value.ToString();

            if (_valueChanged != null)
            {
                _valueChanged(this, EventArgs.Empty);
            }
        }

        private void DecrementValue(object sender, EventArgs e)
        {
            this._value--;
            this.textBox.Text = this._value.ToString();

            if (_valueChanged != null)
            {
                _valueChanged(this, EventArgs.Empty);
            }
        }
    }
}
