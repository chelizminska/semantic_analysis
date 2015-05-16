using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semantic_analysis.Models
{
    public class StopWords
    {
        public int ID { get; set; }
        public string StopWord { get; set; }
    }
}