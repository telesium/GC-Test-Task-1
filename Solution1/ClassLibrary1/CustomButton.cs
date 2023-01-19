using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public class CustomButton : IControlItem
    {
        private bool shouldRepaint = false;

        private Color CurrentBackgroundColor;

        public Rectangle ItemRectangle { get; set; }

        public string Text { get; set; }

        public Color MainBackgroundColor = Color.White;

        public Color HoverBackgroundColor = Color.White;

        public EventHandler OnMouseClick { get; set; }

        public CustomButton()
        {
            this.CurrentBackgroundColor = MainBackgroundColor;
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            var oldBackground = CurrentBackgroundColor;

            if (ItemRectangle.Contains(e.Location))
            {
                CurrentBackgroundColor = HoverBackgroundColor;
            }
            else
            {
                CurrentBackgroundColor = MainBackgroundColor;
            }

            if (oldBackground != CurrentBackgroundColor)
            {
                shouldRepaint = true;
            }
        }

        public void OnMouseLeave(EventArgs e) 
        {
            var oldBackground = CurrentBackgroundColor;
            CurrentBackgroundColor = MainBackgroundColor;

            if (oldBackground != CurrentBackgroundColor)
            {
                shouldRepaint = true;
            }
        }

        public void HandleClickIfItCans(MouseEventArgs e)
        {
            if (ItemRectangle.Contains(e.X, e.Y))
            {
                OnMouseClick(this, e);
            }
        }

        public void OnPaint(Graphics graphics)
        {
            graphics.FillRectangle(
                new SolidBrush(CurrentBackgroundColor),
                ItemRectangle);

            graphics.DrawString(Text,
                new Font("Arial", 7, FontStyle.Regular),
                new SolidBrush(Color.Black),
                ItemRectangle,
                new StringFormat
                {
                    Alignment = StringAlignment.Center
                });

            graphics.DrawRectangle(
                new Pen(Color.Gray, 2),
                ItemRectangle);
        }

        public bool HasToRepaint()
        {
            return shouldRepaint;
        }

        public void OnRepainted()
        {
            shouldRepaint = false;
        }
    }
}
