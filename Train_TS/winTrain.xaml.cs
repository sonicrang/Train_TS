using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;

namespace Train_TS
{
    /// <summary>
    /// winTrain.xaml 的交互逻辑
    /// </summary>
    public partial class winTrain : Window
    {
        private ArrayList listData;
        private int train_len;
        private int v_max;
        private int t_int;
        private int train_num;
        private int acc_a;
        private int acc_b;

        public winTrain(ArrayList listData)
        {
            InitializeComponent();
            this.listData = listData;
            txtTrainLen.Text = this.listData[0].ToString();
            txtV_max.Text = this.listData[1].ToString();
            txtT_int.Text = this.listData[2].ToString();
            txtTrain_num.Text = this.listData[3].ToString();
            txtAcc_a.Text = this.listData[4].ToString();
            txtAcc_b.Text = this.listData[5].ToString();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bool flag;
            flag = int.TryParse(txtTrainLen.Text, out train_len)
             && int.TryParse(txtV_max.Text, out v_max)
             && int.TryParse(txtT_int.Text, out t_int)
             && int.TryParse(txtTrain_num.Text, out train_num)
             && int.TryParse(txtAcc_a.Text, out acc_a)
             && int.TryParse(txtAcc_b.Text, out acc_b);

            if (flag == false)
            {
                MessageBox.Show("参数格式不正确！", "操作提示");
            }
            else
            {
                this.listData.Clear();
                this.listData.Add(train_len);
                this.listData.Add(v_max);
                this.listData.Add(t_int);
                this.listData.Add(train_num);
                this.listData.Add(acc_a);
                this.listData.Add(acc_b);
                this.listData.Add("true");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
