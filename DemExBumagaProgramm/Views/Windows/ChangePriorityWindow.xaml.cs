using DemExBumagaProgramm.Data;
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
    /// Логика взаимодействия для ChangePriorityWindow.xaml
    /// </summary>
    public partial class ChangePriorityWindow : BaseWindow
    {
        private int priority;
        public ChangePriorityWindow(int priority)
        {
            InitializeComponent();
            Priority = priority;
            DataContext = this;

        }
        public int Priority { get => priority; set { priority = value; } }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
