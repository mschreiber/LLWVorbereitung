using SkiRental.Model;
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

namespace SkiRental
{
    /// <summary>
    /// Interaktionslogik für AddEditItem.xaml
    /// </summary>
    public partial class AddEditItem : Window
    {

        public Item Item { get; set; }
        public List<Category> Categories { get; set; }

        private readonly RentalContext rentalContext;

        public AddEditItem(Item item, RentalContext context)
        {
            InitializeComponent();
            Item = item;
            rentalContext = context;
            Categories = rentalContext.Categories.ToList();
            DataContext = this;
        }

        void save(object sender, RoutedEventArgs e)
        {
            if (!isValid())
            {
                return;
            }
            if (Item.Id == 0)
            {
                rentalContext.Items.Local.Add(Item);
            }
            rentalContext.SaveChanges();
            DialogResult = true;
        }

        void cancel(object sender, RoutedEventArgs e)
        {
            rentalContext.Entry<Item>(Item).Reload();
            DialogResult = false; // this closes the dialog
        }

        // this is called by every text change
        void validate(object sender, RoutedEventArgs e)
        {
            isValid();
        }

        // this validates the entered stuff
        // and sets the bg color to red and a tooltip text
        // other option might be to set the error into a textbox in the UI 
        bool isValid()
        {
            bool valid = true;
            nameTxt.Background = Brushes.White;
            nameTxt.ToolTip = "";
            descTxt.Background = Brushes.White;
            descTxt.ToolTip = "";
            priceTxt.Background = Brushes.White;
            priceTxt.ToolTip = "";
            if (string.IsNullOrWhiteSpace(nameTxt.Text))
            {
                nameTxt.Background = Brushes.Red;
                nameTxt.ToolTip = "Name must not be empty";
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(descTxt.Text))
            {
                descTxt.Background = Brushes.Red;
                descTxt.ToolTip = "Description must not be empty";
                valid = false;
            }
            if (!float.TryParse(priceTxt.Text, out float price) || price <= 0)
            {
                priceTxt.Background = Brushes.Red;
                priceTxt.ToolTip = "Price must be greater than 0";
                valid = false;
            }
            return valid;
        }
    }
}
