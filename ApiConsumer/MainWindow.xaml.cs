using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
                TokenViewModel tvm = new TokenViewModel();
                if (pw.IsRegist)
                {
                    await restservice.Post<TokenViewModel, RegisterViewModel>(new RegisterViewModel()
                    {
                        Email = pw.UserName,
                        Password = pw.Password
                    });

                }

                try
                {
                    tvm = await restservice.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                    {
                        Username = pw.UserName,
                        Password = pw.Password
                    });
                }
                catch (Exception)
                {
                    MessageBox.Show("Login failed!");
                    Login();
                }
                

                token = tvm.Token;
                restserviceDev = new RestService("https://localhost:7766/", "/Device", token);

                await GetDevices();

            }
            else
            {
                this.Close();
            }
        }
        

        public async Task GetDevices()
        {
            if(token != null)
            {
                devices = await restserviceDev.Get<Device>();
                listbox.ItemsSource = devices;

                listbox.SelectedIndex = -1;
                listbox.Items.Refresh();
            }
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

        private async void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (token != null && listbox.SelectedIndex != -1)
            {
                Device newDev = devices.ToList()[listbox.SelectedIndex];

                await restserviceDev.Post(newDev);
            }
        }


        private void listbox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }
    }
}
