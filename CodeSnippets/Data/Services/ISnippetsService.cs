using CodeSnippets.Models;

namespace CodeSnippets.Data.Services
{
    public interface ISnippetsService
    {
        IQueryable<Snippet> GetAll();

        Task<Snippet> GetById(int? id);

        Task Add(Snippet snippet);

        Task<Snippet> Update(Snippet snippet);

        Task Delete(Snippet snippet);
    }
}
