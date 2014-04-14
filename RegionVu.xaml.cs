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
using System.Windows.Navigation;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

namespace GettingStarted
{
    /// <summary>
    /// Interaction logic for RegionVu.xaml
    /// </summary>
    public partial class RegionVu : Window
    {
        private KinectSensorChooser sensorChooser;
        public int numberOfStates = 2;
        public string filename = "artAssets/EastCoast.png";
        public string[] StateNames = new string[16];
        public string[] StateFileNames = new string[16];
        public string StatName = "artAssets/TotalUSAStats.png";

        ImageBrush temp;
        public RegionVu()
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

            this.RegionBackground.Source = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute));

            //fill scroll content
            for (int i = 0; i < numberOfStates; i++)
            {
                //temp = new ImageBrush(new BitmapImage(new Uri(filename, UriKind.Relative)));

                var button = new KinectCircleButton
                {
                    //Background = temp,
                    
                    Content = StateNames[i],
                    Foreground = System.Windows.Media.Brushes.LightSkyBlue,
                    //BorderBrush = System.Windows.Media.Brushes.Black,
                    Height = 300
                };

                string chartName = "";
                if (StateNames[i] == "NY") { chartName = "artAssets/NewYorkChart.png"; }
                else if (StateNames[i] == "HI") { chartName = "artAssets/HawaiiChart.png"; }
                else if (StateNames[i] == "WA") { chartName = "artAssets/WashingtonChart.png";  }
                else if (StateNames[i] == "FL") { chartName = "artAssets/Florida.png"; }
                else if (StateNames[i] == "IL") { chartName = "artAssets/IllinoisChart.png"; }
                else { chartName = "artAssets/temp.png"; }
                int i1 = i;
                button.Click +=
                    (o, args) =>
                    {
                        this.sensorChooser.Stop();
                        StateVu State = new StateVu();
                        State.StateName = StateFileNames[i1];
                        State.ChartName = chartName;
                        State.ShowDialog();
                        this.Close();
                    };// MessageBox.Show(StateNames[i] + i1);

                scrollContent.Children.Add(button);
            }
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
        private void StatsOnClick(object sender, RoutedEventArgs e)
        {
            this.sensorChooser.Stop();
            statistics StatScreen = new statistics();
            StatScreen.filename = StatName;
            StatScreen.ShowDialog();
            this.Close();
        }
    }
    }

