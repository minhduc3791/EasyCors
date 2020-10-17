using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorsAnywhere.Models
{
    public class PostObject
    {
        [Required]
        public string HeadersList { get; set; }
        [Required]
        public string RequestUrl { get; set; }
        [Required]
        public string JsonData { get; set; }
        [Required]
        public string Method { get; set; }
    }
}
