using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
 using AForge.Imaging.Filters;


namespace Aforgetry
{

    public partial class Form1 : Form
    {
        public FilterInfoCollection LaptopCams;
        public VideoCaptureDevice cam = null;


        public int red = 0;
        public int green = 0;
        public int blue = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // Button 1 is list
        private void button1_Click(object sender, EventArgs e)
        {

            // Filter list of devices with video ability
            LaptopCams = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // Display name of each camera in popup
            foreach (FilterInfo camera in LaptopCams)
            {
                MessageBox.Show(camera.Name);
            }

        }


        // Button 2 is start
        private void button2_Click(object sender, EventArgs e)
        {

            // These from before
            LaptopCams = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // Why are you camera 4
            cam = new VideoCaptureDevice(LaptopCams[0].MonikerString);


            // When recieve each frame, function "cam_NewFrame" will be called
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

            // StartPosition the camera
            cam.Start();

        }

        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            Bitmap sourceImage = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = ApplyRGBFilter(sourceImage);    //Applying filter and show in picture box

        }

        // Button 3 is stop
        private void button3_Click(object sender, EventArgs e)
        {

            cam.Stop();  // Stop the camera

        }

        private void SetTrackBarProperties()
        {
            trackBar1.Maximum = 255;
            trackBar2.Maximum = 255;
            trackBar3.Maximum = 255;

            trackBar1.Minimum = 0;
            trackBar2.Minimum = 0;
            trackBar3.Minimum = 0;

            trackBar1.TickFrequency = 3;
            trackBar2.TickFrequency = 3;
            trackBar3.TickFrequency = 3;

        }

       

        private Bitmap ApplyRGBFilter(Bitmap sourceImage)
        {

            ColorFiltering filter = new ColorFiltering();
            filter.Red = new AForge.IntRange(50, red);
            filter.Green = new AForge.IntRange(50, green);
            filter.Blue = new AForge.IntRange(50, blue);
            Bitmap processedImage = filter.Apply(sourceImage);
            filter.ApplyInPlace(processedImage);
            return processedImage;
        }

       // public class ColorFiltering : BaseInPlacePartialFilter
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            red = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            green = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            blue = trackBar3.Value;
        }
    }

}
    


