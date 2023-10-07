using System.Collections;

namespace AlgorithmsWebAPI.Services
{
    public interface IBubbleSortService
    {
        Task<string[]> Sort(string array);
    }
    public class BubbleSortService : IBubbleSortService
    {
        public async Task<string[]> Sort(string array)
        {
            List<int> numlist = new List<int>();
            string[] inputArr = array.Split(',');
            foreach(var item in inputArr)
            {

                if (int.TryParse(item, out int number))
                {
                    numlist.Add(number);
                }
            }

            return BubbleSort(numlist);  

        }

        private string[] BubbleSort(List<int> list)
        {
            int n = list.Count;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }

            }

            string[] stringArray = list.ConvertAll(i => i.ToString()).ToArray();

            return stringArray;
        }
    }
}
