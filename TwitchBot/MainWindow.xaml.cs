using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.IO;
using MahApps.Metro.Controls;
using System.ComponentModel;

namespace TwitchBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        TextWriter _writer = null;
        ConnectionSettings conWin;

        public MainWindow()
        {
            InitializeComponent();

            // Instantiate the writer
            _writer = new TextBoxStreamWriter(ConsoleBox);
            // Redirect the out Console stream
            Console.SetOut(_writer);

            Console.WriteLine("Logging to console...");

            TextEntry.PreviewKeyDown += new KeyEventHandler(TextEntered);
            AddChannelTab.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(AddNewChannelTab);
        }

        private void TextEntered(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox s = (TextBox)sender as TextBox;
                Console.WriteLine(s.Text);
                s.Text = "";
            }
        }

        private void AddNewChannelTab(object sender, MouseEventArgs e)
        {
            TabItem newTab = new TabItem();


            //TODO: Add Stuff

            tabControl.Items.Add(newTab);
        }

        private void ConnectionSettings_Click(object sender, RoutedEventArgs e)
        {
            if (conWin == null)
                conWin = new ConnectionSettings();
            if (!conWin.IsVisible)
                conWin.Show();
            else
                conWin.Focus();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (conWin != null)
            {
                conWin.Close();
                conWin = null;
            }
            base.OnClosed(e);
        }

    }
}
