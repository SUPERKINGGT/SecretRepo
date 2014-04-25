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
    /// Interaction logic for USAVu.xaml
    /// </summary>
    public partial class USAVu : Window
    {
        private KinectSensorChooser sensorChooser;

        public USAVu()
        {
            InitializeComponent();
            Loaded += OnLoaded;

        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {

            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();

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

        private void WestOnClick(object sender, RoutedEventArgs e)
        {
            //WESTCOAST
            //this.sensorChooser.Stop();
            this.sensorChooser.Stop();
            RegionVu reg = new RegionVu();
            //reg.se
            reg.numberOfStates = 11;
            reg.StateNames[0] = "WA"; reg.StateFileNames[0] = "artAssets/WashingtonButton.png";
            reg.StateNames[1] = "OR"; reg.StateFileNames[1] = "artAssets/OregonButton.png";
            reg.StateNames[2] = "CA"; reg.StateFileNames[2] = "artAssets/CaliforniaButton.png";
            reg.StateNames[3] = "NV"; reg.StateFileNames[3] = "artAssets/Nevada.png";
            reg.StateNames[4] = "AZ"; reg.StateFileNames[4] = "artAssets/arizonaButton.png";
            reg.StateNames[5] = "NM"; reg.StateFileNames[5] = "artAssets/newMexicoButton.png";
            reg.StateNames[6] = "UT"; reg.StateFileNames[6] = "artAssets/UtahButton.png";
            reg.StateNames[7] = "CO"; reg.StateFileNames[7] = "artAssets/colorodoButton.png";
            reg.StateNames[8] = "ID"; reg.StateFileNames[8] = "artAssets/IdahoButton.png";
            reg.StateNames[9] = "WY"; reg.StateFileNames[9] = "artAssets/wyomingButton.png";
            reg.StateNames[10] = "MT"; reg.StateFileNames[10] = "artAssets/MontanaButton.png";

            reg.filename = "artAssets/WestCoast.png";
            reg.StatName = "artAssets/WestCoastStats.png";
            reg.superbackbuttonstring = "artAssets/WestButton.png";
            reg.regionColor = System.Windows.Media.Brushes.Orange;

            reg.ShowDialog();
            this.Close();
        }
        private void MidWestOnClick(object sender, RoutedEventArgs e)
        {
            //MIDWEST
            this.sensorChooser.Stop();
            RegionVu reg = new RegionVu();
            reg.numberOfStates = 12;
            reg.numberOfStates = 11;
            reg.StateNames[0] = "ND"; reg.StateFileNames[0] = "artAssets/NorthDakotaButton.png";
            reg.StateNames[1] = "SD"; reg.StateFileNames[1] = "artAssets/SouthDakotaButton.png";
            reg.StateNames[2] = "NE"; reg.StateFileNames[2] = "artAssets/NebraskaButton.png";
            reg.StateNames[3] = "KS"; reg.StateFileNames[3] = "artAssets/KansasButton.png";
            reg.StateNames[4] = "MN"; reg.StateFileNames[4] = "artAssets/MinnesotaButton.png";
            reg.StateNames[5] = "IL"; reg.StateFileNames[5] = "artAssets/IllinoisButton.png";
            reg.StateNames[6] = "IA"; reg.StateFileNames[6] = "artAssets/IowaButton.png";
            reg.StateNames[7] = "MI"; reg.StateFileNames[7] = "artAssets/MichiganButton.png";
            reg.StateNames[8] = "WI"; reg.StateFileNames[8] = "artAssets/WisconsinButton.png";
            reg.StateNames[9] = "IN"; reg.StateFileNames[9] = "artAssets/IndianaButton.png";
            reg.StateNames[10] = "MO"; reg.StateFileNames[10] = "artAssets/MissouriButton.png";
            reg.StateNames[11] = "IA"; reg.StateFileNames[11] = "artAssets/IowaButton.png";

            reg.filename = "artAssets/midwest.png";
            reg.StatName = "artAssets/MidWestStats.png";
            reg.superbackbuttonstring = "artAssets/midWestButton.png";
            reg.regionColor = System.Windows.Media.Brushes.Gold;

            reg.ShowDialog();
            this.Close();
        }
        private void EastOnClick(object sender, RoutedEventArgs e)
        {
            //EASTCOAST
            this.sensorChooser.Stop();
            RegionVu reg = new RegionVu();
            reg.numberOfStates = 9;
            reg.StateNames[0] = "ME"; reg.StateFileNames[0] = "artAssets/mainButton.png";
            reg.StateNames[1] = "VT"; reg.StateFileNames[1] = "artAssets/vermontButton.png";
            reg.StateNames[2] = "NH"; reg.StateFileNames[2] = "artAssets/newHampshireButton.png";
            reg.StateNames[3] = "MA"; reg.StateFileNames[3] = "artAssets/massachusettsButton.png";
            reg.StateNames[4] = "NY"; reg.StateFileNames[4] = "artAssets/newYorkButton.png";
            reg.StateNames[5] = "RI"; reg.StateFileNames[5] = "artAssets/rhodeIsland.png";
            reg.StateNames[6] = "PA"; reg.StateFileNames[6] = "artAssets/pennsylvaniaButton.png";
            reg.StateNames[7] = "NJ"; reg.StateFileNames[7] = "artAssets/newJerseyButton.png";
            reg.StateNames[8] = "CT"; reg.StateFileNames[8] = "artAssets/ConnecticutButton.png";

            reg.filename = "artAssets/EastCoast.png";
            reg.StatName = "artAssets/NorthEastStats.png";
            reg.superbackbuttonstring = "artAssets/EastCoastButton.png";
            reg.regionColor = System.Windows.Media.Brushes.Indigo;

            reg.ShowDialog();
            this.Close();
        }
        private void southOnClick(object sender, RoutedEventArgs e)
        {
            //SOUTH
            this.sensorChooser.Stop();
            RegionVu reg = new RegionVu();
            reg.numberOfStates = 16;
            reg.StateNames[0] = "DE"; reg.StateFileNames[0] = "artAssets/DelawareButton.png";
            reg.StateNames[1] = "MD"; reg.StateFileNames[1] = "artAssets/MarylandButton.png";
            reg.StateNames[2] = "VA"; reg.StateFileNames[2] = "artAssets/VirginiaButton.png";
            reg.StateNames[3] = "WV"; reg.StateFileNames[3] = "artAssets/westVirginaButton.png";
            reg.StateNames[4] = "NC"; reg.StateFileNames[4] = "artAssets/northCarolina.png";
            reg.StateNames[5] = "SC"; reg.StateFileNames[5] = "artAssets/southCarolinaButton.png";
            reg.StateNames[6] = "KY"; reg.StateFileNames[6] = "artAssets/kentuckyButton.png";
            reg.StateNames[7] = "TN"; reg.StateFileNames[7] = "artAssets/tennesseeButton.png";
            reg.StateNames[8] = "GA"; reg.StateFileNames[8] = "artAssets/GeorgiaButton.png";
            reg.StateNames[9] = "AL"; reg.StateFileNames[9] = "artAssets/AlabamaButton.png";
            reg.StateNames[10] = "AR"; reg.StateFileNames[10] = "artAssets/ArkansasButton.png";
            reg.StateNames[11] = "MS"; reg.StateFileNames[11] = "artAssets/MississippiButton.png";
            reg.StateNames[12] = "LA"; reg.StateFileNames[12] = "artAssets/LouisianaButton.png";
            reg.StateNames[13] = "FL"; reg.StateFileNames[13] = "artAssets/FloridaButton.png";
            reg.StateNames[14] = "OK"; reg.StateFileNames[14] = "artAssets/oklahomaButton.png";
            reg.StateNames[15] = "TX"; reg.StateFileNames[15] = "artAssets/texasButton.png";

            reg.filename = "artAssets/south.png";
            reg.StatName = "artAssets/DirtySouthStats.png";
            reg.superbackbuttonstring = "artAssets/southButton.png";
            reg.regionColor = System.Windows.Media.Brushes.Firebrick;

            reg.ShowDialog();
            this.Close();
        }
        private void AlaskaOnClick(object sender, RoutedEventArgs e)
        {
            //ALASKA
            this.sensorChooser.Stop();
            RegionVu reg = new RegionVu();
            reg.numberOfStates = 2;
            reg.StateNames[0] = "AK"; reg.StateFileNames[0] = "artAssets/AlaskaStateButton.png";
            reg.StateNames[1] = "HI"; reg.StateFileNames[1] = "artAssets/HawaiiButton.png";
            reg.filename = "artAssets/Alaska.png";
            reg.StatName = "artAssets/pacificStats.png";
            reg.superbackbuttonstring = "artAssets/AlaskaButton.png";

            reg.regionColor = System.Windows.Media.Brushes.ForestGreen;

            reg.ShowDialog();
            this.Close();
        }
        private void StatsOnClick(object sender, RoutedEventArgs e)
        {
            //HomeStats
            this.sensorChooser.Stop();
            statistics StatScreen = new statistics();
            StatScreen.filename = "artAssets/TotalUSAStats.png";
            StatScreen.ShowDialog();
            this.Close();
        }
    }
}
