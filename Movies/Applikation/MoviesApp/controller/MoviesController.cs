using Microsoft.Data.Sqlite;
using MoviesApp.model;
using System.Collections.ObjectModel;
using System.Windows.Navigation;


namespace MoviesApp.controller
{
    class MoviesController
    {

        private readonly ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
        private readonly ObservableCollection<Director> _directors = new ObservableCollection<Director>();
        private readonly ObservableCollection<Genre> _genres = new ObservableCollection<Genre>();
        private static MoviesController? _instance;

        public ObservableCollection<Movie> Movies { get { return _movies; } }
        public ObservableCollection<Director> Directors { get { return _directors; } }
        public ObservableCollection<Genre> Genres { get { return _genres; } }

        private MoviesController()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            loadDirectors();
            loadGenres();
            loadMovies();
        }

        // Methode, damit nur eine Instanz der Klasse erstellt wird
        // Das ist hilfreich, weil dann die Objekte in den Properties für alle
        // die sie verwenden dieselben sind (die selbe Instanz)
        public static MoviesController getInstance()
        {
            if (_instance == null)
            {
                _instance = new MoviesController();
            }
            return _instance;
        }

        // Speichert das Objekt movie in der db, wenn die Id 0 ist, dann wird ein INSERT gemacht, ansonsten ein UPDATE
        public void Save(Movie movie)
        {
            using (var connection = new SqliteConnection(@"Data Source=C:\git\LLWVorbereitung\Movies\Datenbank\movies.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                if (movie.Id != 0)
                {
                    command.CommandText = @"
                                UPDATE movies set title=$title,released=$released,ranking=$ranking,
                                first_year_revenue=$first_year_revenue, budget=$budget,
                                genre_id=$genre_id, director_id=$director_id where movie_id=$movie_id";
                    command.Parameters.AddWithValue("$movie_id", movie.Id);
                } else
                {
                    command.CommandText = @"
                                INSERT INTO movies(movie_id, title,released,ranking,first_year_revenue,budget,genre_id,director_id)
                                VALUES((select max(movie_id) from movies)+1, $title,$released,$ranking,$first_year_revenue,$budget,$genre_id,$director_id);          
                ";
                }
                command.Parameters.AddWithValue("$title", movie.Title);
                command.Parameters.AddWithValue("$released", movie.Released);
                command.Parameters.AddWithValue("$ranking", movie.Ranking);
                command.Parameters.AddWithValue("$first_year_revenue", movie.FirstYearRevenue);
                command.Parameters.AddWithValue("$budget", movie.Budget);
                command.Parameters.AddWithValue("$genre_id", movie.Genre?.Id);
                command.Parameters.AddWithValue("$director_id", movie.Director?.Id);
                command.ExecuteNonQuery();
            }
        }

        // Löscht den angegebenen Film aus der Datenbank
        public void Delete(Movie movie)
        {
            using (var connection = new SqliteConnection(@"Data Source=C:\git\LLWVorbereitung\Movies\Datenbank\movies.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM movies where movie_id=$movie_id";
                command.Parameters.AddWithValue("$movie_id", movie.Id);
                command.ExecuteNonQuery();
            }
        }

        // Lädt die Movies neu (Movies Properties)
        public void ReloadMovies()
        {
            this.loadMovies();
        }

        // Lädt alle movies aus der DB und setzt entsprechend die 
        // Instanzen der abhängigen Objekte wie Genre und Director
        // Darum ist es wichtig, dass Genre und Director bereits geladen sind
        // Das ist ein wenig unschön, weil eben hier implizit erwartet wird, 
        // dass das Genre und Directors Properties befüllt sind.
        private void loadMovies()
        {
            this._movies.Clear();
            using (var connection = new SqliteConnection(@"Data Source=C:\git\LLWVorbereitung\Movies\Datenbank\movies.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
                                SELECT movie_id, title, released, ranking, first_year_revenue,
                                       budget, genre_id, director_id FROM movies m;        
                    ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int genreId = 0;
                        int directorId = 0;
                        Movie movie = new Movie();
                        movie.Id = reader.GetInt32(0);
                        movie.Title = reader.GetString(1);
                        movie.Released = reader.GetInt16(2);
                        movie.Ranking = reader.GetDouble(3);
                        movie.FirstYearRevenue = reader.GetInt32(4);
                        movie.Budget = reader.GetInt32(5);
                        genreId = reader.GetInt32(6);
                        directorId = reader.GetInt32(7);
                        movie.Genre = Genres.FirstOrDefault(x => x.Id == genreId);
                        movie.Director = Directors.FirstOrDefault(x => x.Id == directorId);
                        this._movies.Add(movie);
                    }
                }
            }
        }

        // Lädt alle Directors aus der DB und speichert sie in dem Property Directors
        private void loadDirectors()
        {
            this._directors.Clear();
            using (var connection = new SqliteConnection(@"Data Source=C:\git\LLWVorbereitung\Movies\Datenbank\movies.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
                                SELECT d.director_id, d.firstname, d.lastname 
                                FROM directors d";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Director director = new Director();
                        director.Id = reader.GetInt32(0);
                        director.Firstname = reader.GetString(1);
                        director.Lastname = reader.GetString(2);

                        this._directors.Add(director);
                    }
                }
            }
        }

        // Lädt alle Genres aus der DB und legt sie in das Property Genres
        private void loadGenres()
        {
            this._genres.Clear();
            using (var connection = new SqliteConnection(@"Data Source=C:\git\LLWVorbereitung\Movies\Datenbank\movies.db"))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT genre_id, genre FROM genre";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre();
                        genre.Id = reader.GetInt32(0);
                        genre.Name = reader.GetString(1);
                        this._genres.Add(genre);
                    }
                }
            }

        }
    }

}
