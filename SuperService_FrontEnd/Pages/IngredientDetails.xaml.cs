using SuperService_BackEnd.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperService_FrontEnd.Pages
{
    /// <summary>
    /// Interaction logic for IngredientDetails.xaml
    /// </summary>
    public partial class IngredientDetails : Page
    {
        public Ingredient SelectedIngredient { get; set; }
        public IngredientDetails()
        {
            InitializeComponent();
        }

        public IngredientDetails(Ingredient ingredient)
        {
            InitializeComponent();
            SelectedIngredient = ingredient;
            DataContext = this;
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
