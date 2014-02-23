using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Globalization;

namespace Train_TS
{
    class drawLT
    {
        private ArrayList mylt;
        private int x1, x2, y1, y2;
        private double narrow_x, narrow_y;
        DrawingVisual drawingVisual;
        DrawingContext dc;

        public drawLT()
        {
            mylt = new ArrayList();
        }

        public void set_lt(int lt)
        {
            mylt.Add(lt);
        }

        public void Init_DrawLT(int running_train)
        {
            int i;
            int temp;
            int late_time_range;
            late_time_range = 1000;
            temp = 0;
            x1 = 30;
            y1 = 10;
            x2 = 700;
            y2 = 580;
            narrow_x = (double)running_train / (x2 - x1);
            narrow_y = (double)late_time_range / (y2 - y1);
            drawingVisual = new DrawingVisual();
            dc = drawingVisual.RenderOpen();
            dc.DrawLine(new Pen(Brushes.Black, 1.5), new Point(x1, x1), new Point(x1, y2));
            dc.DrawLine(new Pen(Brushes.Black, 1.5), new Point(x1, y2), new Point(x2, y2));
            dc.DrawText(new FormattedText("N",
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
                    dc.DrawText(new FormattedText((temp * running_train / 5).ToString(),
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
                    dc.DrawText(new FormattedText((temp * late_time_range / 5).ToString(),
                      CultureInfo.GetCultureInfo("en-us"),
                      FlowDirection.LeftToRight,
                      new Typeface("Verdana"),
                      12, System.Windows.Media.Brushes.Black),
                      new System.Windows.Point(x1 - 28, y2 - i - 9));
                    temp++;
                }
            }
        }

        public void DrawLT()
        {
            if (mylt != null)
            {
                int i;
                for (i = 0; i < mylt.Count; i++)
                {
                    dc.DrawEllipse(Brushes.Black,new Pen(), new Point(i / narrow_x + x1, y2 - (int)mylt[i] / narrow_y),2,2);
                  
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
