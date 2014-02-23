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
    /// winTrack.xaml 的交互逻辑
    /// </summary>
    public partial class winTrack : Window
    {
        private ArrayList listData;
        private int track_len;
        private int station_site;
        private int time;
        private int sm;
        private int pause_time;

        public winTrack(ArrayList listData)
        {
            InitializeComponent();
            this.listData = listData;
            txtTrackLen.Text = this.listData[0].ToString();
            txtStationSite.Text = this.listData[1].ToString();
            txtTime.Text = this.listData[2].ToString();
            txtSM.Text = this.listData[3].ToString();
            txtPause.Text = this.listData[4].ToString();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bool flag;
            flag = int.TryParse(txtTrackLen.Text, out track_len)
             && int.TryParse(txtStationSite.Text, out station_site)
             && int.TryParse(txtTime.Text, out time)
             && int.TryParse(txtSM.Text, out sm)
             && int.TryParse(txtPause.Text,out pause_time);

            if (flag == false)
            {
                MessageBox.Show("参数格式不正确！","操作提示");
            }
            else
            {
                this.listData.Clear();
                this.listData.Add(track_len);
                this.listData.Add(station_site);
                this.listData.Add(time);
                this.listData.Add(sm);
                this.listData.Add(pause_time);
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
