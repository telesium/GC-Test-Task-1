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
        private bool _shouldRepaint = false;

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
            using (var brush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(brush,
                    ItemRectangle);
            }

            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font("Arial", 14, FontStyle.Regular))
            {
                e.Graphics.DrawString(Text,
                    font,
                    brush,
                    ItemRectangle,
                    ConvertTextAlignToTextStyle());
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
