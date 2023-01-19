using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public class NumericUpDownLight : Control
    {
        private int _numericValue = 0;
        private HorizontalAlignment _alignmentValue = HorizontalAlignment.Left;
        private Color _mouseHoverBackColorValue = Color.Blue;

        private CustomTextBox _textBox = new CustomTextBox();
        private CustomButton _buttonUp = new CustomButton();
        private CustomButton _buttonDown = new CustomButton();

        private Timer _timer = null;

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
                return _numericValue;
            }
            set
            {
                _numericValue = value;
                _textBox.Text = _numericValue.ToString();
                Invalidate();
            }
        }

        [
        Category("Property Changed"),
        Description("ValueChanged event triggers after the value was changed")
        ]
        public event EventHandler ValueChanged;

        [
        Category("Appearance"),
        Description("Specifies the alignment of the text")
        ]
        public HorizontalAlignment TextAlign
        {
            get
            {
                return _alignmentValue;
            }
            set
            {
                _alignmentValue = value;
                _textBox.TextAlign = _alignmentValue;
                Invalidate();
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
                return _mouseHoverBackColorValue;
            }
            set
            {
                _mouseHoverBackColorValue = value;
                _buttonUp.HoverBackgroundColor = _mouseHoverBackColorValue;
                _buttonDown.HoverBackgroundColor = _mouseHoverBackColorValue;
                base.Invalidate();
            }
        }

        public NumericUpDownLight()
        {
            DoubleBuffered = true;
            _timer = new Timer
            {
                Interval = 50,
            };
            _timer.Tick += new EventHandler(OnTimerTick);
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            bool shouldInvalidate = false;
            
            if (_buttonUp.HasToRepaint()) 
            {
                _buttonUp.OnRepainted();
                shouldInvalidate = true;
            } 
            
            if (_buttonDown.HasToRepaint())
            {
                _buttonDown.OnRepainted();
                shouldInvalidate = true;
            }

            if (_textBox.HasToRepaint())
            {
                _textBox.OnRepainted();
                shouldInvalidate = true;
            }

            if (shouldInvalidate)
            {
                Invalidate();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            _textBox = new CustomTextBox
            {
                Text = Value.ToString(),
                TextAlign = TextAlign,
                ItemRectangle = GetRectangleForTextBox()
            };

            _buttonUp = new CustomButton
            {
                Text = "▲",
                OnMouseClick = IncrementValue,
                HoverBackgroundColor = MouseHoverBackColor,
                ItemRectangle = GetRectangleForButtonUp()
            };

            _buttonDown = new CustomButton
            {
                Text = "▼",
                OnMouseClick = DecrementValue,
                HoverBackgroundColor = MouseHoverBackColor,
                ItemRectangle = GetRectangleForButtonDown()
            };
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            _buttonUp.HandleClickIfItCans(e);
            _buttonDown.HandleClickIfItCans(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _textBox.OnPaint(e);
            _buttonUp.OnPaint(e);
            _buttonDown.OnPaint(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _buttonUp.OnMouseMove(e);
            _buttonDown.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _buttonUp.OnMouseLeave(e);
            _buttonDown.OnMouseLeave(e);
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
            Value++;
            Invalidate();

            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        private void DecrementValue(object sender, EventArgs e)
        {
            Value--;
            Invalidate();

            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }
    }
}
