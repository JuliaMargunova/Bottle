using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace Bottle
{
    public partial class Game : PhoneApplicationPage
    {
        private double Degrees { get; set; }
        private Random random { get; set; }
        private int CountGamers { get; set; }
        IsolatedStorageSettings AppSettings;
        public Game()
        {
            InitializeComponent();
            Degrees = 0;
            AppSettings = IsolatedStorageSettings.ApplicationSettings;
            random = new Random();
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    DivideGrid(4);
        //}

        private void rotate(double degrees)
        {
            Storyboard MyStory = new Storyboard();
            MyStory.Duration = new TimeSpan(0, 0, 0, 0, 500);
            DoubleAnimation My_Double = new DoubleAnimation();
            My_Double.To = degrees;
            My_Double.Duration = new TimeSpan(0, 0, 0, 0, 500);
            MyStory.Children.Add(My_Double);
            RotateTransform MyTransform = new RotateTransform();
            Storyboard.SetTarget(My_Double, MyTransform);
            Storyboard.SetTargetProperty(My_Double, new PropertyPath("Angle"));
            bottleImage.RenderTransform = MyTransform;
            bottleImage.RenderTransformOrigin = new Point(0.5, 0.5);
            MyStory.Begin();
        }

        private void bottleImage_Start(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Degrees = random.Next(360, 720);
            rotate(Degrees);
        }

        private void DivideGrid(int countGamers)
        {
            try
            {
                double heightLine = Math.Sqrt(Math.Pow(LayoutRoot.ActualWidth / 2, 2) + Math.Pow(LayoutRoot.RowDefinitions[1].ActualHeight / 2, 2));
                double angle = 360 / countGamers;
                double transformAngle = 90;
                for (int i = 0; i < countGamers; i++)
                {
                    Line myLine = new Line();
                    transformAngle += angle;
                    string aa = "myLine" + i.ToString();
                    myLine.Stroke = new SolidColorBrush(Colors.Red);
                    myLine.X1 = LayoutRoot.ActualWidth / 2;
                    myLine.X2 = 0;
                    myLine.Y1 = LayoutRoot.RowDefinitions[1].ActualHeight / 2;
                    myLine.Y2 = LayoutRoot.RowDefinitions[1].ActualHeight / 2;
                    myLine.StrokeThickness = 2;
                    myLine.SetValue(Grid.RowProperty, 1);
                    myLine.Name = aa;
                    RotateTransform MyTransform = new RotateTransform();
                    myLine.RenderTransform = MyTransform;
                    myLine.RenderTransformOrigin = new Point(0.5, 0.5);
                    MyTransform.Angle = transformAngle;
                    LayoutRoot.Children.Add(myLine);
                    Canvas.SetZIndex(FindName(aa) as Line, 5 + i);
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }

        private void q_Click(object sender, RoutedEventArgs e)
        {
            var zIndex = Canvas.GetZIndex(FindName("myLine2") as UIElement);
            var zIndex2 = Canvas.GetZIndex(FindName("bottleImage") as UIElement);
            MessageBox.Show(zIndex.ToString() + "," + zIndex2.ToString());
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            DivideGrid(Convert.ToInt32(AppSettings["countGamers"]));
        }
    }


}