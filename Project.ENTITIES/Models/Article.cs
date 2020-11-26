using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public  class Article:BaseEntity
    {
        public string ArticleHeader { get; set; }
        public string ArticleContent { get; set; }
        public int? CategoryID { get; set; }
        public int AppUserID { get; set; }

        // Relational Properties
        public virtual Category  Category{ get; set; }


    }
}
