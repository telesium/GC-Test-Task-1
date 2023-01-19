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
        private bool _ShouldRepaint = false;

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

        public void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(
                new SolidBrush(Color.White),
                ItemRectangle);

            e.Graphics.DrawString(Text,
                new Font("Arial", 14, FontStyle.Regular),
                new SolidBrush(Color.Black),
                ItemRectangle,
                ConvertTextAlignToTextStyle());

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
