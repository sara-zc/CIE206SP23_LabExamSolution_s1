using CIE206LabExam_trial1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIE206LabExam_trial1.Pages
{
    public class AddPetModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty]
        public Pet pet { get; set; }
        private readonly ILogger<AddPetModel> _logger;

        public AddPetModel(ILogger<AddPetModel> logger, DB mydb)
        {
            _logger = logger;
            db = mydb;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                db.AddPet(pet);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}