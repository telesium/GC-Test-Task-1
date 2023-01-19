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
        private bool _ShouldRepaint = false;

        private Color _CurrentBackgroundColor;

        public Rectangle ItemRectangle { get; set; }

        public string Text { get; set; }

        public Color MainBackgroundColor = Color.White;

        public Color HoverBackgroundColor = Color.White;

        public EventHandler OnMouseClick { get; set; }

        public CustomButton()
        {
            _CurrentBackgroundColor = MainBackgroundColor;
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            var oldBackground = _CurrentBackgroundColor;

            if (ItemRectangle.Contains(e.Location))
            {
                _CurrentBackgroundColor = HoverBackgroundColor;
            }
            else
            {
                _CurrentBackgroundColor = MainBackgroundColor;
            }

            if (oldBackground != _CurrentBackgroundColor)
            {
                _ShouldRepaint = true;
            }
        }

        public void OnMouseLeave(EventArgs e) 
        {
            var oldBackground = _CurrentBackgroundColor;
            _CurrentBackgroundColor = MainBackgroundColor;

            if (oldBackground != _CurrentBackgroundColor)
            {
                _ShouldRepaint = true;
            }
        }

        public void HandleClickIfItCans(MouseEventArgs e)
        {
            if (ItemRectangle.Contains(e.X, e.Y))
            {
                OnMouseClick(this, e);
            }
        }

        public void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(
                new SolidBrush(_CurrentBackgroundColor),
                ItemRectangle);

            e.Graphics.DrawString(Text,
                new Font("Arial", 7, FontStyle.Regular),
                new SolidBrush(Color.Black),
                ItemRectangle,
                new StringFormat
                {
                    Alignment = StringAlignment.Center
                });

            e.Graphics.DrawRectangle(
                new Pen(Color.Gray, 2),
                ItemRectangle);
        }

        public bool HasToRepaint()
        {
            return _ShouldRepaint;
        }

        public void OnRepainted()
        {
            _ShouldRepaint = false;
        }
    }
}
