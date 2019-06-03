using FunApp.Services.Models.Jokes;
using FunApp.Sevices.Models.Home;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices
{
    public interface IJokesService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);

        int GetCount();

        Task<int> Create(int categoryId, string content);

        TViewModel GetJokeById<TViewModel>(int id);
    }
}
