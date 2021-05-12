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
        private List<Device> devices = new List<Device>();
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
                TokenViewModel tvm;
                if (pw.IsRegist)
                {
                    await restservice.Post<TokenViewModel, RegisterViewModel>(new RegisterViewModel()
                    {
                        Email = pw.UserName,
                        Password = pw.Password
                    });

                }

                tvm = await restservice.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = pw.UserName,
                    Password = pw.Password
                });

                token = tvm.Token;

                GetDevices();
            }
            else
            {
                this.Close();
            }
        }
        

        public async Task GetDevices()
        {
            listbox.ItemsSource = null;
            RestService restservice = new RestService("https://localhost:7766/", "/Device", token);
            devices =
                await restservice.Get<Device>();

            listbox.ItemsSource = devices.AsEnumerable();
            listbox.SelectedIndex = 0;
            
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

        private void Click_Item(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_BackToLogin(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Button_Click_AddDevice(object sender, RoutedEventArgs e)
        {
            Device newDevice = new Device()
            {
                DescriptionTabData = new DescriptionTabData() { Name = "New Device" }
            };

            RestService restservice = new RestService("https://localhost:7766/", "/Device", token);
            restservice.Post(newDevice);
            GetDevices();
        }

        private void Button_Click_RemoveDevice(object sender, RoutedEventArgs e)
        {

            RestService restservice = new RestService("https://localhost:7766/", "/Device", token);
            restservice.Delete(devices[listbox.SelectedIndex].Id);
            GetDevices();
        }
        private void Button_Click_CopyDevice(object sender, RoutedEventArgs e)
        {

            RestService restservice = new RestService("https://localhost:7766/", "/Device", token);
            restservice.Put(devices[listbox.SelectedIndex].Id);
            GetDevices();
        }
    }
}
