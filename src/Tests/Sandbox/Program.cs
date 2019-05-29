using AngleSharp;
using AngleSharp.Html.Parser;
using FunApp.Data;
using FunApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Sandbox
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;
                SandboxCode(serviceProvider);
            }
        }

        private static void SandboxCode(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<FunAppContext>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var webClient = new WebClient { Encoding = Encoding.GetEncoding("windows-1251") };
            var parser = new HtmlParser();

            for (int i = 3000; i < 10000; i++)
            {
                var url = "http://fun.dir.bg/vic_open.php?id=" + i;
                var html = webClient.DownloadString(url);
                var document = parser.ParseDocument(html);
                var jokeContent = document.QuerySelector("#newsbody")?.TextContent?.Trim();
                var categoryName = document.QuerySelector(".tag-links-left a")?.TextContent?.Trim();

                if (!string.IsNullOrWhiteSpace(jokeContent) && !string.IsNullOrWhiteSpace(categoryName)) { }
                {
                    var category = context.Categories.FirstOrDefault(x => x.Name == categoryName);
                    if (category == null)
                    {
                        category = new Category
                        {
                            Name = categoryName
                        };
                    }

                    var joke = new Joke
                    {
                        Category = category,
                        Content = jokeContent
                    };

                    context.Jokes.Add(joke);
                    context.SaveChanges();
                }

                Console.WriteLine($"{i} -> {categoryName}");
            }        
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables().Build();

            serviceCollection.AddDbContext<FunAppContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
