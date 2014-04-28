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
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

namespace GettingStarted
{
    /// <summary>
    /// Interaction logic for StateVu.xaml
    /// </summary>
    public partial class StateVu : Window
    {
        private KinectSensorChooser sensorChooser;
        public string StateName = "";
        string ChartName = "";
        public string whichRegion = "";
        public string prevRegionStats = "";
        public string backButtonString = "";
        public string StateInitial = "";
        public int prevNumofStates = 0;
        public string[] prevStateNames = new string[16];
        public string[] prevStateFileNames = new string[16];

        public StateVu()
        {
            InitializeComponent();
            Loaded += OnLoaded;

        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //DiscoverSensor();
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();

            //this.StateImage.Source = new BitmapImage(new Uri(StateName, UriKind.RelativeOrAbsolute));
            this.StateImage.Source = new BitmapImage(new Uri(StateName, UriKind.RelativeOrAbsolute));

            ChartName = "artAssets/Charts/" + StateInitial + ".png";

            this.ChartImage.Source = new BitmapImage(new Uri(ChartName, UriKind.RelativeOrAbsolute));
            ImageBrush mybrush = new ImageBrush();
            mybrush.ImageSource = new BitmapImage(new Uri(backButtonString, UriKind.Relative));
            this.BackButton.Background = mybrush;


        }
        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            bool error = false;
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                    error = true;
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    error = true;
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (!error)
                kinectRegion.KinectSensor = args.NewSensor;
        }


        private void HomeOnClick(object sender, RoutedEventArgs e)
        {
            this.sensorChooser.Stop();
            USAVu home = new USAVu();
            home.Show();
            this.Close();
        }
        private void BackOnClick(object sender, RoutedEventArgs e) 
        {
            //eventually go back to one of the region screens
            this.sensorChooser.Stop();
            RegionVu parentRegion = new RegionVu();
            parentRegion.filename = whichRegion;
            parentRegion.StatName = prevRegionStats;
            parentRegion.numberOfStates = prevNumofStates;
            parentRegion.StateNames = prevStateNames;
            parentRegion.StateFileNames = prevStateFileNames;
            parentRegion.superbackbuttonstring = backButtonString;
            parentRegion.Show();
            this.Close();
        }
    }
}
