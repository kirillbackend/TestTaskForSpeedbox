using System.ComponentModel.DataAnnotations;

namespace SpeedboxTestApi.Models
{
    public record class Order
    {
        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Укажите вес в граммах")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Укажите длинну в миллиметрах")]
        public int Length { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Укажите ширину в миллиметрах")]
        public int Width { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Укажите высоту в миллиметрах")]
        public int Height { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Укажите фиас отправителя")]
        public string? FiasDepartureCity { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Укажите фиас получателя")]
        public string? FiasReceivingCity { get; set; }
    }
}
