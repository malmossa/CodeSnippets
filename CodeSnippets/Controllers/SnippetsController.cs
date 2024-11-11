using CodeSnippets.Data;
using CodeSnippets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippets.Controllers
{
    public class SnippetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SnippetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Snippets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Snippet.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Snippets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippet
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // GET: Snippets/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Snippets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,IdentityUserId,CreatedDate,UpdatedDate")] Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(snippet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", snippet.IdentityUserId);
            return View(snippet);
        }

        // GET: Snippets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippet.FindAsync(id);
            if (snippet == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", snippet.IdentityUserId);
            return View(snippet);
        }

        // POST: Snippets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,IdentityUserId,CreatedDate,UpdatedDate")] Snippet snippet)
        {
            if (id != snippet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(snippet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnippetExists(snippet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", snippet.IdentityUserId);
            return View(snippet);
        }

        // GET: Snippets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippet
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // POST: Snippets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var snippet = await _context.Snippet.FindAsync(id);
            if (snippet != null)
            {
                _context.Snippet.Remove(snippet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SnippetExists(int id)
        {
            return _context.Snippet.Any(e => e.Id == id);
        }
    }
}
