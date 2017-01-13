using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
using Microsoft.Phone.Controls;

// first over 13 will be the winner

namespace FTOgame
{
    public partial class MainPage : PhoneApplicationPage
    {
        int Usernum, P1num, P2num, P3num;
        int Userscore, P1score, P2score, P3score;
        int Roundnum;
        static int WINCOUNT = 13;
        Rectangle[] r = new Rectangle[4];

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            reset();
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            Usernum = 5;
            PageTitle.Text = "你选择 5";
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            Usernum = 3;
            PageTitle.Text = "你选择 3";
        }

        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            Usernum = 1;
            PageTitle.Text = "你选择 1";
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            if (Usernum != -1)
            {
                RollButton.IsEnabled = false;

                Roundnum++;

                userNum.Text = Usernum.ToString();


                Random rd = new Random();
                int temp = rd.Next(3);
                switch (temp)
                {
                    case 0:
                        P1num = 1;
                        break;
                    case 1:
                        P1num = 3;
                        break;
                    case 2:
                        P1num = 5;
                        break;
                }
                p1Num.Text = P1num.ToString();

                temp = rd.Next(3);                
                switch (temp)
                {
                    case 0:
                        P2num = 1;
                        break;
                    case 1:
                        P2num = 3;
                        break;
                    case 2:
                        P2num = 5;
                        break;
                }
                p2Num.Text = P2num.ToString();

                temp = rd.Next(3);                
                switch (temp)
                {
                    case 0:
                        P3num = 1;
                        break;
                    case 1:
                        P3num = 3;
                        break;
                    case 2:
                        P3num = 5;
                        break;
                }
                p3Num.Text = P3num.ToString();


                if (Usernum != P1num &&
                    Usernum != P2num &&
                    Usernum != P3num)
                {
                    Userscore += Usernum;
                }

                if (P1num != Usernum &&
                    P1num != P2num &&
                    P1num != P3num)
                {
                    P1score += P1num;
                }

                if (P2num != Usernum &&
                    P2num != P1num &&
                    P2num != P3num)
                {
                    P2score += P2num;
                }

                if (P3num != Usernum &&
                    P3num != P2num &&
                    P3num != P1num)
                {
                    P3score += P3num;
                }
                score1.Text = Userscore.ToString();
                score2.Text = P1score.ToString();
                score3.Text = P2score.ToString();
                score4.Text = P3score.ToString();


                if (judge())
                {
                    // someone win the game
                    PageTitle.Text = "游戏结束！";

                    if (Userscore >= 13)
                        canvas1.Children.Remove(r[0]);
                    else
                    {
                        canvas1.Children.Remove(r[0]);
                        r[0].Height = canvas1.Height;
                        canvas1.Children.Add(r[0]);
                    }

                    if (P1score >= 13)
                        canvas2.Children.Remove(r[1]);
                    else
                    {
                        canvas2.Children.Remove(r[1]);
                        r[1].Height = canvas1.Height;
                        canvas2.Children.Add(r[1]);
                    }

                    if (P2score >= 13)
                        canvas3.Children.Remove(r[2]);
                    else
                    {
                        canvas3.Children.Remove(r[2]);
                        r[2].Height = canvas1.Height;
                        canvas3.Children.Add(r[2]);
                    }

                    if (P3score >= 13)
                        canvas4.Children.Remove(r[3]);
                    else
                    {
                        canvas4.Children.Remove(r[3]);
                        r[3].Height = canvas1.Height;
                        canvas4.Children.Add(r[3]);
                    }


                    FiveButton.IsEnabled = false;
                    ThreeButton.IsEnabled = false;
                    OneButton.IsEnabled = false;
                    RollButton.IsEnabled = false;
                    return;

                }
                else
                {
                    double temph = canvas1.Height;

                    canvas1.Children.Remove(r[0]);
                    canvas2.Children.Remove(r[1]);
                    canvas3.Children.Remove(r[2]);
                    canvas4.Children.Remove(r[3]);

                    r[0].Height = temph * (WINCOUNT - Userscore) / WINCOUNT;
                    r[1].Height = temph * (WINCOUNT - P1score) / WINCOUNT;
                    r[2].Height = temph * (WINCOUNT - P2score) / WINCOUNT;
                    r[3].Height = temph * (WINCOUNT - P3score) / WINCOUNT;
                    canvas1.Children.Add(r[0]);
                    canvas2.Children.Add(r[1]);
                    canvas3.Children.Add(r[2]);
                    canvas4.Children.Add(r[3]);
                }

                
                

                RollButton.IsEnabled = true;
                Usernum = -1;
                PageTitle.Text = "第 " + (Roundnum-1).ToString() + " 轮结束";
                roundText.Text = Roundnum.ToString();
            }
            else
            {
                PageTitle.Text = "请选择数字";
            }
            
        }

        private bool judge()
        {
            if (P1score >= 13)
                return true;
            if (P2score >= 13)
                return true;
            if (P2score >= 13)
                return true;
            if (Userscore >= 13)
                return true;

            return false;
        }

        private void reset()
        {
            P1num = P2num = P3num = 0;
            Userscore = P1score = P2score = P3score = 0;
            Roundnum = 1;
            PageTitle.Text = "先到13者胜";
            score1.Text = "0";
            score2.Text = "0";
            score3.Text = "0";
            score4.Text = "0";
            p1Num.Text = "";
            p2Num.Text = "";
            p3Num.Text = "";
            userNum.Text = "";
            Usernum = -1;
            canvas1.Children.Remove(r[0]);
            canvas2.Children.Remove(r[1]);
            canvas3.Children.Remove(r[2]);
            canvas4.Children.Remove(r[3]);

            for (int i = 0; i < 4; i++)
            {
                r[i] = new Rectangle();
                r[i].Height = canvas1.Height;
                r[i].Width = canvas1.Width;
                SolidColorBrush s = new SolidColorBrush(Colors.Black);
                r[i].Fill = s;
            }
            roundText.Text = Roundnum.ToString();

            canvas1.Children.Add(r[0]);
            canvas2.Children.Add(r[1]);
            canvas3.Children.Add(r[2]);
            canvas4.Children.Add(r[3]);

            FiveButton.IsEnabled = true;
            ThreeButton.IsEnabled = true;
            OneButton.IsEnabled = true;
            RollButton.IsEnabled = true;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }

    }
}