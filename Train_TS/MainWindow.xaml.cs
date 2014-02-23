using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Windows;

namespace Train_TS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrayList listData_track;
        private ArrayList listData_train;
        public ArrayList Logisitic;
        public ArrayList SP;
        private int track_len;             //轨道长
        private int station_site;          //车站位置
        private int time;                  //模拟时间
        private int sm;                    //安全距离
        private int pause_time;            //停留时间
        private int train_len;             //车长
        private int v_max;                 //最高时速
        private int t_int;                 //发车间隔
        private int train_num;             //列车数量
        private int acc_a;                 //加速度
        private int acc_b;                 //减速福
        private bool track_edit_flag;      
        private bool train_edit_flag;
        private static int add_count;      //统计列车数量 
        private int running_train;         //能运行的列车数量
        private int standard_time;         //正常到站时间


        public MainWindow()
        {
            InitializeComponent();
            Init();
        }


        struct S_train
        {
            public int train_len;         
            public int V_max;
            public int T_int;
            public int a;
            public int b;
            public int v;
            public int d;                 //安全距离
            public int c;
            public int x;
            public int gap;              
            public int pause;
            public int arrive_time;
            public int standard_time;
            public int start_time;
            public ArrayList spacetime;

        }

        S_train[] train;

        private void Init()
        {
            train = new S_train[500];
            track_len = 0;
            station_site = 0;
            time = 0;
            sm = 0;
            pause_time = 0;
            train_len = 0;
            v_max = 0;
            t_int = 0;
            train_num = 0;
            acc_a = 0;
            acc_b = 0;
            track_edit_flag = false;
            train_edit_flag = false;
            add_count = 0;
            running_train = 0;

            listData_track = new ArrayList();
            listData_track.Add(track_len);
            listData_track.Add(station_site);
            listData_track.Add(time);
            listData_track.Add(sm);
            listData_track.Add(pause_time);
            listData_track.Add(track_edit_flag);

            listData_train = new ArrayList();
            listData_train.Add(train_len);
            listData_train.Add(v_max);
            listData_train.Add(t_int);
            listData_train.Add(train_num);
            listData_train.Add(acc_a);
            listData_train.Add(acc_b);
            listData_train.Add(train_edit_flag);

            Logisitic = new ArrayList();
            SP = new ArrayList();
            txtInfo.Clear();
            txtTrack.Clear();
            txtTrain.Clear();

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("您确定要新建模拟方案?", "操作提示",
            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Init();
            }
        }

        private void Track_Click(object sender, RoutedEventArgs e)
        {
            winTrack myTrack = new winTrack(listData_track);
            myTrack.Owner = this;
            myTrack.ShowDialog();
            if (bool.Parse(listData_track[5].ToString()) == true)
            {
                track_len = int.Parse(listData_track[0].ToString());
                station_site = int.Parse(listData_track[1].ToString());
                time = int.Parse(listData_track[2].ToString());
                sm = int.Parse(listData_track[3].ToString());
                pause_time = int.Parse(listData_track[4].ToString());
                txtTrack.Text += "更新轨道信息：\n";
                txtTrack.Text += "轨道长度：" + track_len;
                txtTrack.Text += "\n车站位置：" + station_site;
                txtTrack.Text += "\n模拟时间：" + time;
                txtTrack.Text += "\n安全距离：" + sm;
                txtTrack.Text += "\n停留时间：" + pause_time;
                txtTrack.Text += "\n----------------------------\n";
                listData_track[5] = false;
            }
      
        }

        private void Train_Click(object sender, RoutedEventArgs e)
        {
            int i;
            winTrain myTrain = new winTrain(listData_train);
            myTrain.Owner = this;
            myTrain.ShowDialog();
            if (bool.Parse(listData_train[6].ToString()) == true)
            {
                train_len = int.Parse(listData_train[0].ToString());
                v_max = int.Parse(listData_train[1].ToString());
                t_int = int.Parse(listData_train[2].ToString());
                train_num = int.Parse(listData_train[3].ToString());
                acc_a = int.Parse(listData_train[4].ToString());
                acc_b = int.Parse(listData_train[5].ToString());
                standard_time = Standard_Time();
                txtTrain.Text += "添加列车信息：\n";
                txtTrain.Text += "列车长度：" + train_len;
                txtTrain.Text += "\n最高时速：" + v_max;
                txtTrain.Text += "\n发车间隔：" + t_int;
                txtTrain.Text += "\n列车数量：" + train_num;
                txtTrain.Text += "\n加速度：" + acc_a;
                txtTrain.Text += "\n减速度：" + acc_b;
                txtTrain.Text += "\n----------------------------\n";
                listData_train[6] = false;
                for (i = 0 + add_count; i < train_num + add_count; i++)
                {
                    train[i].train_len = train_len;
                    train[i].V_max = v_max;
                    train[i].T_int = t_int;
                    train[i].a = acc_a;
                    train[i].b = acc_b;
                    train[i].c = 0;
                    train[i].x = 0;
                    train[i].v = 0;
                    train[i].pause = 0;
                    train[i].standard_time = standard_time;
                    train[i].gap = station_site;
                    train[i].start_time = 0;
                    train[i].spacetime = new ArrayList();
                }
                add_count += train_num;
            }
        }
        /// <summary>
        /// 计算正常到站时间
        /// </summary>
        /// <returns></returns>
        private int Standard_Time()
        {
            int c = 0;
            int gap = station_site;
            int v = 0;
            int x = 0;
            int t = 0;
            while (t < time)
            {
                gap = station_site - x;
                c = v * v / (2 * acc_b);
                if (gap > c)
                {
                    v = v + acc_a > v_max ? v_max : v + acc_a;
                }
                if (gap < c)
                {
                    v = v > acc_b ? v - acc_b : 0;
                }
                //减速
                v = v > gap ? gap : v;
                //位置更新
                x += v;
                if (x == station_site)
                    return t;
                t++;
            }
            return t;
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            MixInfo();
            runtrain();
        }
        /// <summary>
        /// 置乱不同性能的车辆
        /// </summary>
        private void MixInfo()
        {
            int i;
            S_train temp = new S_train();
            Mix myMix = new Mix();
            Logisitic = myMix.Logistic(add_count);
            for (i = 0; i < add_count; i += 2)
            {
                temp = train[(int)Logisitic[i]];
                train[(int)Logisitic[i]] = train[(int)Logisitic[i + 1]];
                train[(int)Logisitic[i + 1]] = temp;
            }
        }
        /// <summary>
        /// 模拟车辆运行
        /// </summary>
        private void runtrain()
        {
            int i;
            int t = 0;  
            int wait = 0;
            running_train = 0;
            while (t < time)
            {
                for (i = 0; i < add_count; i++)
                {
                    //第一辆列车或者到达发车间隔时间，发车
                    if (running_train == 0 || wait >= train[running_train - 1].T_int)
                    {
                        train[running_train].start_time = t;
                        running_train++;
                        wait = 0;
                    }
                    //只有运行中的列车才进行模拟计算，否则跳出
                    if (i >= running_train)
                    {
                        break;
                    }
                    if ((i == 0 && train[0].x != station_site) || (train[i].x < station_site && train[i - 1].x > station_site))  //前方是车站
                    {
                        //加速
                        if (i == 0 && train[i].x > station_site)
                            train[0].gap = 9999;
                        else
                            train[i].gap = station_site - train[i].x;
                        train[i].c = train[i].v * train[i].v / (2 * train[i].b);
                        if (train[i].gap > train[i].c)
                        {
                            train[i].v = train[i].v + train[i].a > train[i].V_max ? train[i].V_max : train[i].v + train[i].a;
                        }
                        if (train[i].gap < train[i].c)
                        {
                            train[i].v = train[i].v > train[i].b ? train[i].v - train[i].b : 0;
                        }
                        //减速
                        train[i].v = train[i].v > train[i].gap ? train[i].gap : train[i].v;
                        //位置更新
                        train[i].x += train[i].v;

                    }
                    else if (train[i].x < station_site && train[i - 1].x <= station_site || train[i].x > station_site) //前方是列车
                    {
                        //加速
                        int sub_x;
                        sub_x = train[i - 1].x - train[i].x;
                        train[i].d = train[i].V_max * train[i].V_max / (2 * train[i].b) + sm + train[i - 1].train_len;
                        if (sub_x > train[i].d)
                        {
                            train[i].v = train[i].v + train[i].a > train[i].V_max ? train[i].V_max : train[i].v + train[i].a;

                        }
                        if (sub_x < train[i].d)
                        {
                            train[i].v = train[i].v - train[i].b > 0 ? train[i].v - train[i].b : 0;
                        }
                        //减速
                        train[i].v = train[i].v > sub_x ? sub_x : train[i].v;
                        //位置更新
                        train[i].x += train[i].v;
                    }

                    if (train[i].x == station_site) //到站
                    {
                        //记录到站时间
                        if (train[i].arrive_time == 0)
                        {
                            train[i].arrive_time = t;
                            train[i].standard_time = train[i].standard_time + train[i].start_time;
                        }
                        if (train[i].pause <= pause_time)
                        {
                            train[i].v = 0;
                        }
                        else
                        {
                            train[i].v += train[i].a;
                            train[i].x += train[i].v;
                        }
                        train[i].pause++;
                    }
                    //记录时空数据
                    if (train[i].x <= track_len)
                    {
                        train[i].spacetime.Add(t);
                        train[i].spacetime.Add(train[i].x);

                        if(i == 0)
                        {
                            SP.Add(train[0].v);
                            SP.Add(train[0].x);
                        }
                    }
                   
                }
                t++;
                wait++;
            }
            txtInfo.Text += "模拟完成\n运行车辆: " + running_train 
                + "\n-------------------------------------------------------------------------\n";
            
        }

        private void ST_Click(object sender, RoutedEventArgs e)
        {
            int i;
            drawST myst = new drawST();
            if (running_train != 0)
            {
                myst.Init_DrawTS(track_len, time);
                for (i = 0; i < running_train; i++)
                {
                    myst.set_st(train[i].spacetime);
                    myst.DrawTS();
                }
                imgPic.Source = myst.Finish_DrawTS();
            }
            
        }

        private void LT_Click(object sender, RoutedEventArgs e)
        {
            int i;
            drawLT mylt = new drawLT();
            if (running_train != 0)
            {
                mylt.Init_DrawLT(running_train);
                for (i = 0; i < running_train; i++)
                {
                    mylt.set_lt(train[i].arrive_time - train[i].standard_time);
                    mylt.DrawLT();
                }
                imgPic.Source = mylt.Finish_DrawTS();
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSP_Click(object sender, RoutedEventArgs e)
        {
            drawSP mysp= new drawSP();
            if (running_train != 0)
            {
                mysp.Init_DrawSP(SP, track_len, train[0].V_max);
                mysp.DrawSP();
                imgPic.Source = mysp.Finish_DrawSP();
            }

        }

        private void About(object sender, RoutedEventArgs e)
        {
            winAbout myabout = new winAbout();
            myabout.ShowDialog();
        }  

   
    }
}
