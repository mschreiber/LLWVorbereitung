using MoviesApp.controller;
using MoviesApp.model;
using System.Collections.ObjectModel;
using System.Windows;

namespace MoviesApp
{
    // TODO:
    // * Im XAML statische Resources für die Validierungsfehler und andere Styles definieren
    // * Enablement vom Speichern Button abhängig von der Validierung (kein Speichern möglich, wenn Fehler vorhanden)
    // 
    public partial class EditMovieWindow : Window
    {
        public Movie Movie { get; set; }
        public ObservableCollection<Director> Directors { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        private readonly MoviesController _controller = MoviesController.getInstance();

        public EditMovieWindow(Movie movie)
        {
            InitializeComponent();
            this.Directors = _controller.Directors;
            this.Genres = _controller.Genres;
            this.Movie = movie;
            this.DataContext = this;
        }

        public void Save(object sender, EventArgs e)
        {
            this._controller.Save(this.Movie);
            this.DialogResult = true; // Schließt auch den Dialog/Fenster
        }

        public void Cancel(object sender, EventArgs e)
        {
            this.DialogResult = false; //Schließt auch den Dialog/Fenster
        }

    }
}
