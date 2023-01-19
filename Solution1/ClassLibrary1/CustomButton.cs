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
        private bool _shouldRepaint = false;

        private Color _currentBackgroundColor;

        public Rectangle ItemRectangle { get; set; }

        public string Text { get; set; }

        public Color MainBackgroundColor = Color.White;

        public Color HoverBackgroundColor = Color.White;

        public EventHandler OnMouseClick { get; set; }

        public CustomButton()
        {
            _currentBackgroundColor = MainBackgroundColor;
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            var oldBackground = _currentBackgroundColor;

            if (ItemRectangle.Contains(e.Location))
            {
                _currentBackgroundColor = HoverBackgroundColor;
            }
            else
            {
                _currentBackgroundColor = MainBackgroundColor;
            }

            if (oldBackground != _currentBackgroundColor)
            {
                _shouldRepaint = true;
            }
        }

        public void OnMouseLeave(EventArgs e) 
        {
            var oldBackground = _currentBackgroundColor;
            _currentBackgroundColor = MainBackgroundColor;

            if (oldBackground != _currentBackgroundColor)
            {
                _shouldRepaint = true;
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
            using (var brush = new SolidBrush(_currentBackgroundColor))
            {
                e.Graphics.FillRectangle(brush,
                    ItemRectangle);
            }

            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font("Arial", 7, FontStyle.Regular))
            {
                e.Graphics.DrawString(Text, font, brush,
                    ItemRectangle,
                    new StringFormat
                    {
                        Alignment = StringAlignment.Center
                    });
            }

            using (var pen = new Pen(Color.Gray, 2))
            {
                e.Graphics.DrawRectangle(pen,
                    ItemRectangle);
            }
        }

        public bool HasToRepaint()
        {
            return _shouldRepaint;
        }

        public void OnRepainted()
        {
            _shouldRepaint = false;
        }
    }
}
