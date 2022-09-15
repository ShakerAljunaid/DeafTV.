using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;
using System.Runtime.InteropServices;


using System.Windows.Threading;
namespace Deaf_T.V__V1_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();


            this.tbMediaPlayer.Content = umMDp;
            this.tbDictionary.Content = umDic;
            mouseTimer = new DispatcherTimer();
            mouseTimer.Interval = TimeSpan.FromSeconds(3);
            mouseTimer.Tick += new EventHandler(mouseTimer_Tick);
            umMDp.mdElm.LoadedBehavior = MediaState.Manual;
            umMDp.mdElm.MediaOpened += new RoutedEventHandler(umMDp.Element_MediaOpened);
            
        }

        //Var used to change the play and pause btns
        private bool fullscreen = false;
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();
        UCMediaPlayer umMDp = new UCMediaPlayer();
        Dictionary umDic = new Dictionary();
        private DispatcherTimer mouseTimer;

        public string StartupMovie { get; set; }
        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  

            //This is only used to alternate btwn the media player tab and dictionary tab when clkin' on of them
            if(RbnMenu.SelectedIndex==0)
            
                TCMain.SelectedItem = tbMediaPlayer;
             else
                TCMain.SelectedItem = tbDictionary;

        }



        private void mouseTimer_Tick(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                mouseTimer.Stop();
                Cursor = System.Windows.Input.Cursors.None;
                umMDp.brdControls.Opacity = 0;
                
            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    Cursor = System.Windows.Input.Cursors.Hand;
                    mouseTimer.Interval = TimeSpan.FromSeconds(4);
                    mouseTimer.Start();
                    umMDp.brdControls.Opacity = 1;
                    break;
                case WindowState.Minimized:
                    Cursor = System.Windows.Input.Cursors.Arrow;
                    break;
                case WindowState.Normal:
                    Cursor = System.Windows.Input.Cursors.Arrow;
                    break;
                default:
                    break;
            }
        }
        private void MPTab(object sender, RoutedEventArgs e)
        {
            
        }

       
        private void DicTab(object sender, RoutedEventArgs e)
        {
            //var DictinaryTab = new Dictionary();

            //if (!clk)
            //{
                
            //    this.tabControl1.SelectedIndex =
            //   this.tabControl1.Items.Add(
            //   new TabItem { Header = "Dictionary", Content = DictinaryTab, Name = "DicTab" });

            //}
            //else
            //{
            //    this.tabControl1.SelectedIndex = 1;

            //}

        }

        private void Dic_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           
        }

        private void Dic_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
           
        }

        private void Mediaplayer_Click(object sender, RoutedEventArgs e)
        {

            //Chng the tab to the media player tab when clkin' on this btn
            TCMain.SelectedItem = tbMediaPlayer;
            RbnMenu.SelectedItem = Mediaplayer;
            
        }

        private void Dictinary_Click(object sender, RoutedEventArgs e)
        {
            //Chng the tab to the dictionary tab when clkin' on this btn
            TCMain.SelectedItem = tbDictionary;
            RbnMenu.SelectedItem = Dictinary;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //Load the video that u want to run
            OpenFileDialog ofd = new OpenFileDialog(); ;
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Media (*.*) |*.*";
            ofd.ShowDialog();
            umMDp.mdElm.Source = new Uri(ofd.FileName);
            //Assing the loaded video to ur media player
            //
            //mdElmDf.Source = new Uri(ofd.FileName);

            //Call initialing function to set the loaded videos to a default range of time and volume
            umMDp.InitializePropertyValues();
             
          
              

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About ab = new About();
            ab.Show();
        }
         

        private void toggleFulScreen()
        {
            if (!fullscreen)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                
                 
                umMDp.grdMain.Width = this.Width -20;
                umMDp.grdMain.Height = this.Height - 90;
                
                //mdElm.Width = tbMediaPlayer.Width;
                //mdElm.Height = tbMediaPlayer.Height;
                
                
                RbnMenu.Visibility = System.Windows.Visibility.Collapsed;




            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                umMDp.grdMain.Width = tbMediaPlayer.Width;
                umMDp.grdMain.Height = tbMediaPlayer.Height;
                umMDp.brdControls.Opacity = 1;
                RbnMenu.Visibility = System.Windows.Visibility.Visible;

            }

            fullscreen = !fullscreen;


        }
        public void mdElm_MouseDown(object sender, MouseButtonEventArgs e)
        {


            if (e.ClickCount%2==0)
            {
                toggleFulScreen();
            
               
            }


        }

        private void Window_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                if (files.Count() == 1)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(files[0]);
                    if (file.Extension == "avi" ||
                        file.Extension == "vmw" ||
                        file.Extension == "mov")
                    {
                        e.Effects = System.Windows.DragDropEffects.All;
                    }
                }
                else
                {
                    e.Effects = System.Windows.DragDropEffects.None;
                }
            }
        }

        private void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
            if (files.Count() == 1)
            {
                System.IO.FileInfo file = new System.IO.FileInfo(files[0]);
                umMDp. mdElm.Source = new Uri(file.FullName);
                //OpenMovie(file);
            }
        }

        
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            umMDp.volumeSlider.Value = umMDp.volumeSlider.Value + ((e.Delta / 120) * 0.05);
        }

        private void isfullScreen()
        {
            if (fullscreen)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                umMDp.grdMain.Width = tbMediaPlayer.Width;
                umMDp.grdMain.Height = tbMediaPlayer.Height;

                RbnMenu.Visibility = System.Windows.Visibility.Visible;


                fullscreen = !fullscreen;


            }






        }

        private void toggleState()
        {
            if(umMDp.mdElm.LoadedBehavior!= MediaState.Pause)
           
                umMDp.mdElm.LoadedBehavior = MediaState.Pause;
            else
             umMDp.mdElm.LoadedBehavior = MediaState.Play;



            


        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    isfullScreen();
                    break;
                case Key.MediaStop:
                    umMDp.mdElm.Stop();
                    break;
                case Key.Left:
                    umMDp.mdElm.Position += TimeSpan.FromSeconds(-15);
                    break;
                case Key.Right:
                    umMDp.mdElm.Position += TimeSpan.FromSeconds(15);
                    break;
                case Key.Up:
                    umMDp.volumeSlider.Value = umMDp.volumeSlider.Value + (0.1);
                    break;
                case Key.Down:
                    umMDp.volumeSlider.Value = umMDp.volumeSlider.Value - (0.1);
                    break;

                case Key.Enter:
                    toggleFulScreen();
                    break;
                case Key.Space:
                    toggleState();
                    break;
                  case Key.VolumeDown:
                    umMDp.volumeSlider.Value = umMDp.volumeSlider.Value - (0.3);
                   break;
                case Key.VolumeUp:
                   umMDp.volumeSlider.Value = umMDp.volumeSlider.Value + (0.3);
                   break;
                case Key.MediaPlayPause:
                   umMDp.mdElm.Pause();
                   break;
               
             

            }
        }

      

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


        private void PlayMovie(System.IO.FileInfo VidFile)
        {

            MediaPlayer.Video openedVideo = new MediaPlayer.Video
            {
                Path = VidFile.FullName,
                LastComputer = Environment.MachineName,
                LastPosition = 0,
                MovieId = Guid.NewGuid()
            };




            umMDp.mdElm.Source = new Uri(openedVideo.Path);
            umMDp.mdElm.Play();
            
            System.IO.FileInfo fi = null;
            if (!string.IsNullOrEmpty(openedVideo.Path))
                fi = new System.IO.FileInfo(openedVideo.Path);

            this.Title +=  fi.Name;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StartupMovie))
                System.Windows.MessageBox.Show("No file opened");
            else
                PlayMovie(new System.IO.FileInfo(StartupMovie));

           
        }

     

       

        //[DllImport("user32.dll")]
        //private static extern uint GetDoubleClickTime();

       




       
       
          

     
    }//End of the class
}
