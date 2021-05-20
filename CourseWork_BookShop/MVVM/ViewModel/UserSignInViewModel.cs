using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_BookShop.Core;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class UserSignInViewModel : ObservableObject
    {
        public RelayCommand Go_to_sign_up { get; set; }

        public RelayCommand Go_sign_in_again { get; set; }


        public UserSignInViewModel(U_Sign_MainViewModel window_main_sign)
        {
            Go_to_sign_up = new RelayCommand(o =>
                {
                    window_main_sign.CurrentView = new UserSignUpViewModel(window_main_sign);
                }
            );

            Go_sign_in_again = new RelayCommand(o =>
            {
                window_main_sign.CurrentView = new UserSignInViewModel(window_main_sign);
            }
            );
        }
    }
}
