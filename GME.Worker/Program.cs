using AngleSharp;
using AngleSharp.Html.Dom;
using GME.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GME.Worker
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://gaffa.dk/live";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var selector = "div.details";
            var concerts = document.QuerySelectorAll(selector);

            var concertDetails = new List<ConcertDetails>();

            foreach(var concert in concerts)
            {
                var anchors = concert.QuerySelectorAll("h3 a").OfType<IHtmlAnchorElement>();

                var details = new ConcertDetails() {
                    Name = anchors.First().TextContent,
                    Url = anchors.First().Href,
                };

                Console.WriteLine($"Concert {details.Name} with url {details.Url} found.");
                concertDetails.Add(details);
            }

            Console.ReadLine();

        }
    }
}
