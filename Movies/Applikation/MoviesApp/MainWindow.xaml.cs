using MoviesApp.controller;
using MoviesApp.model;
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

namespace MoviesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Movie> Movies { get; set; }

        MoviesController controller = MoviesController.getInstance();

        public MainWindow()
        {
            InitializeComponent();
            Movies = controller.Movies;
            this.DataContext = this;
        }

        public void DeleteMovie(object sender, EventArgs e)
        {
            Movie movie = (Movie)dataGrid.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Wollen sie den Film '{movie.Title}' wirklich löschen?", "Löschen", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                controller.Delete((Movie)dataGrid.SelectedItem);
            }
            controller.ReloadMovies();
        }

        public void EditMovie(object sender, EventArgs e)
        {
            EditMovieWindow editMovieWindow = new EditMovieWindow((Movie)dataGrid.SelectedItem);
            editMovieWindow.ShowDialog();
            controller.ReloadMovies();

        }

        public void AddMovie(object sender, EventArgs e)
        {
            EditMovieWindow editMovieWindow = new EditMovieWindow(new Movie()
            {
                Director = controller.Directors.FirstOrDefault(),
                Genre = controller.Genres.FirstOrDefault(),
                Title = "Neuer Film"
            });
            editMovieWindow.ShowDialog();
            controller.ReloadMovies();
        }

    }


}