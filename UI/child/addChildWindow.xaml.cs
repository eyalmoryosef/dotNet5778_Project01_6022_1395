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

namespace UI
{
    /// <summary>
    /// Interaction logic for addChildWindow.xaml
    /// </summary>
    public partial class addChildWindow : Window
    {
        BE.Child child;
        BL.IBL bl;
        public addChildWindow()
        {
            InitializeComponent();
         
            child = new BE.Child();
            this.DataContext = child;
            bl = new BL.FactoryBL.GetBL();
        }

    

     

        private void childDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded()
        {

        }
    }
}
