using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semantic_analysis.Models
{
    public class StopWordsContext: DbContext
    {
        public DbSet<StopWords> stopWords{ get; set; }
    }
}