using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Globalization;

namespace Train_TS
{
    class drawST
    {
        private ArrayList myst;
        private int x1, x2, y1, y2;
        private double narrow_x, narrow_y;
        DrawingVisual drawingVisual;
        DrawingContext dc;

        public drawST()
        {
            myst = new ArrayList();
        }

        public void set_st(ArrayList st)
        {
            myst = st;
        }

        public void Init_DrawTS(int track_len, int time)
        {
            int i;
            int temp;
            temp = 0;
            x1 = 30;
            y1 = 10;
            x2 = 700;
            y2 = 580;
            narrow_x = (double)track_len / (x2 - x1);
            narrow_y = (double)time / (y2 - y1);
            drawingVisual = new DrawingVisual();
            dc = drawingVisual.RenderOpen();
            dc.DrawLine(new Pen(Brushes.Black, 2), new Point(x1, x1), new Point(x1, y2));
            dc.DrawLine(new Pen(Brushes.Black, 2), new Point(x1, y2), new Point(x2, y2));
            dc.DrawText(new FormattedText("S",
                      CultureInfo.GetCultureInfo("en-us"),
                      FlowDirection.LeftToRight,
                      new Typeface("Verdana"),
                      12, System.Windows.Media.Brushes.Black),
                      new System.Windows.Point(x2 - 30, y2 + 6));
            dc.DrawText(new FormattedText("T",
                      CultureInfo.GetCultureInfo("en-us"),
                      FlowDirection.LeftToRight,
                      new Typeface("Verdana"),
                      12, System.Windows.Media.Brushes.Black),
                      new System.Windows.Point(x1 - 10, y1 - 8));
            for (i = 0; i < (x2 - x1); i++)
            {
                if (i % ((x2 - x1) / 5) == 0)
                {
                    dc.DrawLine(new Pen(Brushes.Black, 1.5), new Point(x1 + i, y2), new Point(x1 + i, y2 - 3));
                    dc.DrawText(new FormattedText((temp * track_len / 5).ToString(),
                      CultureInfo.GetCultureInfo("en-us"),
                      FlowDirection.LeftToRight,
                      new Typeface("Verdana"),
                      12, System.Windows.Media.Brushes.Black),
                      new System.Windows.Point(i + x1 - 5, y2 + 7));
                    temp++;
                }
            }

            temp = 0;
            for (i = 0; i < (y2 - y1); i++)
            {
                if (i % ((y2 - y1) / 5) == 0)
                {
                    dc.DrawLine(new Pen(Brushes.Black, 1.5), new Point(x1, y2 - i), new Point(x1 + 3, y2 - i));
                    dc.DrawText(new FormattedText((temp * time / 5).ToString(),
                      CultureInfo.GetCultureInfo("en-us"),
                      FlowDirection.LeftToRight,
                      new Typeface("Verdana"),
                      12, System.Windows.Media.Brushes.Black),
                      new System.Windows.Point(x1 - 28, y2 - i - 9));
                    temp++;
                }
            }
        }

        public void DrawTS()
        {

            if (myst != null)
            {
                int i;
                for (i = 0; i < myst.Count; i += 2)
                {
                    dc.DrawLine(new Pen(Brushes.Black, 1), new Point((int)myst[i + 1] / narrow_x + x1, y2 - (int)myst[i] / narrow_y), new Point((int)myst[i + 1] / narrow_x + x1 + 1, y2 - (int)myst[i] / narrow_y));
                }
            }

        }

        public RenderTargetBitmap Finish_DrawTS()
        {
            dc.Close();
            RenderTargetBitmap bmp = new RenderTargetBitmap(720, 600,
                96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            return bmp;
        }
    }
}
