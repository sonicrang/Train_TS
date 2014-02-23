using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Train_TS
{
    class Mix
    {
        public ArrayList Logistic(int len)
        {
            int i;
            int temp;
            float x = 0.8f;
            float u = 3.98765f;
            float x_next;
            ArrayList logistic = new ArrayList();
            for (i = 0; i < 200; i++)
            {
                x_next = u * x * (1 - x);
                x = x_next;
            }
            for (i = 0; i < len; i++)
            {
                x_next = u * x * (1 - x);
                x = x_next;
                temp = (int)(x * len);
                logistic.Add(temp);
            }
            return logistic;
        }
    }
}
