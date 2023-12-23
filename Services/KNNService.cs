using AlgorithmsWebAPI.Entities;

namespace AlgorithmsWebAPI.Services
{
    public interface IKNNService
    {
        string StartVer(List<Cluster> clusters, Point newPoint);
    }
    public class KNNService : IKNNService
    {
        static private List<Cluster>? _clusters;
        static private string? _clusterName;
        public string StartVer(List<Cluster> clusters, Point newPoint)
        {
            _clusters = clusters;
            // Yeni veri noktası
            //Point newPoint = new Point(4, 6);

            // k-NN algoritmasıyla en yakın 3 noktayı bul
            int k = 3;
            //clusters[0].Name = "Cluster1";
            //clusters[1].Name = "Cluster2";
            //clusters[2].Name = "Cluster3";
            List<Point> nearestNeighbors = KNearestNeighbors(newPoint, k);

            // En yakın noktaları ekrana yazdır
            Console.WriteLine("Yeni veri noktası: " + $"({newPoint.X}, {newPoint.Y})");
            Console.WriteLine("En yakın " + k + " nokta:");
            foreach (Point neighbor in nearestNeighbors)
            {
                Console.WriteLine($"({neighbor.X}, {neighbor.Y})");
            }

            return _clusterName;
        }

        static List<Point> dataPoints = new List<Point>
        {
            new Point(8, 7), new Point(9, 9), new Point(5, 5), new Point(6, 8),
            new Point(1, 2), new Point(2, 3), new Point(3, 4),
            new Point(12, 13), new Point(15, 16), new Point(10, 12)
        };      

        // k-NN algoritması
        static List<Point> KNearestNeighbors(Point newPoint, int k)
        {
            // Yeni veri noktasının mesafelerini hesapla ve sırala
            dataPoints.Sort((p1, p2) => Distance(p1, newPoint).CompareTo(Distance(p2, newPoint)));

            // En yakın k noktayı al
            List<Point> nearestNeighbors = dataPoints.GetRange(0, Math.Min(k, dataPoints.Count));

            Voting(nearestNeighbors);
            return nearestNeighbors;
        }

        static void Voting(List<Point> nearestNeighbors)
        {
            //foreach (Point neighbor in nearestNeighbors)
            //{
            //    string label = neighbor.ToString();
            //    foreach(Cluster cluster in clusters)
            //    {
            //        foreach(Point point in cluster.Points)
            //        {
            //            if(neighbor == point)
            //            {

            //            }
            //        }
            //    }
            //}
            Dictionary<string, int> classVotes = new Dictionary<string, int>();
            foreach (Point neighbor in nearestNeighbors)
            {
                string label = GetLabel(neighbor); // Eğer etiketleriniz varsa burada ilgili fonksiyonu kullanabilirsiniz
                if (!classVotes.ContainsKey(label))
                {
                    classVotes[label] = 0;
                }
                classVotes[label]++;
            }
            KeyValuePair<string, int> maxEntry = classVotes.Aggregate((x, y) => x.Value > y.Value ? x : y);
            int maxValue = maxEntry.Value;

            string maxKey = maxEntry.Key;
            Console.WriteLine($"En büyük değer: {maxValue}, Anahtar: {maxKey}");
            _clusterName = maxKey;

        }


        static string GetLabel(Point _point)
        {
            string label = "";

            
            foreach (Cluster cluster in _clusters)
            {
                foreach (Point point in cluster.Points)
                {
                    if (_point.X == point.X && _point.Y == point.Y)
                    {
                        label = cluster.Name;
                        return label;
                    }
                }
            }
            
            return label;
        }

        // İki nokta arasındaki mesafeyi hesapla (Öklid mesafesi)
        static double Distance(Point p1, Point p2)
        {
            int dx = (int)(p2.X - p1.X);
            int dy = (int)(p2.Y - p1.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

    }

    //class Point
    //{
    //    public double X { get; set; }
    //    public double Y { get; set; }

    //    public Point(double x, double y)
    //    {
    //        X = x;
    //        Y = y;
    //    }

    //    public double DistanceTo(Point other)
    //    {
    //        double dx = X - other.X;
    //        double dy = Y - other.Y;
    //        return Math.Sqrt(dx * dx + dy * dy);
    //    }
    //}

    //class Cluster
    //{
    //    public List<Point> Points { get; set; }

    //    public Cluster()
    //    {
    //        Points = new List<Point>();
    //    }
    //}
}
