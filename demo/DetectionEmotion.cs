using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

// -----------------------------------------------------------------------
// Camera Code
using AForge;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;

using Size = System.Drawing.Size;

// ----------------------------------------------------------------------- 
// Use the following namesapce for EmotionServiceClient
// -----------------------------------------------------------------------
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;


namespace demo
{        
    
    enum status{ANGER,CONTEMPT,DISGUST,FEAR,HAPPINESS,NEUTRAL,SADNESS,SURPRISE,NONE}

    internal class EmotionResultDisplay
    {
        public string EmotionString
        {
            get;
            set;
        }
        public float Score
        {
            get;
            set;
        }
    }
    public class EmotionResultDisplayItem
    {
        public Uri ImageSource { get; set; }

        public System.Windows.Int32Rect UIRect { get; set; }
        public string Emotion1 { get; set; }
        public string Emotion2 { get; set; }
        public string Emotion3 { get; set; }
    }

    class DetectionEmotion
    {


        #region Video

        public bool flag { get; set; }
        public string g_Path { get; set; }
        public string imageFilePath { get; set; } 

        private FilterInfoCollection videoDevices;
        public VideoCaptureDevice videoSource;
        public int selectedDeviceIndex = 0;    
        private int saved = 0;

  

        public FilterInfoCollection GetDevices()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count != 0)
                {
                    Console.WriteLine("已找到设备");
                    return videoDevices;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("没找到设备" + ex.Message);
                return null;
            }
        }

        public VideoCaptureDevice VideoConnect(int deviceIndex = 0, int resoluution = 0)
        {
            if (videoDevices.Count <= 0) throw new ApplicationException();
            selectedDeviceIndex = deviceIndex;
            videoSource = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];
            videoSource.Start();
            Console.WriteLine("start");
            return videoSource;
        }
        public void GrabBitmap(string path)
        {
            if (videoSource == null)
                return;
            g_Path = path;
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);

        }

        public void CloseDevice()
        {
            videoSource.SignalToStop();
            videoSource.WaitForStop();
            Console.WriteLine("关闭设备");
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)eventArgs.Frame.Clone();
            string fullPath = g_Path + "temp\\";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
          //  saved = saved++;
            string img = fullPath + "temp" + saved + ".jpeg";
            //try
         //   { 
                Stream imageFileStream = File.Open(img,FileMode.OpenOrCreate,FileAccess.ReadWrite);
                bmp.Save(imageFileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                
                imageFileStream.Flush();
                imageFileStream.Close();
                imageFileStream.Dispose();
          //  }
         //   catch (Exception) { ;}
                
            flag = true;
            imageFilePath = img;
            Console.WriteLine(imageFilePath);
            videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);

        }

        #endregion

        /// <summary>
        /// Uploads the image to Project Oxford and detect emotions.
        /// </summary>
        /// <param name="imageFilePath">The image file path.</param>
        /// <returns></returns>
        public async Task<Emotion[]> UploadAndDetectEmotions(string imageFilePath)
        {

            string subscriptionKey = "22b1...";     // Enter your own subscription key here.
            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE STARTS HERE
            // -----------------------------------------------------------------------

            //
            // Create Project Oxford Emotion API Service client
            //
            EmotionServiceClient emotionServiceClient = new EmotionServiceClient(subscriptionKey);

            try
            {
                Console.WriteLine(imageFilePath);
                Emotion[] emotionResult;
                Console.WriteLine("dsf");
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    //
                    // Detect the emotions in the URL
                    //
                    Console.WriteLine("dsf");
                    emotionResult = await emotionServiceClient.RecognizeAsync(imageFileStream);
                    Console.WriteLine("dsf");
                    return emotionResult;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return null;
            }
            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE ENDS HERE
            // -----------------------------------------------------------------------

        }
    }
}
