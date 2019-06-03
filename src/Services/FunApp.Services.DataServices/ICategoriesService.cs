using FunApp.Services.Models.Categories;
using System.Collections.Generic;

namespace FunApp.Services.DataServices
{
    public interface ICategoriesService
    {
        IEnumerable<CategoryIdAndNameViewModel> GetAll();

        bool IsCategoryIdValid(int categoryId);
    }
}
