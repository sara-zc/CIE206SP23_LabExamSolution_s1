using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CIE206LabExam_trial1.Models
{
    public class Pet
    {
        [Required]
        [BindProperty]
        public int id { get; set; }
        [Required]
        [BindProperty]
        public string name { get; set; }
        [Required, Range(1800, 2023, ErrorMessage = "Year can not be less than 1800 or greater than 2023")]
        [BindProperty]
        public string year { get; set; }
        [BindProperty]
        public int? ownerid { get; set; }
        [BindProperty]
        public int? vetid { get; set; }
        public string? ownername { get; set; }
        public string? vetname { get; set; }
    }
}
