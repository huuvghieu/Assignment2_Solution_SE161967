using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;

namespace eBookStore.Client.Pages.Books
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
        ViewData["PublishserId"] = new SelectList(_context.Publisher, "PublisherId", "PublisherName");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Book == null || Book == null)
            {
                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
