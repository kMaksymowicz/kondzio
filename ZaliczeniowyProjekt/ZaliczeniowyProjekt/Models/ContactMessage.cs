using System.ComponentModel.DataAnnotations;

namespace ZaliczeniowyProjekt.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu e-mail.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wiadomość nie może być pusta.")]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
