using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace RestApp
{
    public  class Program
    {
        private  static  readonly  HttpClient client = new HttpClient();



        static void Main(string[] args)
        {

            var repositories = ProcessRepositories().Result;

            foreach (var repo in repositories)
            {
                  Console.WriteLine(repo.Id);
                  Console.WriteLine(repo.Name);
                  Console.WriteLine(repo.FullName);

                  Console.WriteLine(repo.Description);
                  Console.WriteLine(repo.GitHubHomeUrl);
                   Console.WriteLine(repo.Watchers);
              
                 Console.ReadLine();

            }
               
        }


        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".ner fundation repo reporter");

            var getTheString = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            //  GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            var repositories = serializer.ReadObject(await getTheString) as List<Repository>;

            return repositories;

            //var msg = await getTheString;

            //Console.WriteLine(msg);

            //foreach (var repo in repositories)
            //    Console.WriteLine(repo.Name);
            //    Console.ReadKey();


        }

    }
}
