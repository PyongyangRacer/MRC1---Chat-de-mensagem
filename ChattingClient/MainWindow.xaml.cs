using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace ChattingClient
{

    public partial class MainWindow : Window
    {
        public static IChattingService Server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChattingServiceEndPoint");
            Server = _channelFactory.CreateChannel();


        }

        public void TakeMessage(string message, string userName)
        {
            TextDisplayTextBox.Text += "[" + DateTime.Now.ToString("HH:mm:ss tt") + "]" + userName + ": " + message + "\n";
            TextDisplayTextBox.ScrollToEnd();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Server.SendMessageToALL(MessageTextBox.Text, UserNameTextBox.Text);
            TakeMessage(MessageTextBox.Text, "Você");
            MessageTextBox.Text = "";
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            int returnValue = Server.Login(UserNameTextBox.Text);
            if (returnValue == 1)
            {
                MessageBox.Show("Você já está logado");
            }
            else if (returnValue == 0)
            {
                MessageBox.Show("Logado com sucesso!");
                UserNameTextBox.IsEnabled = false;
                LoginButton.IsEnabled = false;
            }
        }
    }
}
