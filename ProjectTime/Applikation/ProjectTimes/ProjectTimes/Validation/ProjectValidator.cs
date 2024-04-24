using ProjectTimes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimes.Validation
{
 
    // This does the validation for a project
    // it works as an extension and is a simple validator.
    // the returned string contains  all errors that occured, separated with a new line character
    public static class ProjectValidator
    {
        public static string? Validate(this Project project)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(project.Name))
            {
                errors.Add("The project name must not be empty.");
            }
            if (string.IsNullOrWhiteSpace(project.Description))
            {
                errors.Add("The description must not be empty.");
            }
            if (project.Budget <= 0)
            {
                errors.Add("The budget must be a positve value greater than 0.");
            }
            if (errors.Count > 0)
            {
                return String.Join("\n", errors);
            }
            return null;
        }
    }
}
