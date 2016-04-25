using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Details { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Question Question { get; set; }
    }
}
