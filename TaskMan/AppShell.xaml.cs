﻿using TaskMan.Views;

namespace TaskMan
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
        }
    }
}
