using System.ComponentModel.DataAnnotations;

namespace APi.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public bool  IsActive{ get; set; }


    }
}
