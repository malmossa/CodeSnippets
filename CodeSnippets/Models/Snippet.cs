using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSnippets.Models
{
    public class Snippet
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null;

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User {  get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; }

    }
}
