namespace AlgorithmsWebAPI.Services
{
    public interface IInsertionSortService
    {
        Task<string[]> Sort(string array);
    }
    public class InsertionSortService : IInsertionSortService
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

            return InsertionSort(numlist);
        }

        private string[] InsertionSort(List<int> list)
        {

            int n = list.Count;
            for (int i = 1; i < n; ++i)
            {
                int key = list[i];
                int j = i - 1;

                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && list[j] > key)
                {
                    list[j + 1] = list[j];
                    j = j - 1;
                }
                list[j + 1] = key;
            }

            string[] stringArray = list.ConvertAll(i => i.ToString()).ToArray();

            return stringArray;
        }
    }
}

