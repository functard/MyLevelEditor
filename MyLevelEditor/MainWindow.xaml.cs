using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Map currentMap;

        private string currentMapPath = "";

        private int blockScale = 15;

        public MainWindow()
        {
            InitializeComponent();
            currentMap = new Map(0, 0);
            cmbBrush.SelectedIndex = 0;

        }
        #region On Menu Buttons Clicked
        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            LevelDimensions dimensions = new LevelDimensions();

            //show dialog window
            dimensions.ShowDialog();

            //set new map
            currentMap = new Map(dimensions.Heightt, dimensions.Widhthh);

            //load map
            LoadMapOnView();

        }

        private void menuLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                //choose path
                currentMapPath = dialog.FileName;

                //set map to load
                currentMap = new Map(currentMapPath);

                //load map
                LoadMapOnView();
            }

        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            //if there is no path
            if (currentMapPath == "")
            {

                SaveFileDialog dialog = new SaveFileDialog();

                if (dialog.ShowDialog() == true)
                {
                    //choose path
                    currentMapPath = dialog.FileName;

                }
            }
            //if path exist already
            if (currentMapPath != "")
                currentMap.SaveMap(currentMapPath);
        }

        private void menuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                //choose path
                currentMapPath = dialog.FileName;

                //save map
                currentMap.SaveMap(currentMapPath);

            }

        }

        private void menuClear_Click(object sender, RoutedEventArgs e)
        {
            //if accepts warning
            if (MessageBox.Show("This will reset your current map", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //clear map
                currentMap.ClearMap();
                LoadMapOnView();
            }
        }
        #endregion

        private void LoadMapOnView()
        {
            //if there is an existing map
            if (currentMap != null)
            {
                mapCanvas.Children.Clear();
                mapCanvas.Width = (currentMap.Widthh * blockScale) + 10;
                mapCanvas.Height = (currentMap.Heightt * blockScale) + 10;

                for (int i = 0; i < currentMap.Heightt; i++)
                {
                    for (int j = 0; j < currentMap.Widthh; j++)
                    {
                        Rectangle block = new Rectangle();
                        block.Stroke = new SolidColorBrush(Colors.Black);
                        block.StrokeThickness = 0.3;
                        block.Width = blockScale;
                        block.Height = blockScale;
                        switch (currentMap.GetElement(j, i))
                        {
                            case 0:
                                block.Fill = new SolidColorBrush(Colors.LightGray);
                                break;
                            case 1:
                                block.Fill = new SolidColorBrush(Colors.Red);
                                break;
                            case 2:
                                block.Fill = new SolidColorBrush(Colors.Green);
                                break;
                            case 3:
                                block.Fill = new SolidColorBrush(Colors.Orange);
                                break;
                            case 4:
                                block.Fill = new SolidColorBrush(Colors.Yellow);
                                break;
                            default:
                                block.Fill = new SolidColorBrush(Colors.Black);
                                break;

                        }
                        block.SetValue(Canvas.LeftProperty, (double)(blockScale * (j + 1)));
                        block.SetValue(Canvas.TopProperty, (double)(blockScale * (i + 1)));

                        mapCanvas.Children.Add(block);
                    }
                }
            }
        }
        private void mapCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Calculate block coordinate
            if (cmbBrush.SelectedIndex > -1)
            {
                Point click = e.MouseDevice.GetPosition(mapCanvas);
                int x = (int)((click.X / blockScale)) - 1;
                int y = (int)((click.Y / blockScale)) - 1;
                var t = (cmbBrush.SelectedItem as ComboBoxItem).Content.ToString();
                currentMap.SetElement(x, y, Convert.ToInt32(t));
                LoadMapOnView();
            }
        }

        private void slidesScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            blockScale = (int)e.NewValue;
            LoadMapOnView();
        }
    }
}
