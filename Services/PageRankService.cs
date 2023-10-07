using AlgorithmsWebAPI.Entities;
using AlgorithmsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgorithmsWebAPI.Services
{
    public interface IPageRankService
    {
        void pageRank();
    }
    public class PageRankService : IPageRankService
    {
        //private readonly DBContext _context;

        //public PageRankService(DBContext context)
        //{
        //    _context = context;
        //}
        

       

        public void pageRank()
        {
            //// A most valuable
            //Dictionary<string, List<string>> dicIncoming = new Dictionary<string, List<string>>()
            //{
            //    { "A", new List<string>{"B", "D" } },
            //    { "B", new List<string>{"A", "C", "D" } },
            //    { "C", new List<string>{"A" } },
            //    { "D", new List<string>{"A", "C" } },
            //};
            //Dictionary<string, List<string>> dicOutgoing = new Dictionary<string, List<string>>()
            //{
            //    { "A", new List<string>{"A", "B", "C", "D"} },
            //    { "B", new List<string>{"A" } },
            //    { "C", new List<string>{"B", "D"} },
            //    { "D", new List<string>{"A", "B" } },
            //};
            // D most valuable
            Dictionary<string, List<string>> dicIncoming = new Dictionary<string, List<string>>()
            {
                { "A", new List<string>{"B" } },
                { "B", new List<string>{"A" } },
                { "C", new List<string>{"A" } },
                { "D", new List<string>{"A", "C", "B" } },
            };
            Dictionary<string, List<string>> dicOutgoing = new Dictionary<string, List<string>>()
            {
                { "A", new List<string>{"A", "B", "C", "D"} },
                { "B", new List<string>{"D" } },
                { "C", new List<string>{"B", "D"} },
                { "D", new List<string>{"A", "B" } },
            };
            Dictionary<string, double> ranks = new Dictionary<string, double>()
            {
                { "A", 0.25 },
                { "B", 0.25 },
                { "C", 0.25 },
                { "D", 0.25 },
            };
            double dampingFactor = 0.85;
            int iteration = 20;

            for (int i = 0; i < iteration; i++)
            {
                foreach (var page in ranks.Keys)
                {
                    double newRank = 1 - dampingFactor;

                    foreach (var incoming in dicIncoming[page])
                    {
                        newRank += dampingFactor * (ranks[incoming] / dicOutgoing[incoming].Count);
                    };
                    newRank = Math.Round(newRank, 5);
                    ranks[page] = newRank;
                    Console.Write(page + " = " + newRank + "  ||  ");
                };
                Console.WriteLine("");
            };
        }


        //public async Task<IEnumerable<Invoice>> GetInvoiceList()
        //{
        //    return await _context.invoices.ToListAsync();
        //}

    }
}
