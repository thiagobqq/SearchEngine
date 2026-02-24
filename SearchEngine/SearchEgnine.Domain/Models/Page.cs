using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEgnine.Domain.Models
{
    public class Page
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}