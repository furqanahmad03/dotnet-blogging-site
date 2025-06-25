using System.Collections.Generic;
using X.Models;

namespace X.Models
{
    public class DashboardViewModel
    {
        public Blog blog { get; set; } = new Blog();
        public List<Blog> blogs { get; set; } = default!;
    }

}