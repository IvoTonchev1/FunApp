using System.Collections.Generic;
using System.Linq;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Mapping;
using FunApp.Services.Models.Categories;

namespace FunApp.Services.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private IRepository<Category> categoriesRepo;

        public CategoriesService(IRepository<Category> categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        public IEnumerable<CategoryIdAndNameViewModel> GetAll()
        {
            var categories = this.categoriesRepo.All().OrderBy(x => x.Name).To<CategoryIdAndNameViewModel>().ToList();

            return categories;
        }

        public bool IsCategoryIdValid(int categoryId)
        {
            return this.categoriesRepo.All().Any(x => x.Id == categoryId);
        }
    }
}
