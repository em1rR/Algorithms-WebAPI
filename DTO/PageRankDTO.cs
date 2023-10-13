namespace AlgorithmsWebAPI.DTO
{
    public class PageRankDTO
    {
        public Dictionary<string, List<string>>? outGoings { get; set; }
        public Dictionary<string, List<string>>? inComings { get; set; }

        public Dictionary<string, double>? rankList { get; set; }
    }
}
