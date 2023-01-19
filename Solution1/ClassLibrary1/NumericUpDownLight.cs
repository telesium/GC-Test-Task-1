using System;
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
                // TODO: Updates textbox text
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
                // TODO: Updates text bot text align
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
                // TODO: Change 
            }
        }

        public NumericUpDownLight()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(
                new Pen(Color.Gray, 2),
                ClientRectangle);
        }

        private void IncrementValue(object sender, EventArgs e)
        {
            this._value++;
            // TODO: Updates text box text

            if (_valueChanged != null)
            {
                _valueChanged(this, EventArgs.Empty);
            }
        }

        private void DecrementValue(object sender, EventArgs e)
        {
            this._value--;
            // TODO: Updates text box text

            if (_valueChanged != null)
            {
                _valueChanged(this, EventArgs.Empty);
            }
        }
    }
}
