/*
 * AUTHOR: Suparna Deb
 * DATE LAST MODIFIED: 26/11/2021
 * FILE NAME: MainWindow.xaml.cs
 * PURPOSE: Handles events in response to the user's interaction with window 'MainWindow'
 * LAYER: Presentation
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NBMFilteringService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * Populate the dictionary with abbreviations and its full form
         * Populate incident list with the incidents
         */
        public MainWindow()
        {
            InitializeComponent();
            DAO.LoadDict();
            DAO.populateIncidentList();
            addBtn.IsEnabled = false;
            errorText.Text = "SELECT EXPORT DIRECTORY";
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMessage newMessage = new NewMessage();
            newMessage.Show();
        }

        /*
         * If SMS List btn clicked, populate list view with all the SMS objects in SMSList
         */
        private void SMSListBtn_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Message ID",
                DisplayMemberBinding = new Binding("ID")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Sender",
                DisplayMemberBinding = new Binding("Sender")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Subject",
                DisplayMemberBinding = new Binding("Subject")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Message Body",
                DisplayMemberBinding = new Binding("Text")
            });
            listView.ItemsSource = DAO.smsList;
        }

        /*
         * If Email List btn clicked, populate list view with all the Email objects in EmailList
         */
        private void EmailListBtn_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Message ID",
                DisplayMemberBinding = new Binding("ID")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Sender",
                DisplayMemberBinding = new Binding("Sender")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Subject",
                DisplayMemberBinding = new Binding("Subject")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Message Body",
                DisplayMemberBinding = new Binding("Text")
            });
            listView.ItemsSource = DAO.emailList;
        }

        /*
         * If Tweet List btn clicked, populate list view with all the Tweet objects in TweetList
         */
        private void TweetListBtn_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Message ID",
                DisplayMemberBinding = new Binding("ID")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Sender",
                DisplayMemberBinding = new Binding("Sender")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Message Body",
                DisplayMemberBinding = new Binding("Text")
            });
            listView.ItemsSource = DAO.tweetList;
        }

        /*
         * If Quarantine List btn clicked, populate list view with all the URL strings in quarantineList
         */
        private void quarantineBtn_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "URL",
            });
            listView.ItemsSource = DAO.quarantineList;
        }

        /*
         * If Hashtag List btn clicked, populate list view with all the Tag objects in groupedHashtagList
         */
        private void hashtagListBtn_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Hashtag",
                DisplayMemberBinding = new Binding("TagName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Count",
                DisplayMemberBinding = new Binding("Count")
            });
            listView.ItemsSource = DAO.groupedHashtagList;
        }

        /*
         * If Mentions List btn clicked, populate list view with all the Tag objects in groupedMentionList
         */
        private void mentionsListBtn_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Hashtag",
                DisplayMemberBinding = new Binding("TagName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Count",
                DisplayMemberBinding = new Binding("Count")
            });
            listView.ItemsSource = DAO.groupedMentionsList;
        }

        /*
         * If SIR List btn clicked, populate list view with all the SIR objects in SIRList
         */
        private void SIRListBtn_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Sort Code",
                DisplayMemberBinding = new Binding("SortCode")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Nature of Incident",
                DisplayMemberBinding = new Binding("NatureOfIncident")
            });
            listView.ItemsSource = DAO.SIRList;
        }

        /*
         * Allows user to set file path for message serialisation via UI
         */
        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                MessageProcess.setFilePath(dialog.SelectedPath);
            }

            if (!String.IsNullOrEmpty(MessageProcess.getFilePath()))
            {
                addBtn.IsEnabled = true;
                errorText.Visibility = Visibility.Hidden;
            }

        }

        /*
         * Allows user to retrieve file for message deserialisation via UI
         */
        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Import XML File",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xml",
                Filter = "xml files (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            openFileDialog1.ShowDialog();
            try
            {
                DAO.SMSDeserializer(openFileDialog1.FileName);
            } catch (Exception error)
            {
            }

            try
            {
                DAO.EmailDeserializer(openFileDialog1.FileName);
            }
            catch (Exception error)
            {
            }

            try
            {
                DAO.TweetDeserializer(openFileDialog1.FileName);
            }
            catch (Exception error)
            {
            }

        }
    }
}
