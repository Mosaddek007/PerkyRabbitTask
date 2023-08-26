using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker_PR.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Category Name is Required!")]
        [StringLength(maximumLength:50, MinimumLength =2)]
        [Display(Name = "Category Name:")]
        public string CatName { get; set; }

        [Required(ErrorMessage = "Category Type is Required!")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        [Display(Name="Transaction Type:")]
        public string CatType { get; set; }
    }
}
