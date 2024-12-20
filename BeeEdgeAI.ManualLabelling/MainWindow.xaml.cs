using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BeeEdgeAI.ManualLabelling.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;



namespace BeeEdgeAI.ManualLabelling;


public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; set; }
   
    public string OpenFileName { get; set; } = string.Empty;
    public MainWindow(MainViewModel mainViewModel)
    {
        this.InitializeComponent();
        this.ViewModel = mainViewModel;        
    }

    

    private async void SaveFileButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
