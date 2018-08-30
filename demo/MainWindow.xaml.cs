using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iMessageDemo;
using System.IO;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel allSt1;
        StackPanel allSt2;
        int targetId = 1;
        public MainWindow()
        {
            InitializeComponent();

            allSt1 = new StackPanel();
            allSt2 = new StackPanel();
            inputBox.Focus();
            //ConsoleManager.Show();
        }
        public void change_target1(object sender,RoutedEventArgs e)
        {
            targetHead.Source = new BitmapImage(new Uri("pt1.png", UriKind.Relative));
            //targetName.Content = "Peter";
            targetId = 1;
            displayBoard.Content = allSt1;
            statusImg.Source = new BitmapImage(new Uri("statusBar2.png", UriKind.Relative));
            inputBox.Focus();
            //allSt.Children.Clear();
        }
        public void change_target2(object sender,RoutedEventArgs e)
        {
            targetHead.Source = new BitmapImage(new Uri("pt2.png", UriKind.Relative));
            statusImg.Source = new BitmapImage(new Uri("statusBar.png", UriKind.Relative));
            //targetName.Content = "NingNing";
            targetId = 2;
            displayBoard.Content = allSt2;
            inputBox.Focus();
            //allSt.Children.Clear();
        }

        bool proco = false;
        StackPanel mySt;
        TextBlock myText;
        Image img;
        BitmapImage myImg;
        BitmapImage bImg;
        iDecorator myDec;

        public void showSelf(string s)
        {
            mySt = new StackPanel();
            mySt.Orientation = 0;
            mySt.Margin = new Thickness(5.0);
            mySt.HorizontalAlignment = (HorizontalAlignment)2;
            myText = new TextBlock();
            myText.Text = inputBox.Text;
            myText.VerticalAlignment = (VerticalAlignment)1;
            
            myDec = new iDecorator();
            myDec.Direction = true;
            myDec.Child = myText;
            myDec.HorizontalAlignment = (HorizontalAlignment)1;
            myDec.VerticalAlignment = (VerticalAlignment)1;
            //myDec.MaxWidth = 200;

           // myImg = new BitmapImage(new Uri("p0.jpg", UriKind.Relative));
            //img = new Image();
            //img.Source = myImg;

            mySt.Children.Add(myDec);
            //mySt.Children.Add(img);
            if (targetId == 1)
            {
                allSt1.Children.Add(mySt);
                displayBoard.Content = allSt1;

            }
            else if (targetId==2)
            {
                allSt2.Children.Add(mySt);
                displayBoard.Content = allSt2;
            }
        }
        public void showTarget(string s)
        {
            
            mySt = new StackPanel();
            mySt.Orientation = 0;
            mySt.HorizontalAlignment = (HorizontalAlignment)0;
            myText = new TextBlock();
            myText.Text = inputBox.Text;
            myText.VerticalAlignment = (VerticalAlignment)1;

            myDec = new iDecorator();
            myDec.Direction = false;
            myDec.Child = myText;
            myDec.HorizontalAlignment = (HorizontalAlignment)1;
            myDec.VerticalAlignment = (VerticalAlignment)1;
            //myDec.MaxWidth = 200;
            if (targetId==1)
            {
                myImg = new BitmapImage(new Uri("pt1.png", UriKind.Relative));
                img = new Image();
                img.Source = myImg;
                mySt.Margin = new Thickness(5.0);
                mySt.Height = 40;
                mySt.Children.Add(img);
                mySt.Children.Add(myDec);
                if (targetId == 1)
                {
                    allSt1.Children.Add(mySt);
                    displayBoard.Content = allSt1;

                }
                else if (targetId == 2)
                {
                    allSt2.Children.Add(mySt);
                    displayBoard.Content = allSt2;
                }
                return;
            }
            switch(s[0])
            {
                case ('0'):
                    myImg = new BitmapImage(new Uri("girl_neutral.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_neutral.png", UriKind.Relative));
                    break;
                case ('1'):
                    myImg = new BitmapImage(new Uri("girl_happy.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_happy.png", UriKind.Relative));
                    break;
                case ('2'):
                    myImg = new BitmapImage(new Uri("girl_angry.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_angry.png", UriKind.Relative));
                    break;
                case ('3'):
                    myImg = new BitmapImage(new Uri("girl_sad.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_sad.png", UriKind.Relative));
                    break;
                default:
                    myImg = new BitmapImage(new Uri("girl.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_normal.png", UriKind.Relative));
                    break;
            }
            //myImg = new BitmapImage(new Uri("pt2.png", UriKind.Relative));
            img = new Image();
            img.Source = myImg;

            //borderImage = new Image();
            this.borderImage.Source = bImg;
            //img.Margin = new Thickness(5,5,5,5);

            mySt.Margin = new Thickness(5.0);
            mySt.Height = 40;
            mySt.Children.Add(img);
            mySt.Children.Add(myDec);
            if (targetId == 1)
            {
                allSt1.Children.Add(mySt);
                displayBoard.Content = allSt1;

            }
            else if (targetId == 2)
            {
                allSt2.Children.Add(mySt);
                displayBoard.Content = allSt2;
            }
        }
        bool isMe = true;
        public void click_send_button(object sender, RoutedEventArgs e)
        {

            if (isMe)
                showSelf(inputBox.Text);
            else if (targetId==2&&proco)
            {
                var t1 = new Thread(get_expression);
                t1.Start();
                while (exFlag == false) ;
                exFlag = false;
                t1.Join();
                string ss=inputBox.Text;
                
                if (op == status.NEUTRAL||op==status.NONE)
                    ss = '0' + ss;
                else if (op == status.HAPPINESS||op==status.SURPRISE)
                    ss = '1' + ss;
                else if (op == status.ANGER||op==status.DISGUST||op==status.CONTEMPT)
                    ss = '2' + ss;
                else if (op == status.SADNESS||op==status.FEAR)
                    ss = '3' + ss;
                else
                    ss = '4' + ss;
                showTarget(ss);
            }
            else
            {
                string ss = inputBox.Text;
                showTarget(ss);
            }
            if (isMe == false)
                isMe = true;
            else
                isMe = false;
            //showTarget("QAQ");
            //sendToTarget(inputBox.Text);

            //when information received
            //showTarget();
            inputBox.Clear();
            displayBoard.ScrollToBottom();
        }

        private status op = status.NEUTRAL;
        private bool exFlag = false;
        private async void get_expression()
        {
            DetectionEmotion de = new DetectionEmotion();
            de.GetDevices();
            de.VideoConnect();
            de.GrabBitmap(Directory.GetCurrentDirectory());
            while (de.flag == false) ;
            de.flag = false;
            de.CloseDevice();

            BinaryReader binReader = new BinaryReader(File.Open(de.imageFilePath, FileMode.Open));
            FileInfo fileInfo = new FileInfo(de.imageFilePath);
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();

            BitmapImage bitmapSource = new BitmapImage();
            bitmapSource.BeginInit();
            bitmapSource.StreamSource = new MemoryStream(bytes);
            bitmapSource.EndInit();

            //
            // Create EmotionServiceClient and detect the emotion with URL
            //

            Emotion[] emotionResult = await de.UploadAndDetectEmotions(de.imageFilePath);

            if (emotionResult.Length > 0)
            {
                EmotionResultDisplay[] resultDisplay = new EmotionResultDisplay[8];
                List<EmotionResultDisplayItem> itemSource = new List<EmotionResultDisplayItem>();
                for (int i = 0; i < emotionResult.Length; i++)
                {
                    Emotion emotion = emotionResult[i];
                    resultDisplay[0] = new EmotionResultDisplay { EmotionString = "Anger", Score = emotion.Scores.Anger };
                    resultDisplay[1] = new EmotionResultDisplay { EmotionString = "Contempt", Score = emotion.Scores.Contempt };
                    resultDisplay[2] = new EmotionResultDisplay { EmotionString = "Disgust", Score = emotion.Scores.Disgust };
                    resultDisplay[3] = new EmotionResultDisplay { EmotionString = "Fear", Score = emotion.Scores.Fear };
                    resultDisplay[4] = new EmotionResultDisplay { EmotionString = "Happiness", Score = emotion.Scores.Happiness };
                    resultDisplay[5] = new EmotionResultDisplay { EmotionString = "Neutral", Score = emotion.Scores.Neutral };
                    resultDisplay[6] = new EmotionResultDisplay { EmotionString = "Sadness", Score = emotion.Scores.Sadness };
                    resultDisplay[7] = new EmotionResultDisplay { EmotionString = "Surprise", Score = emotion.Scores.Surprise };

                    Array.Sort(resultDisplay, delegate(EmotionResultDisplay result1, EmotionResultDisplay result2)
                    {
                        return ((result1.Score == result2.Score) ? 0 : ((result1.Score < result2.Score) ? 1 : -1));
                    });
                }
                string emotionStatus = resultDisplay[0].EmotionString;
                switch (emotionStatus)
                {
                    case "Anger": op = status.ANGER; break;
                    case "Contempt": op = status.CONTEMPT; break;
                    case "Disgust": op = status.DISGUST; break;
                    case "Fear": op = status.FEAR; break;
                    case "Happiness": op = status.HAPPINESS; break;
                    case "Neutral": op = status.NEUTRAL; break;
                    case "Sadness": op = status.SADNESS; break;
                    case "Surprise": op = status.SURPRISE; break;
                    default: op = status.NEUTRAL; break;
                }
                exFlag = true;
            }
            else
            {
                op = status.NONE;
                exFlag = true;
            }

        }
        public void click_trigger_button(object sender, RoutedEventArgs e)
        {
            var t1 = new Thread(get_expression);
            t1.Start();
            
            while (exFlag == false) ;
            exFlag = false;

            switch (op)
            {
                case (status.NEUTRAL):
                    myImg = new BitmapImage(new Uri("neutral.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_neutral.png", UriKind.Relative));
                    break;
                case (status.HAPPINESS):
                case (status.SURPRISE):
                    myImg = new BitmapImage(new Uri("happy.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_happy.png", UriKind.Relative));
                    break;
                case (status.ANGER):
                case (status.DISGUST):
                case (status.CONTEMPT):
                    myImg = new BitmapImage(new Uri("angry.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_angry.png", UriKind.Relative));
                    break;
                case (status.SADNESS):
                case (status.FEAR):
                    myImg = new BitmapImage(new Uri("sad.png", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_sad.png", UriKind.Relative));
                    break;
                default:
                    myImg = new BitmapImage(new Uri("p0.jpg", UriKind.Relative));
                    bImg = new BitmapImage(new Uri("boy_normal.png", UriKind.Relative));
                    break;
            }
            //myImg = new BitmapImage(new Uri("pt2.png", UriKind.Relative));
            img = new Image();
            img.Source = myImg;
            mySt = new StackPanel();
            mySt.Orientation = 0;
            mySt.Margin = new Thickness(5.0);
            mySt.HorizontalAlignment = (HorizontalAlignment)2;
            
            myDec = new iDecorator();
            myDec.Direction = true;
            //myDec.Child = img;
            myDec.HorizontalAlignment = (HorizontalAlignment)1;
            myDec.VerticalAlignment = (VerticalAlignment)1;
            //myDec.MaxWidth = 200;

           // myImg = new BitmapImage(new Uri("p0.jpg", UriKind.Relative));
            //img = new Image();
            //img.Source = myImg;

            //mySt.Children.Add(myDec);
            mySt.Children.Add(img);
            //mySt.Children.Add(img);
            /*
            if (targetId == 1)
            {
                allSt1.Children.Add(mySt);
                displayBoard.Content = allSt1;

            }
            else if (targetId==2)
            {
                allSt2.Children.Add(mySt);
                displayBoard.Content = allSt2;
            }*/


            //borderImage = new Image();
            this.borderImage.Source = bImg;
            //img.Margin = new Thickness(5,5,5,5);

            mySt.Height = 80;
            if (targetId == 1)
            {
                allSt1.Children.Add(mySt);
                displayBoard.Content = allSt1;

            }
            else if (targetId == 2)
            {
                allSt2.Children.Add(mySt);
                displayBoard.Content = allSt2;
            }

        }
        public void switch_proco(object sender, RoutedEventArgs e)
        {
            if (proco)
            {
                proco = false;
                procoImg.Source = new BitmapImage(new Uri("off.png", UriKind.Relative));
                inputBox.Focus();
            }
            else
            {
                proco = true;
                procoImg.Source = new BitmapImage(new Uri("on.png", UriKind.Relative));
                inputBox.Focus();
            }
        }
    }
    /*
    class client
    {
        private static byte[] result = new byte[1024];
        public void init(string tosend)
        {
            //设定服务器IP地址
            IPAddress ip = IPAddress.Parse("10.221.64.167");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //通过clientSocket接收数据
            //int receiveLength = clientSocket.Receive(result);
            //Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            //通过 clientSocket 发送数据
            for (int i = 0; i < 1; i++)
            {
                try
                {
                    //Thread.Sleep(1000);    //等待1秒钟
                    //string sendMessage = "client send Message Hellp" + DateTime.Now;
                    // clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    //Console.WriteLine("向服务器发送消息：{0}" + sendMessage);

                    string str = tosend;
                    clientSocket.Send(Encoding.ASCII.GetBytes(str));
                    Console.WriteLine("向服务器发送消息：" + str);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
            //Console.WriteLine("发送完毕，按回车键退出");
            //Console.ReadLine();

        }

        /*public void init()
        {
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //通过clientSocket接收数据
            int receiveLength = clientSocket.Receive(result);
            Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            //通过 clientSocket 发送数据
            for (int i = 0; i < 1; i++)
            {
                try
                {
                    //Thread.Sleep(1000);    //等待1秒钟
                    //string sendMessage = "client send Message Hellp" + DateTime.Now;
                    // clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    //Console.WriteLine("向服务器发送消息：{0}" + sendMessage);

                    string str = Console.ReadLine();
                    clientSocket.Send(Encoding.ASCII.GetBytes(str));
                    Console.WriteLine("向服务器发送消息：" + str);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
        }
    }//
    */
}


