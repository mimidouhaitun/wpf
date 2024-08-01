namespace MyToDo.Api.Dtos
{
    public class SummaryDto
    {
#pragma warning disable
        public int Total { get; set; }
        public int CompleteCnt { get; set; }
        public string CompleteRate
        {
            get
            {
                if (Total == 0)
                {
                    return "";
                }
                else
                {
                    return ((CompleteCnt * 100.0) / Total) + "%";
                }
            }
        }
    }
}
