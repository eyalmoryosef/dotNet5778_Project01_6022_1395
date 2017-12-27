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
using BE;
using BL;
namespace UI
{

  

    public partial class addNannyWindow : Window
    {
        BE.Nanny nanny;
        BL.IBL bl;
        public addNannyWindow()
        {
            InitializeComponent();
            nanny = new BE.Nanny();
          //  bl = new BL.FactoryBL.GetBL();
        }
        
        

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource nannyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("nannyViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // nannyViewSource.Source = [generic data source]
        //}

    }
}
