using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Sevices.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunApp.Services.DataServices
{
    public class JokesService : IJokesService
    {
        private IRepository<Joke> jokesRepo;

        public JokesService(IRepository<Joke> jokesRepo)
        {
            this.jokesRepo = jokesRepo;
        }

        public IEnumerable<IndexJokeViewModel> GetRandomJokes(int count)
        {
            var jokes = this.jokesRepo.All().OrderBy(x => Guid.NewGuid()).Select(x => new IndexJokeViewModel
            {
                Content = x.Content,
                CategoryName = x.Category.Name
            }).Take(count).ToList();

            return jokes;
        }

        public int GetCount()
        {
            return this.jokesRepo.All().Count();
        }
    }
}
