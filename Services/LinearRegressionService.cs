namespace AlgorithmsWebAPI.Services
{
    public interface ILinearRegressionService
    {
        double StartVer(double input);
        (double[] X, double[] Y) RawData();
        (double slope, double intercept) TrainLinearRegressionModel(double[] X, double[] Y);
    }
    public class LinearRegressionService : ILinearRegressionService
    {
        public double StartVer(double input)
        {
            var rawData = RawData();

            // Lineer regresyon modelini eğit
            var model = TrainLinearRegressionModel(rawData.X, rawData.Y);

            // Test verisi
            //double input = 6;

            // Tahmin yap
            double prediction = Predict(input, model);

            Console.WriteLine($"Girdi: {input}, Tahmin: {prediction}");
            return prediction;
        }

        // Lineer regresyon modelini eğitme fonksiyonu
        public (double slope, double intercept) TrainLinearRegressionModel(double[] X, double[] Y)
        {
            // Veri setinin boyutu
            int n = X.Length;

            // Ortalama X ve Y değerleri
            double meanX = 0;
            double meanY = 0;

            for (int i = 0; i < n; i++)
            {
                meanX += X[i];
                meanY += Y[i];
            }

            meanX /= n;
            meanY /= n;

            // Eğim (slope) ve kesme (intercept) hesaplama
            double numerator = 0;
            double denominator = 0;

            for (int i = 0; i < n; i++)
            {
                numerator += (X[i] - meanX) * (Y[i] - meanY);
                denominator += Math.Pow(X[i] - meanX, 2);
            }

            double slope = numerator / denominator;
            double intercept = meanY - slope * meanX;

            return (slope, intercept);
        }

        // Tahmin fonksiyonu
        static public double Predict(double input, (double slope, double intercept) model)
        {
            return model.intercept + model.slope * input;
        }

        public (double[] X, double[] Y) RawData() 
        {
            // Eğitim veri seti
            double[] X = { 1, 2, 3, 4, 5 };
            double[] Y = { 2, 4, 5, 4, 5 };

            return (X, Y);
        }
    }
}
