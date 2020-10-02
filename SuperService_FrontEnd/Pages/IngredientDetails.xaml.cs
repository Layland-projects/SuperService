using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using SuperService_FrontEnd.GUIHelpers;
using SuperService_FrontEnd.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    public partial class IngredientDetails : Page, INotifyPropertyChanged
    {
        IngredientHelper _iHelper;
        Ingredient _selectedIngredient;
        public Ingredient SelectedIngredient 
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                Common.OnPropertyChanged(PropertyChanged, this);
            }
        }
        public IngredientDetails()
        {
            InitializeComponent();
        }

        public IngredientDetails(Ingredient ingredient)
        {
            _iHelper = new IngredientHelper();
            SelectedIngredient = ingredient;
            InitializeComponent();
            DataContext = this;
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnEditSave.Content == "Save")
            {
                if (int.TryParse(NumberInStock.Text, out _))
                {
                    _iHelper.UpdateIngredient(SelectedIngredient);
                }
                else
                {
                    MessageBox.Show("Stock value must be a numeric value only containing numbers 0-9", "Invalid stock value", MessageBoxButton.OK);
                }
            }
            ToggleEditMode();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditMode();
            SelectedIngredient = _iHelper.UndoIngredientChanges(SelectedIngredient);
        }

        private void ToggleEditMode()
        {
            if ((string)btnEditSave.Content == "Save")
            {
                ((Stock)((Frame)((MainWindow)App.Current.MainWindow)._frame).Content).RefreshIngredients();
            }
            ((Stock)((Frame)((MainWindow)App.Current.MainWindow)._frame).Content)._stockList.IsEnabled = !((Stock)((Frame)((MainWindow)App.Current.MainWindow)._frame).Content)._stockList.IsEnabled;
            btnUndo.IsEnabled = !btnUndo.IsEnabled;
            NumberInStock.IsEnabled = !NumberInStock.IsEnabled;
            btnEditSave.Content = (string)btnEditSave.Content == "Edit" ? "Save" : "Edit";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
