using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApplication.Models
{
    /// <summary>
    /// class to define the book information
    /// </summary>
    public class BookDetails
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }
    }
}
