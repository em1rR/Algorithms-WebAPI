using AlgorithmsWebAPI.DTO;
using AlgorithmsWebAPI.Entities;
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
        private readonly IKMeansService _kMeansService;
        private readonly IKNNService _kNNService;
        public AlgorithmsController(IBubbleSortService bubbleSortService, ISelectionSortService selectionSortService, IInsertionSortService insertionSortService, IDFSService dfsService, IPageRankService pageRankService, IKMeansService kMeansService, IKNNService kNNService)
        {
            _bubbleSortService = bubbleSortService;
            _selectionSortService = selectionSortService;
            _insertionSortService = insertionSortService;
            _dfsService = dfsService;
            _pageRankService = pageRankService;
            _kMeansService = kMeansService;
            _kNNService = kNNService;

        }

        #region BubbleSort

        [HttpPost]
        [Route("BubbleSort")]
        public async Task<string[]> BubbleSort(string array)
        {
            return await _bubbleSortService.Sort(array);
        }

        #endregion
        #region SelectionSort
        [HttpPost]
        [Route("SelectionSort")]
        public async Task<string[]> Selection(string array)
        {
            return await _selectionSortService.Sort(array);
        }
        #endregion
        #region InsertionSort
        [HttpPost]
        [Route("InsertionSort")]
        public async Task<string[]> InsertionSort(string array)
        {
            return await _insertionSortService.Sort(array);
        }
        #endregion
        #region DepthFirstSearch
        [HttpPost]
        [Route("DepthFirstSearch")]
        public string DFSSearch(string array)
        {
            _dfsService.Search("sea");
            return "dfs girdi";
        }
        #endregion
        #region PageRank
        [HttpGet]
        [Route("PageRank")]
        public PageRankDTO PageRank([FromQuery] string selection)
        {
            return _pageRankService.pageRank(selection);
            //return "ss";
        }
        #endregion
        
        [HttpGet]
        [Route("GetData")]
        public DataDTO GetData([FromQuery] string selection)
        {
            DataDTO data = new DataDTO();
            data.DataSet = _pageRankService.GetData(selection);
            return data;
        }

        #region K-Means
        [HttpGet]
        [Route("K-Means")]
        public string GetKMeans()
        {
            List<Cluster> clusters;
            clusters = _kMeansService.StartVer();
            
            return "k-means";
        }
        #endregion

        #region KNN
        [HttpGet]
        [Route("KNN")]
        public string GetKNN()
        {
            List<Cluster> clusters;
            clusters = _kMeansService.StartVer();
            _kNNService.StartVer(clusters);

            return "kNN";
        }
        #endregion
    }
}
