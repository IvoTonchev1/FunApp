using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Mapping;
using FunApp.Services.Models.Jokes;
using FunApp.Sevices.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices
{
    public class JokesService : IJokesService
    {
        private IRepository<Joke> jokesRepo;
        private readonly IRepository<Category> categoriesRepo;

        public JokesService(IRepository<Joke> jokesRepo, IRepository<Category> categoriesRepo)
        {
            this.jokesRepo = jokesRepo;
            this.categoriesRepo = categoriesRepo;
        }

        public IEnumerable<IndexJokeViewModel> GetRandomJokes(int count)
        {
            var jokes = this.jokesRepo.All().OrderBy(x => Guid.NewGuid()).To<IndexJokeViewModel>().Take(count).ToList();

            return jokes;
        }

        public int GetCount()
        {
            return this.jokesRepo.All().Count();
        }

        public async Task<int> Create(int categoryId, string content)
        {
            var joke = new Joke
            {
                CategoryId = categoryId,
                Content = content
            };

            await this.jokesRepo.AddAsync(joke);
            await this.jokesRepo.SaveChangesAsync();

            return joke.Id;
        }

        public JokeDetailsViewModel GetJokeById(int id)
        {
            var joke = this.jokesRepo.All().Where(x => x.Id == id).To<JokeDetailsViewModel>().FirstOrDefault();

            return joke;
        }
    }
}
