using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker_PR.Models
{
    public class ExpenseModel
    {
        [Key]
        public int ExpensesID { get; set; }

        [Display(Name = "Category Name:")]
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        [Required]
        [StringLength(50)]
        public string Amount { get; set; }
        
        [StringLength(100)]
        public string Description { get; set; }

        [Required]        
        public DateTime Date { get; set; } = DateTime.Now;


    }
}
