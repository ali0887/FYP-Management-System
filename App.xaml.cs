﻿using System.Configuration;
using System.Data;
using System.Windows;
using Syncfusion.Licensing;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string _username { get; set; }

        public App()
        {

            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCe0x3RHxbf1x0ZFFMYFtbRHBPIiBoS35RckVnWH5feXFRR2lZVEZ+");

        }

    }

}
