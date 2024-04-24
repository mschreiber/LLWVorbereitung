using Microsoft.EntityFrameworkCore;
using ProjectTimes.Model;
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

namespace ProjectTimes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {

        public ObservableCollection<Project> Projects { get; set; }
        private readonly ProjectsContext projectsContext = new ProjectsContext();

        public MainWindow()
        {
            InitializeComponent();
            projectsContext.Projects.Include(p => p.Leader).Load();
            Projects = projectsContext.Projects.Local.ToObservableCollection();
            this.DataContext = this;
        }

        private void edit(object sender, RoutedEventArgs e)
        {
            // Opens the AddEditProjectWindow 
            // The currently selected project and the db context is passed to the new window
            // After the dialog gets closed, do a refresh to reflect changes in the UI
            var project = (Project)projectsGrid.SelectedItem;
            AddEditProjectWindow editWindow = new AddEditProjectWindow(project, projectsContext);
            editWindow.ShowDialog();
            projectsGrid.Items.Refresh();
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var project = (Project)projectsGrid.SelectedItem;
            var confirm = MessageBox.Show($"Should the project {project.Name} be deleted?", "Confirm Delete", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
            {
                projectsContext.Projects.Local.Remove(project);
                projectsContext.SaveChanges();
            }
        }

        private void addProject(object sender, RoutedEventArgs e)
        {
            // set some default values for the new project
            var project = new Project();
            project.Leader = projectsContext.Employees.FirstOrDefault();
            project.Name = "new project";
            AddEditProjectWindow editWindow = new AddEditProjectWindow(project, projectsContext);
            editWindow.ShowDialog();
        }
    }
}