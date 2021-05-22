using CourseWork_BookShop.MVVM.ViewModel;
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

namespace CourseWork_BookShop
{
    /// <summary>
    /// Логика взаимодействия для User_Sign_MainWindow.xaml
    /// </summary>
    public partial class User_Sign_MainWindow : Window
    {
        public User_Sign_MainWindow()
        {
            InitializeComponent();

            U_Sign_MainViewModel sign_MainViewModel = new U_Sign_MainViewModel();
            this.DataContext = sign_MainViewModel;

            if(sign_MainViewModel.CloseAction==null)
            {
                sign_MainViewModel.CloseAction = new Action(this.Close);
            }
        }
    }
}
