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
using System.Windows.Controls.Ribbon;
using System.Data;
using System.Windows.Forms;

using Microsoft.Expression.Encoder;
using System.IO;

namespace Deaf_T.V__V1_
{
    /// <summary>
    /// Interaction logic for Dictionary.xaml
    /// </summary>
    public partial class Dictionary 
    {
        public Dictionary()
        {
            InitializeComponent();
            btnPlay.Visibility = System.Windows.Visibility.Hidden;
            

           
            lstbxEngExpr = bindItems.Bind(lstbxEngExpr, dt_sign, "Expr");
            lstbxArbExpr = bindItems.Bind(lstbxArbExpr, dt_sign, "Arb_trnsl");
           
           
             //lstbxEngExpr.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Content", System.ComponentModel.ListSortDirection.Ascending));
           
        }

        Functions_cls.Bind_Items bindItems = new Functions_cls.Bind_Items();
        DB_cls.cls_searchSigns clsSearch = new DB_cls.cls_searchSigns();
        DB_cls.cls_ShowExpr clsShowExpr = new DB_cls.cls_ShowExpr();
        DataTable dt_sign = new DataTable();
        string videoPath = "";
     
        private void txtSrch_TextChanged(object sender, TextChangedEventArgs e)
        {
          
            //Search for a text that a user wants
              dt_sign = clsSearch.SearchSign(txtSrch.Text);
            //Fill the list by the corresponding searched words 
               lstbxArbExpr = bindItems.Bind(lstbxArbExpr, dt_sign, "Arb_trnsl");
               lstbxEngExpr = bindItems.Bind(lstbxEngExpr, dt_sign, "Expr");
               
           
            //lstbxEngExpr.DisplayMemberPath = "Expr";
            //lblPath.Text = dt_sign.Rows[0]["Sign_video_path"].ToString();}
           
       }
       

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //If the user clicks to show the video then checks if he search for a word or not
            if (txtSrch.Text != "")
            {
                //If the word exists in the database then show the appropriate sign video
                videoPath = dt_sign.Rows[0]["Sign_video_path"].ToString();
                mElmDic.Source = new Uri(dt_sign.Rows[0]["Sign_video_path"].ToString());
                //Play the video 
                mElmDic.LoadedBehavior = MediaState.Play;
            }
          

           
           
            
        }

        private void lstbxEngExpr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




            if (lstbxEngExpr.SelectedItem != null)
            {
                DataTable srch_dt = new DataTable();
                srch_dt = clsSearch.SearchSign(lstbxEngExpr.SelectedItem.ToString());

                //If the word exists in the database then show the appropriate sign video
                videoPath = srch_dt.Rows[0]["Sign_video_path"].ToString();
                 mElmDic.Source = new Uri(videoPath);
                //Play the video 
                mElmDic.LoadedBehavior = MediaState.Play;
               // lblPath.Text = srch_dt.Rows[0]["Sign_video_path"].ToString();
            }
           
            
           
           
           // mElmDic.Source = new Uri(@"C:\Users\Murtada Al-Junaid\Documents\Visual Studio 2013\Projects\Deaf T.V (V1) - Copy\Deaf T.V (V1)\bin\Debug\" + dt_sign.Rows[0]["Sign_video_path"].ToString());

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if ( videoPath != " ")
            {
               
                //btnSearch_Click(sender, e);
                mElmDic.LoadedBehavior = MediaState.Play;
               btnPlay.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                
                //DataTable srch_dt = clsSearch.SearchSign(lstbxEngExpr.SelectedValue.ToString());
                ////If the word exists in the database then show the appropriate sign video
                //mElmDic.Source = new Uri(srch_dt.Rows[0]["Sign_video_path"].ToString());
                ////Play the video 
                //mElmDic.LoadedBehavior = MediaState.Play;
                
                //lblPath.Text = srch_dt.Rows[0]["Sign_video_path"].ToString();
            }
            
           
        }

        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = System.Windows.Visibility.Visible;
            mElmDic.LoadedBehavior = MediaState.Stop;
        }
           

        private void Element_MediaOpened(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = System.Windows.Visibility.Hidden;

        }

       


        private void lstbxArbExpr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblPath.Text = lstbxArbExpr.SelectedItem.ToString();

            if (lstbxArbExpr.SelectedItem != null)
            {
                DataTable srch_dt = new DataTable();
                srch_dt = clsSearch.SearchSign(lstbxArbExpr.SelectedItem.ToString());

                //If the word exists in the database then show the appropriate sign video
                videoPath = srch_dt.Rows[0]["Sign_video_path"].ToString();
                mElmDic.Source = new Uri(videoPath);
                //Play the video 
                mElmDic.LoadedBehavior = MediaState.Play;
                //lblPath.Text = srch_dt.Rows[0]["Sign_video_path"].ToString();
            }
           
        }


        private void Mrg_Click()
        {

         

              

            try
            {
                // Create a job.
                using (Job job = new Job())
                {
                    
                   

                    // Make one MediaItem containing all of the sources.
                    MediaItem media_item = new MediaItem(dt_sign.Rows[0]["Sign_video_path"].ToString());
                    job.MediaItems.Add(media_item);

                    for (int i = 1; i < dt_sign.Rows.Count; i++)
                    {
                        media_item.Sources.Add( new Source(dt_sign.Rows[i]["Sign_video_path"].ToString()));
                    }

                    // Set the output directory.
                    FileInfo file_info = new FileInfo(@"C:\Users\Murtada Al-Junaid\Desktop\Yahia.mp4");
                    job.OutputDirectory = file_info.DirectoryName;

                    // Set the output file name.
                    media_item.OutputFileName = file_info.Name;

                    // Don't create a subdirectory.
                    job.CreateSubfolder = false;

                    // Use the original size.
                    media_item.OutputFormat.VideoProfile.Size = media_item.OriginalVideoSize;

                   

                    // Encode.
                    job.Encode();
                }
            }
            catch (Exception ex)
            {
                System.Windows. MessageBox.Show(ex.Message);
            }

          
         
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           //Mrg_Click();


           //mElmDic.Source = new Uri(@"C:\Users\Murtada Al-Junaid\Desktop\Yahia.avi");
            ////Play the video 
            //mElmDic.LoadedBehavior = MediaState.Play;
          
        }

        private void txtSrch_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

            
                
        }

        private void txtSrch_LayoutUpdated(object sender, EventArgs e)
        {
            

        }

        private void UserControl_LayoutUpdated(object sender, EventArgs e)
        {
            if (InputLanguage.CurrentInputLanguage.LayoutName == "Arabic (101)")
            {
                txtSrch.TextAlignment = TextAlignment.Right;
                TCLangs.SelectedIndex = 1;

            }
            else
            {
                txtSrch.TextAlignment = TextAlignment.Left;
                TCLangs.SelectedIndex = 0;
            }
        }

        // Display progress.
       
       
    }
}
