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
using System.Windows.Media.Imaging;

namespace Bottle
{
    public partial class Game : PhoneApplicationPage
    {
        //private double Degrees { get; set; }
        private Random random { get; set; }
        private int CountGamers { get; set; }
        private int CurrentGamer { get; set; }
        IsolatedStorageSettings AppSettings;
        public Game()
        {
            InitializeComponent();
           // Degrees = 0;
            CurrentGamer = 1;
            AppSettings = IsolatedStorageSettings.ApplicationSettings;
            random = new Random();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CountGamers = Convert.ToInt32(AppSettings["countGamers"]);
            bottleImage.Source = new BitmapImage(new Uri (NavigationContext.QueryString["ParameterString"].ToString(),UriKind.RelativeOrAbsolute));
        }

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
            MyStory.Completed += animationRotateBottle_Completed;
        }

        private void animationRotateBottle_Completed(object sender, EventArgs e)
        {
            text.Text = String.Format("{0} ИГРОК КРУТИТ БУТЫЛОЧКУ", CurrentGamer);
        }

        private void bottleImage_Start(object sender, System.Windows.Input.GestureEventArgs e)
        {
            double degrees = 0;
            do
            {
                degrees = random.Next(360, 720);
            } while (GetNumberGamer(degrees) == CurrentGamer);
            CurrentGamer = GetNumberGamer(degrees);
            rotate(degrees);

            //MessageBox.Show((Degrees - n * 360).ToString() + "," + Degrees.ToString() + " ," + GetNumberGamer(Degrees - n * 360).ToString());
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
                double heightLine = Math.Sqrt(Math.Pow(LayoutRoot.ActualWidth / 2, 2) + Math.Pow(LayoutRoot.RowDefinitions[1].ActualHeight / 2, 2));
                double angle = 360.0 / countGamers;
                double transformAngleLine = -90;
                double transformAngleNumber = 0;
                for (int i = 0; i < countGamers; i++)
                {
                    Line myLine = new Line();
                    transformAngleLine += angle;
                    transformAngleNumber = transformAngleLine - angle / 2;
                    string nameLine = "myLine" + i.ToString();
                    myLine.Stroke = new SolidColorBrush(Colors.Cyan);
                    myLine.X1 = LayoutRoot.ActualWidth / 2;
                    myLine.X2 = LayoutRoot.ActualWidth;
                    myLine.Y1 = LayoutRoot.RowDefinitions[1].ActualHeight / 2;
                    myLine.Y2 = LayoutRoot.RowDefinitions[1].ActualHeight / 2;
                    myLine.StrokeThickness = 2;
                    myLine.SetValue(Grid.RowProperty, 1);
                    myLine.Name = nameLine;
                    RotateTransform MyTransform = new RotateTransform();
                    myLine.RenderTransform = MyTransform;
                    myLine.RenderTransformOrigin = new Point(0.5, 0.5);
                    MyTransform.Angle = transformAngleLine;
                    LayoutRoot.Children.Add(myLine);
                    Canvas.SetZIndex(FindName(nameLine) as Line, 1);

                    TextBlock numberGamer = new TextBlock();
                    numberGamer.Name = "number" + i.ToString();
                    numberGamer.Text = (i + 1).ToString();
                    numberGamer.HorizontalAlignment = HorizontalAlignment.Center;
                    numberGamer.VerticalAlignment = VerticalAlignment.Center;
                    numberGamer.FontSize = GridNumberGamer.ActualHeight * 80 / 100;

                    var radius = LayoutRoot.RowDefinitions[1].ActualHeight / 2;
                    var y0 = LayoutRoot.ActualHeight / 2;
                    var х1 = (Math.Cos(Math.PI * transformAngleNumber / 180) * radius * 85 / 100);
                    var у1 = y0 + (Math.Sin(Math.PI * transformAngleNumber / 180) * radius * 85 / 100);
                    MatrixTransform matrixTransform = new MatrixTransform();
                    numberGamer.RenderTransform = matrixTransform;
                    matrixTransform.Matrix = new Matrix(1, 0, 0, 1, х1, у1);
                    LayoutRoot.Children.Add(numberGamer);
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