﻿using CodeSnippets.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeSnippets.Data.Services
{
    public class SnippetsService : ISnippetsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public SnippetsService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IQueryable<Snippet> GetAll()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var snippets = _context.Snippet.Where(x => x.IdentityUserId == userId);

            return snippets;
        }

        public async Task<Snippet> GetById(int? id)
        {
            var snippet = await _context.Snippet.FirstOrDefaultAsync(x => x.Id == id);

            return snippet;
        }


        public Task Add(Snippet snippet)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Snippet snippet)
        {
            throw new NotImplementedException();
        }



        public Task<Snippet> Update(Snippet snippet)
        {
            throw new NotImplementedException();
        }
    }
}