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
using Microsoft.Phone.Controls;

namespace FTOgame
{
    public partial class Help : PhoneApplicationPage
    {
        public Help()
        {
            InitializeComponent();
            string help = "这个游戏需要您快速判断与选择\n每次从5、3、1中选一个数字\n只要其他三人没有跟你选择一样的\n您就可以加上对应选择的数字的分数\n先到13分，你就赢了！";
            help += "\n\n多人游戏及联机游戏将在陆续加入！\n这是另一个CodePlane Product！";
            textBlock1.Text = help;
        }
    }
}