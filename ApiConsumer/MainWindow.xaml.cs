using Models;
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

namespace ApiConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string token;
        public MainWindow()
        {
            InitializeComponent();

            Login();
        }

        public async Task Login()
        {
            PasswordWindow pw = new PasswordWindow();
            if (pw.ShowDialog() == true)
            {
                RestService restservice = new RestService("https://localhost:7766/", "/Auth");
                TokenViewModel tvm = await restservice.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = pw.UserName,
                    Password = pw.Password
                });
                token = tvm.Token;
                
                GetPlayLists();
            }
            else
            {
                this.Close();
            }
        }

        public async Task GetPlayLists()
        {
            cbox.ItemsSource = null;
            RestService restservice = new RestService("https://localhost:7766/", "/Device", token);
            IEnumerable<Device> devices =
                await restservice.Get<Device>();

            cbox.ItemsSource = devices;
            cbox.SelectedIndex = 0;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Video newvideo = new Video()
            //{
            //    Title = tb_title.Text,
            //    YoutubeId = tb_youtube.Text,
            //    Rating = 5,
            //    PlayListUid = (cbox.SelectedItem as Playlist).UID
            //};

            //RestService restservice = new RestService("https://localhost:7766/", "/Video", token);
            //restservice.Post(newvideo);
            //GetPlayListNames().Wait();
        }
    }
}
