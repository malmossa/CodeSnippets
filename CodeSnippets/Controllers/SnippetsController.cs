using CodeSnippets.Data.Services;
using CodeSnippets.Data;
using CodeSnippets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeSnippets.Controllers
{
    public class SnippetsController : Controller
    {
        private readonly ISnippetsService _snippetsService;

        public SnippetsController(ISnippetsService snippetsService)
        {
            _snippetsService = snippetsService;
        }

        // GET: Snippets
        public async Task<IActionResult> Index()
        {
            return View(await _snippetsService.GetAll().ToListAsync());
        }

        // GET: Snippets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _snippetsService.GetById(id);
                
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // GET: Snippets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Snippets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,IdentityUserId,CreatedDate")] Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                await _snippetsService.Add(snippet);
                return RedirectToAction(nameof(Index));
            }
           
            return View(snippet);
        }

        // GET: Snippets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _snippetsService.GetById(id);

            if (snippet.IdentityUserId != User.FindFirstValue(ClaimTypes.NameIdentifier) || snippet == null)
            {
                return NotFound();
            }

           
            return View(snippet);
        }

        // POST: Snippets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,IdentityUserId")] Snippet snippet)
        {
            if (id != snippet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               await _snippetsService.Update(snippet);
                return RedirectToAction(nameof(Index));
            }

            return View(snippet);
        }

        // GET: Snippets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _snippetsService.GetById(id);

            if (snippet.IdentityUserId != User.FindFirstValue(ClaimTypes.NameIdentifier) || snippet == null)
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
            var snippet = await _snippetsService.GetById(id);

            if (snippet != null)
            {
                await _snippetsService.Delete(snippet);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
