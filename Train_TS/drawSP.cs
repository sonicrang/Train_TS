using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace Train_TS
{
    class drawSP
    {
        private ArrayList mysp;
        private int x1, x2, y1, y2;
        private double narrow_x, narrow_y;
        DrawingVisual drawingVisual;
        DrawingContext dc;
        public drawSP()
        {
            mysp = new ArrayList();
        }

        public void Init_DrawSP(ArrayList sp, int track_len, int v_max)
        {
            int i;
            int temp;
            mysp = sp;
            temp = 0;
            x1 = 30;
            y1 = 10;
            x2 = 700;
            y2 = 580;
            narrow_x = (double)track_len / (x2-x1);
            narrow_y = (double)v_max / ((y2 - y1) / 2);
            drawingVisual = new DrawingVisual();
            dc = drawingVisual.RenderOpen();
            dc.DrawLine(new Pen(Brushes.Black, 1.5), new Point(x1, x1), new Point(x1, y2));
            dc.DrawLine(new Pen(Brushes.Black, 1.5), new Point(x1, y2), new Point(x2, y2));
            dc.DrawText(new FormattedText("T",
                      CultureInfo.GetCultureInfo("en-us"),
                      FlowDirection.LeftToRight,
                      new Typeface("Verdana"),
                      12, System.Windows.Media.Brushes.Black),
                      new System.Windows.Point(x2 - 30, y2 + 6));
            dc.DrawText(new FormattedText("V",
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
            for (i = 0; i < (y2 - y1)/2; i++)
            {
                if (i % ((y2 - y1) / 10) == 0)
                {
                    dc.DrawLine(new Pen(Brushes.Black, 1.5), new Point(x1, y2 - i), new Point(x1 + 3, y2 - i));
                    dc.DrawText(new FormattedText((temp * v_max / 5).ToString(),
                      CultureInfo.GetCultureInfo("en-us"),
                      FlowDirection.LeftToRight,
                      new Typeface("Verdana"),
                      12, System.Windows.Media.Brushes.Black),
                      new System.Windows.Point(x1 - 28, y2 - i - 9));
                    temp++;
                }
            }
        }

        public void DrawSP()
        {
            if (mysp != null)
            {
                int i;
                for (i = 0; i < mysp.Count; i+=2)
                {
                    dc.DrawEllipse(Brushes.Black, new Pen(), new Point((int)mysp[i + 1] / narrow_x + x1, y2 - (int)mysp[i] / narrow_y), 1, 1);
                }
            }
        }

        public RenderTargetBitmap Finish_DrawSP()
        {
            dc.Close();
            RenderTargetBitmap bmp = new RenderTargetBitmap(720, 600,
                96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            return bmp;
        }
    }
}
