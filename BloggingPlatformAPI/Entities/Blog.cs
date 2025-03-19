using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingPlatformAPI.Entities;

public class Blog
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(255)] public string Title { get; set; } = string.Empty;

    [Required] public string Content { get; set; } = string.Empty;

    [Required] [MaxLength(100)] public string Category { get; set; } = string.Empty;

    [MaxLength(255)] public string? Tags { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}