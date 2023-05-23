using CIE206LabExam_trial1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace CIE206LabExam_trial1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, DB My_dB)
        {
            _logger = logger;
            db = My_dB;
        }
        [BindProperty]
        public int PetId { get; set; }
        [BindProperty]
        public string PetName { get; set; }
        [BindProperty]
        public int age { get; set; }
        public DB db { get; set; }
        public List<Pet> myPets { get; set; }

        public void OnGet()
        {
            myPets = db.getAllPets();
            
        }
    }
}