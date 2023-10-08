using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;

namespace eBookStore.Client.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly eBookStore.Repository.Context.EBookStoreDbContext _context;

        public CreateModel(eBookStore.Repository.Context.EBookStoreDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PublisherId"] = new SelectList(_context.Publisher, "PublisherId", "PublisherName");
        ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleId");
            return Page();
        }

        [BindProperty]
        public User User { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
