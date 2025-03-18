namespace DTOs.Web.WebLogs
{
    public class RequestLog
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string RequestStr { get; set; }
        public ResponseLog Response { get; set; } = new ResponseLog();
    }
}
