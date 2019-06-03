using System.ComponentModel.DataAnnotations;

namespace FunApp.Web.Model.Jokes
{
    public class CreateJokeInputModel
    {
        [Required]
        [MinLength(20)]
        public string Content { get; set; }

        [ValidCategoryId]
        public int CategoryId { get; set; }
    }
}
