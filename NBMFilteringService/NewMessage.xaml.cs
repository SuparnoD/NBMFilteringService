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

namespace NBMFilteringService
{
    /// <summary>
    /// Interaction logic for NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        public NewMessage()
        {
            InitializeComponent();
            validateMessage();
            errorText.Visibility = Visibility.Hidden;
            SIRCheck.Visibility = Visibility.Hidden;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = idBox.Text;
            string send = senderBox.Text;
            string subject = subjectBox.Text;
            string body = messageBox.Text;

            id = id.ToUpper();
            string id2 = id.Substring(1);
            bool isNumeric = int.TryParse(id2, out _);

            if((id.Length != 10) || (!isNumeric))
            {
                MessageBox.Show("Error");
            } else
            {
                MessageProcess.CategoriseMessage(id);
                MessageProcess.SanitiseMessage(id, send, subject, body);
                MessageProcess.ResetType();
                MessageProcess.GroupHashtagList();
                this.Close();
            }
        }

        private void idBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateMessage();
        }

        private void validateMessage()
        {
            string id = idBox.Text.ToUpper();
            string send = senderBox.Text;
            string subject = subjectBox.Text;
            string body = messageBox.Text;

            // ensures the ID starts with S, E or T for the system to allocate it a type. else return error and disable add button
            if ((!id.StartsWith("S")) && (!id.StartsWith("E")) && (!id.StartsWith("T")))
            {
                errorText.Text = "ID should start with S, E or T";
                senderBox.IsEnabled = false;
                subjectBox.IsEnabled = false;
                messageBox.IsEnabled = false;
                addBtn.IsEnabled = false;
                errorText.Visibility = Visibility.Visible;
            }
            else
            {
                errorText.Visibility = Visibility.Hidden;
                senderBox.IsEnabled = true;
                subjectBox.IsEnabled = true;
                messageBox.IsEnabled = true;
                addBtn.IsEnabled = true;
            }

            // if message type is of tweet, disable the subject field, else keep it enabled
            if (id.StartsWith("T"))
            {
                subjectBox.IsEnabled = false;
                subjectBox.Clear();
            } 

            // if message type is of email, present the option to flag as significant incident report
            if (id.StartsWith("E"))
            {
                SIRCheck.Visibility = Visibility.Visible;
            } else
            {
                SIRCheck.Visibility = Visibility.Hidden;
            }

            // ensures that tweet/sms message body is within the 140 character limit. otherwise return error and disable the add button
            if(id.StartsWith("S") || (id.StartsWith("T")))
            {
                if(body.Length > 140)
                {
                    addBtn.IsEnabled = false;
                    errorText.Visibility = Visibility.Visible;
                    errorText.Text = "Tweet/SMS are a maximum of 140 characters";
                } else
                {
                    addBtn.IsEnabled = true;
                    errorText.Visibility = Visibility.Hidden;
                }
            }

            //ensures that email subject is within 20 character limit and email message body is within 1028 character limit. otherwise return error and disable the add button
            if (id.StartsWith("E"))
            {
                if((subject.Length > 20) || (body.Length > 1028))
                {
                    addBtn.IsEnabled = false;
                    errorText.Visibility = Visibility.Visible;
                    errorText.Text = "Email subjects max limit is 20 chars. Message text max limit is 1028 chars.";
                } else
                {
                    addBtn.IsEnabled = true;
                    errorText.Visibility = Visibility.Hidden;
                }
            }
        }

        private void SIRCheck_Checked(object sender, RoutedEventArgs e)
        {
            subjectBox.Text = "SIR dd/mm/yy";
            messageBox.Text = "Sort Code: \n" +
                "Nature of Incident: ";
        }

        private void SIRCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            subjectBox.Clear();
            messageBox.Clear();
        }

        private void messageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateMessage();
        }

        private void subjectBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateMessage();
        }

        private void senderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateMessage();
        }
    }
}
