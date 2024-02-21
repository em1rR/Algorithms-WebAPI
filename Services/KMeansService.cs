using AlgorithmsWebAPI.Entities;

namespace AlgorithmsWebAPI.Services
{
    public interface IKMeansService
    {
        List<Cluster> StartVer();
        List<Cluster> StartVer(int clusterNumber);
        List<Point> GenerateData();
    }
    public class KMeansService : IKMeansService
    {
        
        public List<Cluster> StartVer()
        {
            List<Point> dataPoints = GenerateData();
            int k = 3; // default value
            List<Point> initialCentroids = InitializeCentroids(dataPoints, k);
            List<Cluster> clusters = KMeans(dataPoints, initialCentroids);

            for (int i = 0; i < clusters.Count; i++)
            {
                Console.WriteLine($"Cluster {i + 1}:");
                foreach (var point in clusters[i].Points)
                {
                    Console.WriteLine($"({point.X}, {point.Y})");
                }
                Console.WriteLine();
            }

            return clusters;
        }

        public List<Cluster> StartVer(int clusterNumber)
        {
            List<Point> dataPoints = GenerateData();
            int k = 3; // default value
            if (clusterNumber != 0)
                k = clusterNumber;
            List<Point> initialCentroids = InitializeCentroids(dataPoints, k);
            List<Cluster> clusters = KMeans(dataPoints, initialCentroids);

            for (int i = 0; i < clusters.Count; i++)
            {
                Console.WriteLine($"Cluster {i + 1}:");
                foreach (var point in clusters[i].Points)
                {
                    Console.WriteLine($"({point.X}, {point.Y})");
                }
                Console.WriteLine();
            }

            return clusters;
        }

        public List<Point> GenerateData()
        {
            List<Point> dataPoints = new List<Point>();
            // Örnek veri noktalarını burada oluşturun
            // Örneğin:
            dataPoints.Add(new Point(1, 2));
            dataPoints.Add(new Point(2, 3));
            dataPoints.Add(new Point(3, 4));
            dataPoints.Add(new Point(8, 7));
            dataPoints.Add(new Point(9, 9));
            dataPoints.Add(new Point(12, 13));
            dataPoints.Add(new Point(15, 16));
            dataPoints.Add(new Point(5, 5));
            dataPoints.Add(new Point(10, 12));
            dataPoints.Add(new Point(6, 8));
            // ve diğer veri noktaları...

            // generate random data
            //Random random = new Random();

            //while (dataPoints.Count < 20)
            //{
            //    int randomX = random.Next(1, 100); // Örnek olarak 1 ile 100 arasında rastgele bir x değeri
            //    int randomY = random.Next(1, 100); // Örnek olarak 1 ile 100 arasında rastgele bir y değeri

            //    Point newPoint = new Point(randomX, randomY);

            //    // Oluşturulan noktanın daha önce oluşturulan noktalardan farklı olup olmadığını kontrol et
            //    if (!dataPoints.Exists(p => p.X == newPoint.X && p.Y == newPoint.Y))
            //    {
            //        dataPoints.Add(newPoint);
            //    }
            //}

            return dataPoints;
        }

        //static List<Point> InitializeCentroids(List<Point> dataPoints, int k)
        //{
        //    // Rastgele k merkezi noktasını başlatın
        //    Random random = new Random();
        //    List<Point> centroids = new List<Point>();
        //    for (int i = 0; i < k; i++)
        //    {
        //        int index = random.Next(dataPoints.Count);
        //        centroids.Add(dataPoints[index]);
        //    }
        //    return centroids;
        //}

        static List<Point> InitializeCentroids(List<Point> dataPoints, int k)
        {
            if (dataPoints == null || dataPoints.Count == 0 || k <= 0)
            {
                return null;
            }

            Random random = new Random();
            List<Point> centroids = new List<Point>();

            // Veri noktalarından rastgele farklı k merkezi noktasını seç
            while (centroids.Count < k)
            {
                int index = random.Next(dataPoints.Count);
                Point selectedPoint = dataPoints[index];

                // Seçilen nokta daha önce seçilen merkez noktalardan farklı mı diye kontrol et
                if (!centroids.Any(c => c.X == selectedPoint.X && c.Y == selectedPoint.Y))
                {
                    centroids.Add(selectedPoint);
                }
            }

            return centroids;
        }


        static List<Cluster> KMeans(List<Point> dataPoints, List<Point> centroids)
        {
            List<Cluster> clusters = new List<Cluster>();
            for (int i = 0; i < centroids.Count; i++)
            {
                clusters.Add(new Cluster(){ Name = "Cluster"+(i+1)});
            }

            bool converged = false;

            while (!converged)
            {
                // Her veri noktasını en yakın merkeze atayın
                foreach (var point in dataPoints)
                {
                    int nearestCentroidIndex = GetNearestCentroidIndex(point, centroids);
                    clusters[nearestCentroidIndex].Points.Add(point);
                }
                WriteClusters(clusters);
                // Merkezleri güncelleyin
                List<Point> newCentroids = new List<Point>();
                for (int i = 0; i < clusters.Count; i++)
                {
                    newCentroids.Add(CalculateNewCentroid(clusters[i].Points));
                }
                WriteCentroids(centroids);

                // Merkezlerin değişip değişmediğini kontrol edin
                converged = CentroidsConverged(centroids, newCentroids);

                centroids = newCentroids;

                Console.WriteLine();
                WriteCentroids(centroids);
                // Cluster'ları temizleyin
                if(!converged)
                {
                    foreach (var cluster in clusters)
                    {
                        cluster.Points.Clear();
                    }
                }
            }

            return clusters;
        }

        static int GetNearestCentroidIndex(Point point, List<Point> centroids)
        {
            int nearestCentroidIndex = 0;
            double minDistance = double.MaxValue;

            for (int i = 0; i < centroids.Count; i++)
            {
                double distance = point.DistanceTo(centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestCentroidIndex = i;
                }
            }

            return nearestCentroidIndex;
        }

        //static Point CalculateNewCentroid(List<Point> points)
        //{
        //    if (points.Count == 0)
        //    {
        //        return null;
        //    }

        //    double sumX = 0;
        //    double sumY = 0;

        //    foreach (var point in points)
        //    {
        //        sumX += point.X;
        //        sumY += point.Y;
        //    }

        //    return new Point(sumX / points.Count, sumY / points.Count);
        //}

        static Point CalculateNewCentroid(List<Point> points)
        {
            if (points == null || points.Count == 0)
            {
                return null;
            }

            double sumX = 0;
            double sumY = 0;

            foreach (var point in points)
            {
                // X ve Y değerlerini sınırla
                double clampedX = Math.Clamp(point.X, double.MinValue, double.MaxValue);
                double clampedY = Math.Clamp(point.Y, double.MinValue, double.MaxValue);

                sumX += clampedX;
                sumY += clampedY;
            }

            double centroidX = sumX / points.Count;
            double centroidY = sumY / points.Count;

            // Yeni bir Point örneği oluştur ve döndür
            return new Point((int)centroidX, (int)centroidY);
        }


        static bool CentroidsConverged(List<Point> oldCentroids, List<Point> newCentroids)
        {
            // Merkezlerin değişip değişmediğini kontrol edin
            for (int i = 0; i < oldCentroids.Count; i++)
            {
                if (oldCentroids[i].DistanceTo(newCentroids[i]) > 0.001)
                {
                    return false;
                }
            }
            return true;
        }

        static void WriteCentroids(List<Point> points)
        {
            foreach (var point in points)
            {
                Console.WriteLine(point.X + "," + point.Y);
            }
        }

        static void WriteClusters(List<Cluster> points)
        {
            foreach(var cluster in points)
            {
                foreach(var point in cluster.Points)
                {
                    Console.Write("("+point.X + "," + point.Y +")"+ "  ");
                }
                Console.WriteLine();
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
    
}
