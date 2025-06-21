namespace DotNetPractice.MvcChartApp.Models
{
    public class ApexBarChartModel
    {
        public List<int> ?Data { get; set; }
        public List<string> ?Categories { get; set; }
        public ApexBarChartModel(List<int> data, List<string> categories)
        {
            Data = data;
            Categories = categories;
        }
    }
}
