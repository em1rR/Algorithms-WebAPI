using AlgorithmsWebAPI.DTO;
using AlgorithmsWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmsWebAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AlgorithmsController : Controller
    {
        private readonly IBubbleSortService _bubbleSortService;
        private readonly ISelectionSortService _selectionSortService;
        private readonly IInsertionSortService _insertionSortService;
        private readonly IDFSService _dfsService;
        private readonly IPageRankService _pageRankService;
        public AlgorithmsController(IBubbleSortService bubbleSortService, ISelectionSortService selectionSortService, IInsertionSortService insertionSortService, IDFSService dfsService, IPageRankService pageRankService)
        {
            _bubbleSortService = bubbleSortService;
            _selectionSortService = selectionSortService;
            _insertionSortService = insertionSortService;
            _dfsService = dfsService;
            _pageRankService = pageRankService;
        }

        #region BubbleSort

        [HttpPost]
        [Route("BubbleSort")]
        public async Task<string[]> BubbleSort(string array)
        {
            return await _bubbleSortService.Sort(array);
        }

        #endregion

        [HttpPost]
        [Route("SelectionSort")]
        public async Task<string[]> Selection(string array)
        {
            return await _selectionSortService.Sort(array);
        }

        [HttpPost]
        [Route("InsertionSort")]
        public async Task<string[]> InsertionSort(string array)
        {
            return await _insertionSortService.Sort(array);
        }

        [HttpPost]
        [Route("DepthFirstSearch")]
        public string DFSSearch(string array)
        {
            _dfsService.Search("sea");
            return "dfs girdi";
        }

        [HttpGet]
        [Route("PageRank")]
        public PageRankDTO PageRank([FromQuery] string selection)
        {
            return _pageRankService.pageRank(selection);
            //return "ss";
        }

        [HttpGet]
        [Route("GetData")]
        public DataDTO GetData([FromQuery] string selection)
        {
            DataDTO data = new DataDTO();
            data.DataSet = _pageRankService.GetData(selection);
            return data;
        }

    }
}
