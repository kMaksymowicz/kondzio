using System.ComponentModel.DataAnnotations;

namespace ZaliczeniowyProjekt.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole 'Tytuł' jest wymagane.")]
        [StringLength(100, ErrorMessage = "Tytuł może mieć maksymalnie 100 znaków.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków.")]
        public string Description { get; set; }

        // Każde zadanie będzie powiązane z konkretnym użytkownikiem
        public string UserId { get; set; }

        // Relacja do ApplicationUser (nawigacja)
        public ApplicationUser User { get; set; }
    }
}
