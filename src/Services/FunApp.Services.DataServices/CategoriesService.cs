using System.Collections.Generic;
using System.Linq;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Models;

namespace FunApp.Services.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private IRepository<Category> categoriesRepo;

        public CategoriesService(IRepository<Category> categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        public IEnumerable<IdAndNameViewModel> GetAll()
        {
            var categories = this.categoriesRepo.All().OrderBy(x => x.Name).Select(x => new IdAndNameViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return categories;
        }

        public bool IsCategoryIdValid(int categoryId)
        {
            return this.categoriesRepo.All().Any(x => x.Id == categoryId);
        }
    }
}
