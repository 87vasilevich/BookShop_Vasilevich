﻿using CourseWork_BookShop.MVVM.Model;
using CourseWork_BookShop.Core;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class BookInformationViewModel : ObservableObject
    {
        private IRepository<Basket> basket_db = new SQLBasketRepository();
        private IRepository<Books> book_db = new SQLBookRepository();

        #region Для добавления в корзину
        //Переменные
        public string b_amount;
        public int _bookid;
        public string totalamount; //узнать, сколько на складе
        public double baskettotalsum;
        public string aname;
        public string asurname;
        public string bname;
        public Books cur_book;

        //Свойства
        public int BookID
        {
            get { return _bookid; }
            set
            {
                _bookid = value;
                OnPropertyChanged();
            }
        }

        public string BName
        {
            get { return bname; }
            set
            {
                bname = value;
                OnPropertyChanged();
            }
        }

        public string AName
        {
            get { return aname; }
            set
            {
                aname = value;
                OnPropertyChanged();
            }
        }

        public string ASurname
        {
            get { return asurname; }
            set
            {
                asurname = value;
                OnPropertyChanged();
            }
        }

        public string TotalAmount //узнать, сколько на складе
        {
            get { return totalamount; }
            set
            {
                totalamount = value;
                OnPropertyChanged();
            }
        }

        public string BAmount
        {
            get { return b_amount; }
            set
            {
                b_amount = value;
                OnPropertyChanged();
            }
        }

        public double BasketTotalSum
        {
            get { return baskettotalsum; }
            set
            {
                baskettotalsum = value;
                OnPropertyChanged();
            }
        }

        public Books CurrentBook
        {
            get { return cur_book; }
            set
            {
                cur_book = value;
                OnPropertyChanged();
            }
        }

        //Команда
        public RelayCommand AddToBasket { get; set; }

        #endregion

        public Books SelectedBook { get; set; }
        public void SetBook(Books selectedBook)
        {
            this.SelectedBook = selectedBook;
            CurrentBook = this.SelectedBook; //находим в бд текущую книгу
            //Для корзины
            BookID = this.SelectedBook.BookID;
            TotalAmount = this.SelectedBook.BookAmount.ToString();

            AName = this.SelectedBook.AuthorName;
            ASurname = this.SelectedBook.AuthorSurame;
            BName = this.SelectedBook.BookName;
        }

        public RelayCommand GoBack_button { get; set; }

        public BookInformationViewModel(HomeViewModel homeVM, int _userID)
        {
            GoBack_button = new RelayCommand(o =>
            {
                homeVM.mainVM.CurrentView = new HomeViewModel(homeVM.mainVM, _userID);
            });

            AddToBasket = new RelayCommand(o =>
            {
                if((Convert.ToInt32(BAmount) <= Convert.ToInt32(TotalAmount)) && BAmount!="" && BAmount!=null && (Convert.ToInt32(BAmount)>0))
                { 
                    CurrentBook.BookAmount = (Convert.ToInt32(TotalAmount) - Convert.ToInt32(BAmount)); //изменяем кол-во на складе
                    book_db.Update(CurrentBook); //обновляем бд
                    book_db.Save();

                    BasketTotalSum = Convert.ToDouble(Convert.ToDouble(BAmount) * CurrentBook.BookPrice_Single);
                    basket_db.Create(new Basket(_userID, BookID, Convert.ToInt32(BAmount), BasketTotalSum, AName, ASurname, BName)); //добавляем в корзину
                    basket_db.Save();

                    MessageBox.Show("Добавлено в корзину!");

                    BAmount = "";
                    CurrentBook = book_db.GetElement(BookID); //для обновления
                }
                else
                {
                    MessageBox.Show("Введите корректные данные!");
                }
            });
        }
    }
}
