using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperService_FrontEnd.Pages
{
    /// <summary>
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class Stock : Page
    {
        IngredientHelper _iHelper;
        public ICollection<Ingredient> Ingredients { get; private set; }
        public Ingredient SelectedIngredient { get; private set; }
        public bool IsInStock { get; set; }

        public Stock()
        {
            _iHelper = new IngredientHelper();
            RefreshIngredients();
            InitializeComponent();
            DataContext = this;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedIngredient = (Ingredient)((ListView)sender).SelectedItem;
            _detailsFrame.Navigate(new IngredientDetails(SelectedIngredient));
        }

        public  void RefreshIngredients()
        {
            Ingredients = _iHelper.GetAllIngredientsWithDistinctNames().ToList();
            Ingredient item = new Ingredient();
            if (_stockList != null)
            {
                item = (Ingredient)_stockList.SelectedItem;
                _stockList.ItemsSource = Ingredients;
            }
        }
    }
}
