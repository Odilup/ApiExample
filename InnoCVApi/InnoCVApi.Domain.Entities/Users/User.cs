using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnoCVApi.Domain.Entities.Users
{
    public class User : BaseModel<int>
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        [Column("id")]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        [Required]
        [StringLength(maximumLength:250)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("birthdate")]
        public DateTime BirthDate { get; set; }
    }
}