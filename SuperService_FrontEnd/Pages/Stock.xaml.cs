using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
        public Ingredient SelectedIngredient { get; set; }
        public bool IsInStock { get; set; }

        public Stock()
        {
            _iHelper = new IngredientHelper();
            Ingredients = _iHelper.GetAllIngredientsWithDistinctNames().ToList();
            InitializeComponent();
            DataContext = this;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ingredient = (Ingredient)((ListView)sender).SelectedItem;
            _detailsFrame.Navigate(new IngredientDetails(ingredient));
        }
    }
}
