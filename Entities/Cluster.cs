namespace AlgorithmsWebAPI.Entities
{
    public class Cluster
    {
        public string Name { get; set; }
        public List<Point> Points { get; set; }

        public Cluster()
        {
            Points = new List<Point>();
        }
    }


}
