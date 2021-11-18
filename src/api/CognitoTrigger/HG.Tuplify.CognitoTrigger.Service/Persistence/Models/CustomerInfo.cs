using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HG.Tuplify.CognitoTrigger.Service.Persistence.Models
{
    public record CustomerInfo
    {
        public CustomerInfo()
        {
            CreateDate = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerSurname { get; set; }

        [Required]
        public DateTime CreateDate { get; init; }
    }
}
