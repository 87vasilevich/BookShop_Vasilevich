using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        #region Для BookInformation
        //Переменные
        public MainViewModel mainVM;
        private Books _selectedBook;
        public int t_userID;

        //Свойство
        public BookInformationViewModel BookInfoVM { get; set; }

        //Метод
        private void OnBookSelected(Books selectedBook)
        {
            BookInfoVM = new BookInformationViewModel(this, t_userID);

            BookInfoVM.SetBook(SelectedBook);
            mainVM.CurrentView = BookInfoVM;
        }

        //Свойство
        public Books SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnBookSelected(value);
            }
        }
        #endregion

        //---------------------------------------------------------
        private IRepository<Books> db = new SQLBookRepository();
        public ObservableCollection<Books> allBooks { get; set; }
        public ObservableCollection<Books> AllBooks
        {
            get { return allBooks; }
            set { allBooks = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------


        //---------------------------------------------------------
        private string choice;
        public string Choice
        {
            get { return choice; }
            set
            {
                choice = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand Sort_books { get; set; } //Для сортировки

        public HomeViewModel(MainViewModel mainVM, int _userID) //Конструктор
        {
            this.mainVM = mainVM;
            AllBooks = Get_allBooks();
            t_userID = _userID;

            Sort_books = new RelayCommand(o =>
            {
                switch(Choice)
                {
                    case "умолчанию":
                        AllBooks = Get_allBooks();
                        break;

                    case "названию (A->Я)":
                        AllBooks = Sort_Books_ByName_asc();
                        break;

                    case "названию (Я->А)":
                        AllBooks = Sort_Books_ByName_desc();
                        break;

                    case "жанру (A->Я)":
                        AllBooks = Sort_Books_ByGenre_asc();
                        break;

                    case "жанру (Я->А)":
                        AllBooks = Sort_Books_ByGenre_desc();
                        break;

                    case "автору (A->Я)":
                        AllBooks = Sort_Books_ByAuthor_asc();
                        break;

                    case "автору (Я->А)":
                        AllBooks = Sort_Books_ByAuthor_desc();
                        break;
                }
            });
        }

        #region Методы
        private ObservableCollection<Books> Get_allBooks() //Вывод всех книг
        {
            return new ObservableCollection<Books>(db.GetDataList().ToList());
        }

        private ObservableCollection<Books> Sort_Books_ByName_asc() //Сортировка по названию (asc)
        {
            return new ObservableCollection<Books>(db.GetDataList().OrderBy(Book => Book.BookName).ToList());
        }

        private ObservableCollection<Books> Sort_Books_ByName_desc() //Сортировка по названию (desc)
        {
            return new ObservableCollection<Books>(db.GetDataList().OrderByDescending(Book => Book.BookName).ToList());
        }

        private ObservableCollection<Books> Sort_Books_ByGenre_asc() //Сортировка по жанру (asc)
        {
            return new ObservableCollection<Books>(db.GetDataList().OrderBy(Book => Book.BookGenre).ToList());
        }

        private ObservableCollection<Books> Sort_Books_ByGenre_desc() //Сортировка по жанру (desc)
        {
            return new ObservableCollection<Books>(db.GetDataList().OrderByDescending(Book => Book.BookGenre).ToList());
        }

        private ObservableCollection<Books> Sort_Books_ByAuthor_asc() //Сортировка по жанру (asc)
        {
            return new ObservableCollection<Books>(db.GetDataList().OrderBy(Book => Book.AuthorSurame).ThenBy(Book => Book.AuthorName).ToList());
        }

        private ObservableCollection<Books> Sort_Books_ByAuthor_desc() //Сортировка по жанру (desc)
        {
            return new ObservableCollection<Books>(db.GetDataList().OrderByDescending(Book => Book.AuthorSurame).ThenBy(Book => Book.AuthorName).ToList());
        }
        #endregion
        //---------------------------------------------------------
    }
}
