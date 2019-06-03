using AutoMapper;
using FunApp.Data.Models;
using FunApp.Services.Mapping;

namespace FunApp.Services.Models.Categories
{
    public class CategoryIdAndNameViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAndCount => $"{this.Name} ({this.CountOfAllJokes})";

        public int CountOfAllJokes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryIdAndNameViewModel>().ForMember(x => x.CountOfAllJokes, m => m.MapFrom(c => c.Jokes.Count));
        }
    }
}
