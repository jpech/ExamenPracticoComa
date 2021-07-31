using System.ComponentModel.DataAnnotations;

namespace Services.Models.Request
{
    public class PersonaUpdateRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la persona es requerido ")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido de la persona es requerido ")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo de la persona es requerido ")]
        [EmailAddress(ErrorMessage = "El correo no  es una dirección de correo electrónico válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La edad de la persona es requerida ")]
        public int Edad { get; set; }
    }
}
