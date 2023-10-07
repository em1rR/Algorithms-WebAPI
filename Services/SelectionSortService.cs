namespace AlgorithmsWebAPI.Services
{
    public interface ISelectionSortService
    {
        Task<string[]> Sort(string array);
    }
    public class SelectionSortService : ISelectionSortService
    {
        public async Task<string[]> Sort(string array)
        {
            List<int> numlist = new List<int>();
            string[] inputArr = array.Split(',');
            foreach (var item in inputArr)
            {

                if (int.TryParse(item, out int number))
                {
                    numlist.Add(number);
                }
            }

            return SelectionSort(numlist);
        }

        private string[] SelectionSort(List<int> list)
        {

            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (list[j] < list[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // Swap list[i] and list[minIndex]
                int temp = list[i];
                list[i] = list[minIndex];
                list[minIndex] = temp;
            }

            string[] stringArray = list.ConvertAll(i => i.ToString()).ToArray();

            return stringArray;
        }
    }
}
