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

        private readonly CustomTextBox TextBox = new CustomTextBox();
        private readonly CustomButton ButtonUp = new CustomButton();
        private readonly CustomButton ButtonDown = new CustomButton();

        private bool IsBackgroundChanged = false;

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
                base.Invalidate();
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
                base.Invalidate();
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
            }
        }

        public NumericUpDownLight()
        {
            DoubleBuffered = true;

            TextBox = new CustomTextBox
            {
                Text = _value.ToString(),
                TextAlign = TextAlign,
            };

            ButtonUp = new CustomButton
            {
                Text = "▲",
                OnMouseClick = this.IncrementValue
            };

            ButtonDown = new CustomButton
            {
                Text = "▼",
                OnMouseClick = this.DecrementValue
            };
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            var upButtonRectangle = GetRectangleForButtonUp();
            var downButtonRectangle = GetRectangleForButtonDown();

            if (upButtonRectangle.Contains(e.Location))
            {
                ButtonUp.OnMouseClick(this, e);
            }
            else if (downButtonRectangle.Contains(e.Location))
            {
                ButtonDown.OnMouseClick(this, e);
            }
        }

        private void UpdateTextBox(Graphics g)
        {
            TextBox.Text = _value.ToString();
            TextBox.TextAlign = _textAlign;
            TextBox.OnPaint(GetRectangleForTextBox(), g);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            UpdateTextBox(e.Graphics);
            ButtonUp.OnPaint(GetRectangleForButtonUp(), e.Graphics);
            ButtonDown.OnPaint(GetRectangleForButtonDown(), e.Graphics);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var upButtonRectangle = GetRectangleForButtonUp();
            var downButtonRectangle = GetRectangleForButtonDown();

            if (!IsBackgroundChanged)
            {
                if (upButtonRectangle.Contains(e.Location))
                {
                    ButtonUp.BackgroundColor = _mouseHoverBackColor;
                    IsBackgroundChanged = true;
                    base.Invalidate();
                }
                else if (downButtonRectangle.Contains(e.Location))
                {
                    ButtonDown.BackgroundColor = _mouseHoverBackColor;
                    IsBackgroundChanged = true;
                    base.Invalidate();
                }
            }

            if (!upButtonRectangle.Contains(e.Location))
            {
                ButtonUp.BackgroundColor = Color.White;
                IsBackgroundChanged = false;
                base.Invalidate();
            }
                
            if (!downButtonRectangle.Contains(e.Location))
            {
                ButtonDown.BackgroundColor = Color.White;
                IsBackgroundChanged = false;
                base.Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            ButtonUp.BackgroundColor = Color.White;
            ButtonDown.BackgroundColor = Color.White;
            IsBackgroundChanged = false;
            base.Invalidate();
        }

        private Rectangle GetRectangleForTextBox()
        {
            return new Rectangle(
                ClientRectangle.Location,
                ClientRectangle.Size);
        }

        private Rectangle GetRectangleForButtonUp()
        {
            var height = ClientRectangle.Height / 2;
            var width = ClientRectangle.Width / 8;

            var x = ClientRectangle.X + ClientRectangle.Width - width;
            var y = ClientRectangle.Y;

            return new Rectangle(
                new Point(x, y),
                new Size(width, height));
        }

        private Rectangle GetRectangleForButtonDown()
        {
            var height = ClientRectangle.Height / 2;
            var width = ClientRectangle.Width / 8;

            var x = ClientRectangle.X + ClientRectangle.Width - width;
            var y = ClientRectangle.Y + height;

            return new Rectangle(
                new Point(x, y),
                new Size(width, height));
        }

        private void IncrementValue(object sender, EventArgs e)
        {
            this._value++;
            base.Invalidate();

            if (_valueChanged != null)
            {
                _valueChanged(this, EventArgs.Empty);
            }
        }

        private void DecrementValue(object sender, EventArgs e)
        {
            this._value--;
            base.Invalidate();

            if (_valueChanged != null)
            {
                _valueChanged(this, EventArgs.Empty);
            }
        }
    }
}
