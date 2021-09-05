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
using System.Windows.Threading;
using WpfDesktopUI.ViewModels;

namespace WpfDesktopUI.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        private bool videoIsPlaying = true;
        private bool sliderIsMoving = false;

        public PlayerView()
        {
            InitializeComponent();
            SetupTimer();
            SetupPlayer();
        }


        private void SetupTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }


        private void SetupPlayer()
        {
            Player.Volume = .5;
            Volume.Value = Player.Volume;
            VolumeStatus.Text = $"{(int)(Volume.Value * 100)}%";
            Player.Play();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if ((Player.Source != null) && (Player.NaturalDuration.HasTimeSpan) && (!sliderIsMoving))
            {
                Progress.Minimum = 0;
                Progress.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds;
                Progress.Value = Player.Position.TotalSeconds;
            }
        }


        private void Progress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            sliderIsMoving = true;
        }


        private void Progress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            sliderIsMoving = false;
            Player.Position = TimeSpan.FromSeconds(Progress.Value);
        }


        private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProgressStatus.Text = TimeSpan.FromSeconds(Progress.Value).ToString(@"hh\:mm\:ss");
        }


        private void Player_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((Player != null) && (Player.Source != null))
            {
                if (!videoIsPlaying)
                {
                    Player.Play();
                }
                else
                {
                    Player.Pause();
                }
                videoIsPlaying = !videoIsPlaying;
            }
        }


        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            Player.Volume = Volume.Value;
            VolumeStatus.Text = $"{(int)(Volume.Value * 100)}%";
        }
    }
}
