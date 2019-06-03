using FunApp.Data.Models;
using FunApp.Services.Mapping;

namespace FunApp.Services.Models.Jokes
{
    public class JokeDetailsViewModel : IMapFrom<Joke>
    {
        public string Content { get; set; }

        public string CategoryName { get; set; }
    }
}
