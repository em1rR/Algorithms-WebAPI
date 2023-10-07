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
            var data = new List<Dictionary<string, List<string>>>();
            var ranks = new Dictionary<string, double>();

            data = GetData("A");
            var dicIncoming = data[0];
            var dicOutgoing = data[1];
            ranks = GetRank();

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

        private List<Dictionary<string, List<string>>> GetData(string mostValuable)
        {
            List<Dictionary<string, List<string>>> data = new List<Dictionary<string,List<string>>>();
            if (mostValuable == "A" || mostValuable == "a")
            {
                Dictionary<string, List<string>> dicIncoming = new Dictionary<string, List<string>>()
                {
                    { "A", new List<string>{"B", "D" } },
                    { "B", new List<string>{"A", "C", "D" } },
                    { "C", new List<string>{"A" } },
                    { "D", new List<string>{"A", "C" } },
                };
                Dictionary<string, List<string>> dicOutgoing = new Dictionary<string, List<string>>()
                {
                    { "A", new List<string>{"A", "B", "C", "D"} },
                    { "B", new List<string>{"A" } },
                    { "C", new List<string>{"B", "D"} },
                    { "D", new List<string>{"A", "B" } },
                };
                data.Add(dicIncoming);
                data.Add(dicOutgoing);
            }
            else if (mostValuable == "D" || mostValuable == "d")
            {
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
                data.Add(dicIncoming);
                data.Add(dicOutgoing);
            }
            return data;
        }

        private Dictionary<string, double> GetRank()
        {
            Dictionary<string, double> ranks = new Dictionary<string, double>()
            {
                { "A", 0.25 },
                { "B", 0.25 },
                { "C", 0.25 },
                { "D", 0.25 },
            };
            return ranks;
        }

        //public async Task<IEnumerable<Invoice>> GetInvoiceList()
        //{
        //    return await _context.invoices.ToListAsync();
        //}

    }
}
