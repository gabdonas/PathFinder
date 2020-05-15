namespace PathFinder.Api.Model.Api
{
    public class PathFinderResultApiModel
    {
        public int[] InputArray { get; set; }
        public bool IsTraversable { get; set; }
        public int[] ResultArray { get; set; }
        public bool ResultFromDb { get; set; }
    }


    public static class PathResultApiModelExtensions
    {
        public static PathFinderResultApiModel ToApiModel(this PathResult p, bool isFromDb)
        {
            return new PathFinderResultApiModel()
            {
                InputArray = p.InputArray,
                IsTraversable = p.IsTraversable,
                ResultArray = p.ResultArray,
                ResultFromDb = isFromDb
            };
        }
    }
}