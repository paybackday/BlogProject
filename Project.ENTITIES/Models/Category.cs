using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        // Relational Properties
        public virtual List<Article> Articles { get; set; }
        public Category()
        {
            Articles = new List<Article>();
        }
    }
}
