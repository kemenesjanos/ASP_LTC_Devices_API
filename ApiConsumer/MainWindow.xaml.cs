using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ApiConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string token;
        private IEnumerable<Device> devices = new List<Device>();
        private RestService restserviceDev;

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
                restserviceDev = new RestService("https://localhost:7766/", "/Device", token);

                GetDevices();
            }
            else
            {
                this.Close();
            }
        }
        

        public async Task GetDevices()
        {
            devices = await restserviceDev.Get<Device>();
            listbox.ItemsSource = devices;

            listbox.SelectedIndex = -1;
            listbox.Items.Refresh();

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

        private async void Button_Click_AddDevice(object sender, RoutedEventArgs e)
        {
            Device newDevice = new Device()
            {
                DescriptionTabData = new DescriptionTabData() { Name = "New Device" }
            };

            if(token != null)
            {
                await restserviceDev.Post(newDevice);
            }
            
            await GetDevices();
        }

        private async void Button_Click_RemoveDevice(object sender, RoutedEventArgs e)
        {
            
            if (token != null && listbox.SelectedIndex != -1)
            {
                await restserviceDev.Delete((listbox.SelectedItem as Device).Id);
            }

            await GetDevices();
        }
        private async void Button_Click_CopyDevice(object sender, RoutedEventArgs e)
        {
            if (token != null && listbox.SelectedIndex != -1)
            {
                Device copyDev = devices.ToList()[listbox.SelectedIndex];
                copyDev.DescriptionTabData.Name += "_copy";
                await restserviceDev.Post(copyDev);
            }

           await GetDevices();
        }
    }
}
