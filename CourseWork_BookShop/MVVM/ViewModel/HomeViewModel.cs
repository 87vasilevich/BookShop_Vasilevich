using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject, IDataErrorInfo
    {
        #region Для поиска
        //Для выбора жанра
        private string choice_genre;
        public string ChoiceGenre
        {
            get { return choice_genre; }
            set
            {
                choice_genre = value;
                OnPropertyChanged();
            }
        }

        //Для названия книги
        public string _bookn="";
        public string BookName
        {
            get { return _bookn; }
            set
            {
                _bookn = value;
                OnPropertyChanged();
            }
        }

        //Для ФИО автора
        public string _afio="";
        public string AuthorFIO
        {
            get { return _afio; }
            set
            {
                _afio = value;
                OnPropertyChanged();
            }
        }

        //Команда
        public RelayCommand Search_book { get; set; }

        public RelayCommand ResetSearch { get; set; }
        #endregion

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

        #region Валидация
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    //---------------------------------------------------------
                    case "BookName":
                        if (BookName.Length == 0)
                        {
                            error = String.Empty;
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidBookName(BookName) )
                            {
                                error = "Введите корректное название!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (BookName.Length < 2 || BookName.Length > 50)
                                {
                                    error = "Введите название от 2 до 50 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "AuthorFIO":
                        if (AuthorFIO.Length == 0)
                        {
                            error = String.Empty;
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidFioName(AuthorFIO) )
                            {
                                error = "Введите корректное ФИО!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (AuthorFIO.Length < 2 || AuthorFIO.Length > 50)
                                {
                                    error = "Введите ФИО от 2 до 50 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                    //---------------------------------------------------------
                }

                return error;
            }
        }

        static readonly string[] ValidatedProperties = { "BookName", "AuthorFIO" };
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (this[property] != String.Empty)
                        return false;
                }

                return true;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
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
        //Для сортировки
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
            ChoiceGenre = "Жанр";
            Choice = "умолчанию";

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

            Search_book = new RelayCommand(o =>
            {
                if (IsValid)
                {
                    //=====================Все поля заполнены
                    if((BookName != "") && (AuthorFIO != "") && (ChoiceGenre != "Жанр"))
                    {
                        BookName = BookName.Trim();
                        string[] tempBName = BookName.Split(' ');

                        AuthorFIO = AuthorFIO.Trim();
                        string[] tempAFIO = AuthorFIO.Split(' ');
                        AllBooks = new ObservableCollection<Books>(db.GetDataList().Where(b => b.BookGenre.ToLower() == ChoiceGenre.ToLower() && SearchName(tempBName, b) && SearchFIO(tempAFIO, b)).ToList());
                        if (AllBooks.Count == 0)
                        {
                            MessageBox.Show("Нет ни одного результата по вашему запросу!", "Поиск");
                            AllBooks = Get_allBooks();
                        }
                    }
                    else
                    {
                        //=====================Только название и ФИО
                        if ((BookName != "") && (AuthorFIO != ""))
                        {
                            BookName = BookName.Trim();
                            string[] tempBName = BookName.Split(' ');

                            AuthorFIO = AuthorFIO.Trim();
                            string[] tempAFIO = AuthorFIO.Split(' ');
                            AllBooks = new ObservableCollection<Books>(db.GetDataList().Where(b => SearchName(tempBName, b) && SearchFIO(tempAFIO, b)).ToList());
                            if (AllBooks.Count == 0)
                            {
                                MessageBox.Show("Нет ни одного результата по вашему запросу!", "Поиск");
                                AllBooks = Get_allBooks();
                            }
                        }
                        else
                        {
                            //=====================Только название и жанр
                            if ((BookName != "") && (ChoiceGenre != "Жанр"))
                            {
                                BookName = BookName.Trim();
                                string[] tempBName = BookName.Split(' ');


                                AllBooks = new ObservableCollection<Books>(db.GetDataList().Where(b => b.BookGenre.ToLower() == ChoiceGenre.ToLower() && SearchName(tempBName, b)).ToList());
                                if (AllBooks.Count == 0)
                                {
                                    MessageBox.Show("Нет ни одного результата по вашему запросу!", "Поиск");
                                    AllBooks = Get_allBooks();
                                }
                            }
                            else
                            {
                                //=====================Только название
                                if (BookName != "")
                                {
                                    BookName = BookName.Trim();
                                    string[] tempBName = BookName.Split(' ');

                                    AllBooks = new ObservableCollection<Books>(db.GetDataList().Where(b => SearchName(tempBName, b)).ToList());
                                    if (AllBooks.Count == 0)
                                    {
                                        MessageBox.Show("Нет ни одного результата по вашему запросу!", "Поиск");
                                        AllBooks = Get_allBooks();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Строка поиска по названию книги пуста!", "Поиск");
                                    AllBooks = Get_allBooks();
                                }
                            }
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Неккоректные данные!", "Поиск");
                }
            });

            ResetSearch = new RelayCommand(o => {

                AllBooks = Get_allBooks();
                ChoiceGenre = "Жанр";
                Choice = "умолчанию";
                BookName = "";
                AuthorFIO = "";
            });
        }

        #region Методы
        public static bool IsValidBookName(string name) //Для названия книги
        {
            string pattern = @"^[A-z | А-я]+[\s|-]*[A-z | А-я]*[\s|-]*[A-z | А-я]*$";
            Match isMatch = Regex.Match(name, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidFioName(string name) //Для фио
        {
            string pattern = @"^[A-z | А-я]+[\s|-]*[A-z | А-я]*[\s|-]*[A-z | А-я]*$";
            Match isMatch = Regex.Match(name, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        private ObservableCollection<Books> Get_allBooks() //Вывод всех книг ВНАЧАЛЕ
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

        private bool SearchName(string[] _tempBName, Books _b)
        {
            string temp_SearchName;
            foreach(string s in _tempBName)
            {
                temp_SearchName = s.ToLower();
                if(!_b.BookName.ToLower().Contains(temp_SearchName))
                {
                    return false;
                } 
            }
            return true;
        }

        private bool SearchFIO(string[] _tempAFIO, Books _b)
        {
            string temp_SearchFIO;
            if (_b.AuthorOtchestevo == null)
            {
                _b.AuthorOtchestevo = "";
            }
            foreach (string s in _tempAFIO)
            {
                temp_SearchFIO = s.ToLower();
                if (!_b.AuthorName.ToLower().Contains(temp_SearchFIO))
                {
                    if (!_b.AuthorSurame.ToLower().Contains(temp_SearchFIO))
                    {
                        if(!_b.AuthorOtchestevo.ToLower().Contains(temp_SearchFIO))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion
        //---------------------------------------------------------
    }
}
