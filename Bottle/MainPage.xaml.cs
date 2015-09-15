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
using System.Windows.Media;

namespace Bottle
{
    public partial class MainPage : PhoneApplicationPage
    {
        public int CountGamers { get; set; }
        public int NumberBottle { get; set; }
        public int NumberBackground { get; set; }
        IsolatedStorageSettings AppSettings;
        BottleRepository bottleRepository;
        BackgroundRepository backgroundRepository;
        public int CountBottle { get; set; }
        public int CountBackground { get; set; }
        BottleInfo bottle;
        Background background;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            bottleRepository = new BottleRepository();
            backgroundRepository = new BackgroundRepository();
            SetStartValue();                     
        }

        public void SetStartValue()
        {
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

            if (AppSettings.Contains("numberBottle"))
            {
                NumberBottle = (int)AppSettings["numberBottle"];                  
            }
            else
            {
                NumberBottle = 1;                
            }
            bottle = bottleRepository.GetBottle(NumberBottle);
            bottleImage.Source = new BitmapImage(new Uri(bottle.Path, UriKind.RelativeOrAbsolute)); 


            if (AppSettings.Contains("numberBackground"))
            {
                NumberBackground = (int)AppSettings["numberBackground"];                
            }
            else
            {
                NumberBackground = 1;
            }
            background = backgroundRepository.GetBackground(NumberBackground);
            BackgroundImage.Source = new BitmapImage(new Uri(background.Path, UriKind.RelativeOrAbsolute));
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri(background.Path, UriKind.RelativeOrAbsolute));
            SettingsGrid.Background = img;
            CountBottle = bottleRepository.GetCountBottle();
            CountBackground = backgroundRepository.GetCountBackground();
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

            if (!AppSettings.Contains("numberBottle"))
            {
                AppSettings.Add("numberBottle", NumberBottle);
            }
            if (AppSettings.Contains("numberBottle") && (int)AppSettings["numberBottle"] != NumberBottle)
            {
                AppSettings["numberBottle"] = NumberBottle;
            }

            if (!AppSettings.Contains("numberBackground"))
            {
                AppSettings.Add("numberBackground", NumberBackground);
            }
            if (AppSettings.Contains("numberBackground") && (int)AppSettings["numberBackground"] != NumberBackground)
            {
                AppSettings["numberBackground"] = NumberBackground;
            }
            NavigationService.Navigate(new Uri(String.Format("/Game.xaml?ParameterString={0},{1},{2}", CountGamers, NumberBottle, NumberBackground), UriKind.Relative));
        }



        private void ReductionGamers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CountGamers != 2)
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
            if (CountBottle != NumberBottle)
            {
                NumberBottle++;
            }
            else
            {
                NumberBottle = 1;
            }
            bottle = bottleRepository.GetBottle(NumberBottle);
            bottleImage.Source = new BitmapImage(new Uri(bottle.Path, UriKind.RelativeOrAbsolute));
        }

        private void ReductionBottle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (1 != NumberBottle)
            {
                NumberBottle--;
            }
            else
            {
                NumberBottle = CountBottle;
            }
            bottle = bottleRepository.GetBottle(NumberBottle);
            bottleImage.Source = new BitmapImage(new Uri(bottle.Path, UriKind.RelativeOrAbsolute));
        }

        private void IncreaseBackground_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CountBackground != NumberBackground)
            {
                NumberBackground++;
            }
            else
            {
                NumberBackground = 1;
            }
            background = backgroundRepository.GetBackground(NumberBackground);
            BackgroundImage.Source = new BitmapImage(new Uri(background.Path, UriKind.RelativeOrAbsolute));
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri(background.Path, UriKind.RelativeOrAbsolute));
            SettingsGrid.Background = img;

        }

        private void ReductionBackgground_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (1 != NumberBackground)
            {
                NumberBackground--;
            }
            else
            {
                NumberBackground = CountBackground;
            }
            background = backgroundRepository.GetBackground(NumberBackground);
            BackgroundImage.Source = new BitmapImage(new Uri(background.Path, UriKind.RelativeOrAbsolute));
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri(background.Path, UriKind.RelativeOrAbsolute));
            SettingsGrid.Background = img;
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