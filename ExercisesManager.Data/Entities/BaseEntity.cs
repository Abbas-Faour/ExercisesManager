using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExercisesManager.Data.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        [Required,MaxLength(225)]
        public string CreatedBy { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }

        [MaxLength(225)]
        public string UpdateBy { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? UpdateAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}