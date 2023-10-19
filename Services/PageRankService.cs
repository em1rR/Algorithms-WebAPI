using AlgorithmsWebAPI.DTO;
using AlgorithmsWebAPI.Entities;
using AlgorithmsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgorithmsWebAPI.Services
{
    public interface IPageRankService
    {
        List<Dictionary<string, List<string>>> GetData(string mostValuable);
        PageRankDTO pageRank(string selection);
        
    }
    public class PageRankService : IPageRankService
    {
        //private readonly DBContext _context;

        //public PageRankService(DBContext context)
        //{
        //    _context = context;
        //}
        
        public PageRankDTO pageRank(string selection)
        {
            var data = new List<Dictionary<string, List<string>>>();
            var ranks = new Dictionary<string, double>();
            var pageRankDTO = new PageRankDTO();

            data = GetData(selection);
            var dicIncoming = data[0];
            var dicOutgoing = data[1];
            if(selection == "D" || selection == "d")
                ranks = GetRank();
            else
                 ranks = GetRank100();
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

            pageRankDTO.rankList = ranks;
            pageRankDTO.outGoings = dicOutgoing;
            pageRankDTO.inComings = dicIncoming;
            return pageRankDTO;
        }

        public List<Dictionary<string, List<string>>> GetData(string mostValuable)
        {
            List<Dictionary<string, List<string>>> data = new List<Dictionary<string,List<string>>>();
            if (mostValuable == "A" || mostValuable == "a")
            {
                //Dictionary<string, List<string>> dicIncoming = new Dictionary<string, List<string>>()
                //{
                //    { "A", new List<string>{"B", "D", "C"} },
                //    { "B", new List<string>{"A" } },
                //    { "C", new List<string>{ } },
                //    { "D", new List<string>{"C" } },
                //};
                //Dictionary<string, List<string>> dicOutgoing = new Dictionary<string, List<string>>()
                //{
                //    { "A", new List<string>{"B"} },
                //    { "B", new List<string>{"A" } },
                //    { "C", new List<string>{"A", "D"} },
                //    { "D", new List<string>{"A" } },
                //};
                Dictionary<string, List<string>> dicIncoming = new Dictionary<string, List<string>>();
                Dictionary<string, List<string>> dicOutgoing = new Dictionary<string, List<string>>();
                for (int i = 0; i < 100; i++)
                {
                    string node = "Node" + i;
                    List<string> incomingLinks = new List<string>();
                    List<string> outgoingLinks = new List<string>();

                    // Rastgele gelen bağlantılar ekleyelim (örneğin, 2 ile 5 arasında rastgele sayıda gelen bağlantı)
                    int numIncomingLinks = new Random().Next(2, 6);
                    for (int j = 0; j < numIncomingLinks; j++)
                    {
                        incomingLinks.Add("Node" + new Random().Next(0, 100));
                    }

                    // Rastgele çıkan bağlantılar ekleyelim
                    int numOutgoingLinks = new Random().Next(2, 6);
                    for (int j = 0; j < numOutgoingLinks; j++)
                    {
                        outgoingLinks.Add("Node" + new Random().Next(0, 100));
                    }

                    dicIncoming[node] = incomingLinks;
                    dicOutgoing[node] = outgoingLinks;
                }
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
                    { "A", new List<string>{"B", "C", "D"} },
                    { "B", new List<string>{"A", "D"} },
                    { "C", new List<string>{"B", "D"} },
                    { "D", new List<string>{ } },
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
        private Dictionary<string, double> GetRank100()
        {
            Dictionary<string, double> ranks = new Dictionary<string, double>();
            for (int i = 0; i < 100; i++)
            {
                ranks.Add("Node" + i, 0.25);
            }
            return ranks;
        }

        //public async Task<IEnumerable<Invoice>> GetInvoiceList()
        //{
        //    return await _context.invoices.ToListAsync();
        //}

    }
}
