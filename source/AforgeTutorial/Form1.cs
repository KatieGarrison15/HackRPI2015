using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge;

namespace AforgeTutorial
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection ListOfCams;
        private VideoCaptureDevice SelectedCam; //From where we will take image

        private HSLFiltering filter = new HSLFiltering();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListOfCams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            if (ListOfCams.Count == 0)
                return;

            comboBox1.Items.Clear();
            foreach (FilterInfo Cam in ListOfCams)
            {
                comboBox1.Items.Add(Cam.Name);
            }

        }

        private void StopCamera()
        {
            SelectedCam.SignalToStop();
            SelectedCam.Stop();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
                return;

            SelectedCam = new VideoCaptureDevice(ListOfCams[comboBox1.SelectedIndex].MonikerString);

            SelectedCam.NewFrame += new NewFrameEventHandler(SelectedCam_NewFrame);

            
            SelectedCam.Start();
        }

        void SelectedCam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone();

            //Reference : http://www.aforgenet.com/framework/docs/html/74331vc v1a9-6c27-972d-39d2-ddc383dd1dd4.htm
            
            // set color ranges to keep red-orange
            filter.Hue = new IntRange(0, 20);
            //c filter.Saturation = new DoubleRange(0.5, 1);
            
            // apply the filter
            filter.ApplyInPlace(image);
			
			// display in picture box
            pictureBox1.Image = image;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

    }
}
