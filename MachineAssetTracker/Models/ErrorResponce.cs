namespace MachineAssetTracker.Models
{
    public class ErrorResponce
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
