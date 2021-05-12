﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ApiConsumer
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsRegist { get; set; }

        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserName = tb_username.Text;
            Password = tb_pass.Password;
            IsRegist = false;
            this.DialogResult = true;
        }

        private void Button_Click_Regist(object sender, RoutedEventArgs e)
        {
            UserName = tb_username.Text;
            Password = tb_pass.Password;
            IsRegist = true;
            this.DialogResult = true;
        }
    }
}
