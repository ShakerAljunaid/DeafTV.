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
using System.Windows.Forms;
using System.IO;
using System.Windows.Threading;


namespace Deaf_T.V__V1_ 
{
    /// <summary>
    /// Interaction logic for UCMediaPlayer.xaml
    /// </summary>
    public partial class UCMediaPlayer 
    {
        public UCMediaPlayer()
        {
            InitializeComponent();

           //mdElmDf.Source = new Uri(@"C:\Users\Murtada Al-Junaid\Desktop\C# WPF Tutorial 6- Login Form using sqlite in C# WPF application PART-1-2.mp4");
         // mdElm.Source = new Uri(@"C:\Users\Murtada Al-Junaid\Desktop\C# WPF Tutorial 6- Login Form using sqlite in C# WPF application PART-1-2.mp4");
          InitializePropertyValues();
          timer = new DispatcherTimer();
          timer.Interval = TimeSpan.FromSeconds(1);
          timer.Tick += new EventHandler(timer_Tick);
        //  mdElm.LoadedBehavior = MediaState.Manual;
         // mdElm.MediaOpened += new RoutedEventHandler(Element_MediaOpened);

          
           
        }


       

        //Var used to change the play and pause btns
        Boolean isDragging;

         private DispatcherTimer timer;

        //Enables the user to stop the video
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

            mdElm.LoadedBehavior = MediaState.Stop;
            mdElmDf.LoadedBehavior = MediaState.Stop;
        }



        public void btnLoad()
       {   //Load the video that u want to run
        //    OpenFileDialog ofd = new OpenFileDialog(); ;
        //    ofd.AddExtension = true;
        //    ofd.DefaultExt = "*.*";
        //    ofd.Filter = "Media (*.*) |*.*";
        //    ofd.ShowDialog();

        //    //Assing the loaded video to ur media player
        //    mdElm.Source = new Uri(ofd.FileName);
        //    mdElmDf.Source = new Uri(ofd.FileName);

            //Call initialing function to set the loaded videos to a default range of time and volume
          

           
        }

        private void ucMediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Width = mw.tbMediaPlayer.Width;
            this.Height = mw.tbMediaPlayer.Height;
        }

        public void Element_MediaOpened(object sender, RoutedEventArgs e)
        {


            if (mdElm.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = mdElm.NaturalDuration.TimeSpan;
              
                timelineSlider.Maximum = ts.TotalSeconds;
                timelineSlider.SmallChange = 1;
                timelineSlider.LargeChange = Math.Min(12, ts.Seconds / 10);
                lblTotal.Content = (DateTime.Today + mdElm.NaturalDuration.TimeSpan).ToString("HH:mm:ss");
            } 
            timer.Start();
          
           
            timer.Start();

            //lblPeriod.Content = String.Format("{0} / {1}", mdElm.Position.ToString(@"mm\:ss"), mdElm.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
        }


        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {
            mdElmDf.Stop();
            mdElm.Stop();
        }


        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.

        // Function  to set the sound of the played videos to the sound set by the user
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            try
            {
                mdElm.Volume = (double)volumeSlider.Value;

                mdElmDf.Volume = (double)volumeSlider.Value;

            }
            catch (Exception)
            {

                throw;
            }


        }


        private void timelineSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void timelineSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mdElm.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            isDragging = false;
        }
        //Play the video after it's been loaded
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            mdElm.LoadedBehavior = MediaState.Play;
            mdElmDf.LoadedBehavior = MediaState.Play;

        }

        //Initialize the volum and time of the videos to the default values
      public  void InitializePropertyValues()
        {
            // Set the media's starting Volume  to the current value of the
            // their respective slider controls.
            mdElm.Volume = (double)0.5;
            mdElmDf.Volume = (double)0.5;
            volumeSlider.Value = 0.5;



        }

      private void timer_Tick(object sender, EventArgs e)
      {

          if (!isDragging)
          {
              timelineSlider.Value = mdElm.Position.TotalSeconds;

              string formatted = (DateTime.Today + mdElm.Position).ToString("HH:mm:ss");

              lblPeriod.Content = formatted;
          }

        
         
      }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mdElm.LoadedBehavior = MediaState.Pause;
            mdElmDf.LoadedBehavior = MediaState.Pause;
        }

        //Enables the user to move the videos to any position he wants
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;


            mdElm.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            //TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            //mdElm.Position = ts;
            //lblPeriod.Content = String.Format("{0} / {1}", mdElm.Position.ToString(@"mm\:ss"), mdElm.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
          //  mdElmDf.Position = ts;
        }

        private void mdElm_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            
        }

        private void mdElm_Drop(object sender, System.Windows.DragEventArgs e)
        {
         
        }

    
   
       
    }
}
