using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    class CustomButton : IControlItem
    {
        public string Text { get; set; }

        public Color BackgroundColor = Color.White;

        public EventHandler OnMouseClick { get; set; }

        public void OnPaint(Rectangle area, Graphics graphics)
        {
            graphics.FillRectangle(
                new SolidBrush(BackgroundColor),
                area);

            graphics.DrawString(Text,
                new Font("Arial", 7, FontStyle.Regular),
                new SolidBrush(Color.Black),
                area,
                new StringFormat
                {
                    Alignment = StringAlignment.Center
                });

            graphics.DrawRectangle(
                new Pen(Color.Gray, 2),
                area);
        }
    }
}
