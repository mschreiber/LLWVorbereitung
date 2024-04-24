using ProjectTimes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjectTimes.Validation;

namespace ProjectTimes
{
    /// <summary>
    /// Interaktionslogik für AddEditProjectWindow.xaml
    /// </summary>
    public partial class AddEditProjectWindow : Window
    {


        public Project Project { get; }
        private readonly ProjectsContext projectsContext;
        public ObservableCollection<Employee> Employees { get; set; }

        public AddEditProjectWindow(Project project, ProjectsContext projectsContext)
        {
            InitializeComponent();
            Project = project;
            this.projectsContext = projectsContext;
            Employees = projectsContext.Employees.Local.ToObservableCollection();
            DataContext = this;
        }

        private void save(object sender, EventArgs e)
        {
            // Validate before saving
            string? errors = Project.Validate();
            if (errors != null)
            {
                MessageBox.Show(errors, "Error");
            }
            else
            {

                // If it is a new project (id = 0) add 
                // the project to the context before saving all changes
                if (Project.Id == 0)
                {
                    projectsContext.Projects.Add(Project);
                }
                projectsContext.SaveChanges();
                DialogResult = true; // this closes the dialog
            }
        }

        private void cancel(object sender, EventArgs e)
        {
            // Reload the Project to discard changes, and close the dialog
            projectsContext.Entry<Project>(Project).Reload();
            DialogResult = false;
        }

    }
}
