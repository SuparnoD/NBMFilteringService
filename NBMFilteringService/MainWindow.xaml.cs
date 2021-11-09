﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            MessageProcess.LoadDict();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMessage newMessage = new NewMessage();
            newMessage.Show();
        }

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
    }
}
