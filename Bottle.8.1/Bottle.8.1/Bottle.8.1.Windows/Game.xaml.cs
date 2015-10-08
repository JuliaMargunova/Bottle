using Bottle._8._1.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bottle._8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        private Random random { get; set; }
        private int CountGamers { get; set; }
        private int CurrentGamer { get; set; }
        private Color colorLine { get; set; }
        private int numberBottle { get; set; }
        public ResourceLoader resourceLoader { get; set; }
        ApplicationDataContainer AppSettings = ApplicationData.Current.RoamingSettings;
        private bool IsRotateStarted { get; set; }
        private List<string> Desires { get; set; }
        public string Desire { get; set; }
        private Image BottleImage { get; set; }

        BackgroundRepository backgroundRepository;
        BottleRepository bottleRepository;

        public Game()
        {
            InitializeComponent();
            CurrentGamer = 1;
            random = new Random();
            resourceLoader = new ResourceLoader();
            backgroundRepository = new BackgroundRepository();
            bottleRepository = new BottleRepository();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            BackgroundRepository backgroundRepository = new BackgroundRepository();
            BottleRepository bottleRepository = new BottleRepository();
            string[] parameters = e.Parameter.ToString().Split(',');
            CountGamers = Convert.ToInt32(parameters[0]);
            numberBottle = Convert.ToInt32(parameters[1]);
            int numberBackground = Convert.ToInt32(parameters[2]);
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri(backgroundRepository.GetBackground(numberBackground).Path, UriKind.RelativeOrAbsolute));
            LayoutRoot.Background = img;
            colorLine = backgroundRepository.GetBackground(numberBackground).ColorLine;
            CurrentGamerLabel.Foreground = new SolidColorBrush(colorLine);
            CurrentGamerLabel.Text = string.Format(resourceLoader.GetString("CurrentGamer/Text"), CurrentGamer);
            Desires = GetListDesires();
        }

        private List<string> GetListDesires()
        {
            var currentCulture = CultureInfo.CurrentCulture.ToString().Substring(0, 2);
            string fileName = currentCulture.ToString()+"Desire.txt";
            var file = GetDictionary(fileName);
            var taskStream = file.OpenStreamForReadAsync();
            taskStream.Wait();
            List<string> listText = new List<string>();
            using (var objReader = new StreamReader(taskStream.Result, Encoding.GetEncoding("Windows-1251")))
            {
                while (!objReader.EndOfStream)
                {
                    listText.Add(objReader.ReadLine());
                }
            }
            return listText;
        }

        private static StorageFile GetDictionary(string fileName)
        {
            Task<StorageFolder> taskFolder = Package.Current.InstalledLocation.GetFolderAsync("Desires").AsTask();
            taskFolder.Wait();
            Task<StorageFile> taskFile = taskFolder.Result.GetFileAsync(fileName).AsTask();
            taskFile.Wait();
            return taskFile.Result;
        }

        private void rotate(double degrees)
        {
            Storyboard MyStory = new Storyboard();
            MyStory.Duration = TimeSpan.FromMilliseconds(degrees);
            DoubleAnimation My_Double = new DoubleAnimation();
            My_Double.To = degrees;
            My_Double.Duration = TimeSpan.FromMilliseconds(degrees);
            MyStory.Children.Add(My_Double);
            RotateTransform MyTransform = new RotateTransform();
            Storyboard.SetTarget(My_Double, MyTransform);
            Storyboard.SetTargetProperty(My_Double, "Angle");
            BottleImage.RenderTransform = MyTransform;
            BottleImage.RenderTransformOrigin = new Point(0.5, 0.5);
            mediaElement.Play();
            MyStory.Begin();
            GridLabelCurrentGamer.Visibility = Visibility.Collapsed;
            MyStory.Completed += animationRotateBottle_Completed;
        }

        private  void animationRotateBottle_Completed(object sender, object e)
        {
            mediaElement.Stop();
            SetDesire();
            title.Text = string.Format(resourceLoader.GetString("GamerDesire/Text"), CurrentGamer);
            body.Text = Desire;
            body.Foreground = new SolidColorBrush(colorLine);
            body.FontSize = GridNumberGamer.ActualHeight * 40 /100;
            gridMessage.Visibility = Visibility.Visible;
        }

    
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            IsRotateStarted = false;
            CurrentGamerLabel.Text = string.Format(resourceLoader.GetString("CurrentGamer/Text"), CurrentGamer);
            GridLabelCurrentGamer.Visibility = Visibility.Visible;
            gridMessage.Visibility = Visibility.Collapsed;
        }

        private void SetDesire()
        {
            string desire;
            do
            {
                desire = Desires[random.Next(0, Desires.Count())];
            } while (Desire == desire);
            Desire = desire;
        }

        private void bottleImage_Start(object sender, TappedRoutedEventArgs e)
        {
            if (!IsRotateStarted)
            {
                IsRotateStarted = true;
                double degrees = 0;
                do
                {
                    degrees = random.Next(360, 1080);
                } while (GetNumberGamer(degrees) == CurrentGamer);
                CurrentGamer = GetNumberGamer(degrees);
                rotate(degrees);
            }
        }

        private int GetNumberGamer(double angle)
        {
            var n = Math.Floor(angle / 360);
            angle = angle - n * 360;
            int numberGamer = Convert.ToInt32(Math.Floor(angle / (360.0 / CountGamers)) + 1);
            return numberGamer;
        }

        private void DivideGrid(int countGamers)
        {
            try
            {
                double angle = 360.0 / countGamers;
                double transformAngleLine = -90;
                double transformAngleNumber = 0;
                for (int i = 0; i < countGamers; i++)
                {
                    Line myLine = new Line();
                    transformAngleLine += angle;
                    transformAngleNumber = transformAngleLine - angle / 2;
                    string nameLine = "myLine" + i.ToString();
                    myLine.Stroke = new SolidColorBrush(colorLine);
                    myLine.X1 = LayoutRoot.ActualWidth / 2;
                    myLine.X2 = LayoutRoot.ActualWidth;
                    myLine.Y1 = LayoutRoot.ActualHeight / 2;
                    myLine.Y2 = LayoutRoot.ActualHeight / 2;
                    myLine.StrokeThickness = 3;
                    myLine.SetValue(Grid.RowProperty, 0);
                    myLine.SetValue(Grid.RowSpanProperty, 2);
                    myLine.Name = nameLine;
                    RotateTransform MyTransform = new RotateTransform();
                    myLine.RenderTransform = MyTransform;
                    myLine.RenderTransformOrigin = new Point(0.5, 0.5);
                    MyTransform.Angle = transformAngleLine;
                    LayoutRoot.Children.Add(myLine);

                    TextBlock numberGamer = new TextBlock();
                    numberGamer.Name = "number" + i.ToString();
                    numberGamer.Text = (i + 1).ToString();
                    numberGamer.HorizontalAlignment = HorizontalAlignment.Center;
                    numberGamer.VerticalAlignment = VerticalAlignment.Center;
                    numberGamer.FontSize = GridNumberGamer.ActualHeight * 80 / 100;
                    numberGamer.TextWrapping = TextWrapping.NoWrap;
                    numberGamer.FontFamily = new FontFamily("Comic Sans MS");


                    Border b = new Border();
                    Border b2 = new Border();
                    b.BorderThickness = b2.BorderThickness = new Thickness(3);
                    b.CornerRadius = b2.CornerRadius = new CornerRadius(20);
                    b.BorderBrush = b2.BorderBrush = new SolidColorBrush(colorLine);
                    b.Child = numberGamer;
                    b.Height = b.Width = b2.Height = b2.Width = GridNumberGamer.ActualHeight;

                    if (GridNumberGamer.ActualHeight > GridNumberGamer.ColumnDefinitions[1].ActualWidth)
                    {
                        b.Margin = b2.Margin = new Thickness(0, 0, -GridNumberGamer.ActualHeight + GridNumberGamer.ColumnDefinitions[1].ActualWidth, 0);
                    }
                    b2.Background = new SolidColorBrush(Colors.Black);
                    b2.Opacity = 0.4;
                    var radius = LayoutRoot.ActualHeight / 2 * 84 / 100;

                    var y0 = LayoutRoot.ActualHeight / 2 * 84 / 100;
                    var х1 = (Math.Cos(Math.PI * transformAngleNumber / 180) * radius);
                    var у1 = y0 + (Math.Sin(Math.PI * transformAngleNumber / 180) * radius);
                    MatrixTransform matrixTransform = new MatrixTransform();
                    b.RenderTransform = b2.RenderTransform = matrixTransform;
                    matrixTransform.Matrix = new Matrix(1, 0, 0, 1, х1, у1);

                    b2.SetValue(Grid.ColumnProperty, 1);
                    b.SetValue(Grid.ColumnProperty, 1);
                    GridNumberGamer.Children.Add(b2);
                    GridNumberGamer.Children.Add(b);
                    backButton.Height = GridNumberGamer.ActualHeight;
                }

                BottleImage = new Image();
                BottleImage.Name = "bottleImage";
                BottleImage.Source = new BitmapImage(new Uri(bottleRepository.GetBottle(numberBottle).Path, UriKind.RelativeOrAbsolute));
                BottleImage.RenderTransformOrigin = new Point(0.5, 0.5);
                RotateTransform rotateBottle = new RotateTransform();
                rotateBottle.Angle = 10;
                BottleImage.RenderTransform = rotateBottle;
                BottleImage.SetValue(Grid.RowProperty, 0);
                BottleImage.SetValue(Grid.RowSpanProperty, 2);
                BottleImage.Tapped += bottleImage_Start;
                BottleImage.PointerPressed += bottleImage_PointerPressed;
                BottleImage.Height = GridBottle.ActualHeight;
                BottleImage.HorizontalAlignment = HorizontalAlignment.Center;
                BottleImage.VerticalAlignment = VerticalAlignment.Center;
                LayoutRoot.Children.Add(BottleImage);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }


        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var item in LayoutRoot.Children)
            {
                if (item.GetType() == typeof(Line) || item.GetType() == typeof(Image))
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
            GridNumberGamer.Children.Clear();
            LayoutRoot.UpdateLayout();
            DivideGrid(Convert.ToInt32(AppSettings.Values["countGamers"]));
        }

        private void bottleImage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (!IsRotateStarted)
            {
                IsRotateStarted = true;
                double degrees = 0;
                do
                {
                    degrees = random.Next(360, 1080);
                } while (GetNumberGamer(degrees) == CurrentGamer);
                CurrentGamer = GetNumberGamer(degrees);
                rotate(degrees);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }        
    }
}
