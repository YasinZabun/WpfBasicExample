using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation.Views
{
    /// <summary>
    /// Window1.xaml etkileşim mantığı
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            this.DataContext = new ViewModelClass();
            InitializeComponent();
            /*
             xmlns:modelview="clr-namespace:wpfornekdetay.viewmodelfolder" xmlns:modelfolder="clr-namespace:wpfornekdetay.modelfolder" 
            xmlns:base="clr-namespace:Utils.Base" d:DataContext="{d:DesignInstance Type=base:ObservableBase}"

             */
        }
    }
}
