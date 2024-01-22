using System.ComponentModel.DataAnnotations;

namespace APi.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Name")]

        [StringLength(20),MinLength(4,ErrorMessage = "Must be at least 4 character long.")]
        public string Name {  get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public bool  IsActive{ get; set; }


    }
}
