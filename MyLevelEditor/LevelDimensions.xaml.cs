using System;
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
using System.Windows.Shapes;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for LevelDimensions.xaml
    /// </summary>
    public partial class LevelDimensions : Window
    {
        public int Widhthh { get; set; }

        public int Heightt { get; set; }

        public LevelDimensions()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsDimensionValuesNumeric(txbWidht.Text, txbHeight.Text))
            {
                if (IsDimensionValuesInRange(txbWidht.Text, txbHeight.Text))
                {
                    //set width and height
                    Widhthh = Convert.ToInt32(txbWidht.Text);
                    Heightt = Convert.ToInt32(txbHeight.Text);
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Your widht and height should be between 1 and 100, please enter a valid input", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            else
            {
                MessageBox.Show("Wrong dimension values, please enter a valid input", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
        #region Utility Functions


        private bool IsDimensionValuesNumeric(string _widht, string _height)
        {
            bool widthIsNumeric = int.TryParse(_widht, out _);
            bool heightIsNumeric = int.TryParse(_height, out _);

            if (widthIsNumeric && heightIsNumeric)
                return true;

            return false;

        }

        private bool IsDimensionValuesInRange(string _widht, string _height)
        {
            if ((int.Parse(_widht) > 100 || int.Parse(_widht) < 1) || (int.Parse(_height) > 100 || int.Parse(_height) < 1))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
