using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    class CustomTextBox : IControlItem
    {
        private bool shouldRepaint = false;

        public Rectangle ItemRectangle { get; set; }

        public string Text { get; set; }

        public HorizontalAlignment TextAlign { get; set; }

        private StringFormat ConvertTextAlignToTextStyle()
        {
            var style = new StringFormat();
            style.Alignment = StringAlignment.Near;
            switch (TextAlign)
            {
                case HorizontalAlignment.Left:
                    style.Alignment = StringAlignment.Near;
                    break;
                case HorizontalAlignment.Right:
                    style.Alignment = StringAlignment.Far;
                    break;
                case HorizontalAlignment.Center:
                    style.Alignment = StringAlignment.Center;
                    break;
            }

            return style;
        }

        public void OnPaint(Graphics graphics)
        {
            graphics.FillRectangle(
                new SolidBrush(Color.White),
                ItemRectangle);

            graphics.DrawString(Text,
                new Font("Arial", 14, FontStyle.Regular),
                new SolidBrush(Color.Black),
                ItemRectangle,
                ConvertTextAlignToTextStyle());

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
