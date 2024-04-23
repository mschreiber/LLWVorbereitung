using Microsoft.EntityFrameworkCore;
using ProjectTimes.model;
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

        public List<Project> Projects { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ProjectsContext projectsContext = new ProjectsContext();
            this.Projects = projectsContext.Projects.Include(p => p.Leader).ToList();
            this.DataContext = this;
        }
    }
}