using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Bottle.Resources;
using System.IO.IsolatedStorage;
using Bottle.Repository;
using Bottle.Entities;
using System.Windows.Media.Imaging;

namespace Bottle
{
    public partial class MainPage : PhoneApplicationPage
    {
        public int CountGamers { get; set; }
        IsolatedStorageSettings AppSettings;
        BottleRepository bottleRepository;
        public int CountBottle { get; set; }
        int numberBottle = 1;
        BottleInfo bottle;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            bottleRepository = new BottleRepository();
            bottle = bottleRepository.GetBottle(numberBottle);
            CountBottle = bottleRepository.GetCountBottle();
            AppSettings = IsolatedStorageSettings.ApplicationSettings;
            if (AppSettings.Contains("countGamers"))
            {
                CountGamers = (int)AppSettings["countGamers"];
                TBCountGamers.Text = CountGamers.ToString();
            }
            else
            {
                CountGamers = Convert.ToInt32(TBCountGamers.Text);
            }
        }


        private void game_Click_1(object sender, RoutedEventArgs e)
        {
            if (!AppSettings.Contains("countGamers"))
            {
                AppSettings.Add("countGamers", CountGamers);
            }
            if (AppSettings.Contains("countGamers") && (int)AppSettings["countGamers"] != CountGamers)
            {
                AppSettings["countGamers"] = CountGamers;
            }
            NavigationService.Navigate(new Uri(String.Format("/Game.xaml?ParameterString={0}", bottle.Path), UriKind.Relative));
        }



        private void ReductionGamers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CountGamers != 1)
            {
                CountGamers--;
                TBCountGamers.Text = CountGamers.ToString();
            }
        }

        private void IncreaseGamers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CountGamers != 11)
            {
                CountGamers++;
                TBCountGamers.Text = CountGamers.ToString();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            bottleImage.Source = new BitmapImage(new Uri(bottle.Path, UriKind.RelativeOrAbsolute));
        }

        private void IncreaseBottle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CountBottle != numberBottle)
            {
                numberBottle++;
                bottle = bottleRepository.GetBottle(numberBottle);
                bottleImage.Source = new BitmapImage(new Uri(bottle.Path, UriKind.RelativeOrAbsolute));
            }
        }

        private void ReductionBottle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (1 != numberBottle)
            {
                numberBottle--;
                bottle = bottleRepository.GetBottle(numberBottle);
                bottleImage.Source = new BitmapImage(new Uri(bottle.Path, UriKind.RelativeOrAbsolute));
            }
        }



        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}