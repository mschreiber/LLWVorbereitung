using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.model
{
    public class Movie
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Released { get; set; }
        public double Ranking { get; set; }
        public int FirstYearRevenue {  get; set; }
        public int Budget {  get; set; }
        public Genre? Genre { get; set; }
        public Director? Director { get; set; }

    }
}
