using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public class NumericUpDownLight : Control
    {
        private int numericValue = 0;
        private HorizontalAlignment alignmentValue = HorizontalAlignment.Left;
        private Color mouseHoverBackColorValue = Color.Blue;

        private CustomTextBox TextBox = new CustomTextBox();
        private CustomButton ButtonUp = new CustomButton();
        private CustomButton ButtonDown = new CustomButton();

        private Timer timer = null;

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
                return numericValue;
            }
            set
            {
                numericValue = value;
                TextBox.Text = numericValue.ToString();
                base.Invalidate();
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
                return alignmentValue;
            }
            set
            {
                alignmentValue = value;
                TextBox.TextAlign = alignmentValue;
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
                return mouseHoverBackColorValue;
            }
            set
            {
                mouseHoverBackColorValue = value;
                ButtonUp.HoverBackgroundColor = mouseHoverBackColorValue;
                ButtonDown.HoverBackgroundColor = mouseHoverBackColorValue;
                base.Invalidate();
            }
        }

        public NumericUpDownLight()
        {
            DoubleBuffered = true;
            this.timer = new Timer
            {
                Interval = 50,
            };
            this.timer.Tick += new EventHandler(this.OnTimerTick);
            this.timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            bool shouldInvalidate = false;
            
            if (ButtonUp.HasToRepaint()) 
            {
                ButtonUp.OnRepainted();
                shouldInvalidate = true;
            } 
            
            if (ButtonDown.HasToRepaint())
            {
                ButtonDown.OnRepainted();
                shouldInvalidate = true;
            }

            if (TextBox.HasToRepaint())
            {
                TextBox.OnRepainted();
                shouldInvalidate = true;
            }

            if (shouldInvalidate)
            {
                base.Invalidate();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            TextBox = new CustomTextBox
            {
                Text = Value.ToString(),
                TextAlign = TextAlign,
                ItemRectangle = GetRectangleForTextBox()
            };

            ButtonUp = new CustomButton
            {
                Text = "▲",
                OnMouseClick = this.IncrementValue,
                HoverBackgroundColor = MouseHoverBackColor,
                ItemRectangle = GetRectangleForButtonUp()
            };

            ButtonDown = new CustomButton
            {
                Text = "▼",
                OnMouseClick = this.DecrementValue,
                HoverBackgroundColor = MouseHoverBackColor,
                ItemRectangle = GetRectangleForButtonDown()
            };
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ButtonUp.HandleClickIfItCans(e);
            ButtonDown.HandleClickIfItCans(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            TextBox.OnPaint(e.Graphics);
            ButtonUp.OnPaint(e.Graphics);
            ButtonDown.OnPaint(e.Graphics);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ButtonUp.OnMouseMove(e);
            ButtonDown.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ButtonUp.OnMouseLeave(e);
            ButtonDown.OnMouseLeave(e);
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
            this.Value++;
            base.Invalidate();

            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        private void DecrementValue(object sender, EventArgs e)
        {
            this.Value--;
            base.Invalidate();

            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }
    }
}
