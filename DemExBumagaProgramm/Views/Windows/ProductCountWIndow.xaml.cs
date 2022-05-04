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

namespace DemExBumagaProgramm.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductCountWIndow.xaml
    /// </summary>
    public partial class ProductCountWindow : BaseWindow
    {
        private int count;
        public ProductCountWindow()
        {
            InitializeComponent();
            DataContext = this;

        }
        public int Count { get => count; set { count = value; } }


        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (Count <= 0)
            {
                MessageBox.Show("Количество не может быть меньше или равно 0");
            }
            else
            {
                DialogResult = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
