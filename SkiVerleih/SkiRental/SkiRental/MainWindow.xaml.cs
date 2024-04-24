using Microsoft.EntityFrameworkCore;
using SkiRental.Model;
using System.Collections.ObjectModel;
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

namespace SkiRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly RentalContext rentalContext = new RentalContext();

        public ObservableCollection<Item> Items { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            rentalContext.Items.Include(i => i.Category).Load();
            Items = rentalContext.Items.Local.ToObservableCollection();
            DataContext = this;

        }

        void delete(object sender, RoutedEventArgs e)
        {
            var item = (Item)itemGrid.SelectedItem;
            var result = MessageBox.Show($"Should the item {item.Name} be deleted?", "Confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                rentalContext.Items.Local.Remove(item);
                rentalContext.SaveChanges();
            }
        }
        void add(object sender, RoutedEventArgs e)
        {
            // populate the new item with values
            // so that the form is valid right away
            var item = new Item();
            item.Name = "New item";
            item.Description = "Description";
            item.Price = 1;
            item.Category = rentalContext.Categories.FirstOrDefault();
            openAddEditItem(item);
        }

        void edit(object sender, RoutedEventArgs e)
        {
            openAddEditItem((Item)itemGrid.SelectedItem);
        }

        void openAddEditItem(Item item)
        {
            var addEdit = new AddEditItem(item, rentalContext);
            if (addEdit.ShowDialog() == true)
            {
                itemGrid.Items.Refresh();
            }
        }
    }
}