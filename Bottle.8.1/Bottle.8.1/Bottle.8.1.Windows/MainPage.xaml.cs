using Bottle._8._1.Entities;
using Bottle._8._1.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bottle._8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public int CountGamers { get; set; }
        public int NumberBottle { get; set; }
        public int NumberBackground { get; set; }
        ApplicationDataContainer AppSettings = ApplicationData.Current.RoamingSettings;
        BottleRepository bottleRepository;
        BackgroundRepository backgroundRepository;
        public int CountBottle { get; set; }
        public int CountBackground { get; set; }
        BottleInfo bottle;
        Background background;

        public MainPage()
        {
            InitializeComponent();
            bottleRepository = new BottleRepository();
            backgroundRepository = new BackgroundRepository();
            SetStartValue();
        }

        public void SetStartValue()
        {
            if (AppSettings.Values["countGamers"] != null)
            {
                CountGamers = (int)AppSettings.Values["countGamers"];
                TBCountGamers.Text = CountGamers.ToString();
            }
            else
            {
                CountGamers = Convert.ToInt32(TBCountGamers.Text);
            }

            if (AppSettings.Values["numberBottle"] != null)
            {
                NumberBottle = (int)AppSettings.Values["numberBottle"];
            }
            else
            {
                NumberBottle = 1;
            }
            bottle = bottleRepository.GetBottle(NumberBottle);
            bottleImage.Source = new BitmapImage(new Uri(bottle.Path, UriKind.RelativeOrAbsolute));


            if (AppSettings.Values["numberBackground"] != null)
            {
                NumberBackground = (int)AppSettings.Values["numberBackground"];
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

        private void game_Click(object sender, RoutedEventArgs e)
        {
            if (AppSettings.Values["countGamers"] == null)
            {
                AppSettings.Values["countGamers"] = CountGamers;
            }
            if (AppSettings.Values["countGamers"] != null && (int)AppSettings.Values["countGamers"] != CountGamers)
            {
                AppSettings.Values["countGamers"] = CountGamers;
            }

            if (AppSettings.Values["numberBottle"] == null)
            {
                AppSettings.Values["numberBottle"] = NumberBottle;
            }
            if (AppSettings.Values["numberBottle"] != null && (int)AppSettings.Values["numberBottle"] != NumberBottle)
            {
                AppSettings.Values["numberBottle"] = NumberBottle;
            }

            if (AppSettings.Values["numberBackground"] == null)
            {
                AppSettings.Values["numberBackground"] = NumberBackground;
            }
            if (AppSettings.Values["numberBackground"] != null && (int)AppSettings.Values["numberBackground"] != NumberBackground)
            {
                AppSettings.Values["numberBackground"] = NumberBackground;
            }
            Frame.Navigate(typeof(Game), string.Format("{0},{1},{2}", CountGamers, NumberBottle, NumberBackground));
        }



        private void ReductionGamers_Tap(object sender, TappedRoutedEventArgs e)
        {
            if (CountGamers != 2)
            {
                CountGamers--;
                TBCountGamers.Text = CountGamers.ToString();
            }
        }

        private void IncreaseGamers_Tap(object sender, TappedRoutedEventArgs e)
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

        private void IncreaseBottle_Tap(object sender, TappedRoutedEventArgs e)
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

        private void ReductionBottle_Tap(object sender, TappedRoutedEventArgs e)
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

        private void IncreaseBackground_Tap(object sender, TappedRoutedEventArgs e)
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

        private void ReductionBackgground_Tap(object sender, TappedRoutedEventArgs e)
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

        private void RateReminder_TryReminderCompleted(object sender, AppPromo.RateReminderResult e)
        {
            if (e.Runs == RateReminder.RunsBeforeReminder && !e.RatingShown)
            {
                RateReminder.RunsBeforeReminder *= 2;
                RateReminder.ResetCounters();
            }
        }
    }
}
