﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinComponent.Views;

namespace CustomComponent
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MyPage();
        }
    }
}
