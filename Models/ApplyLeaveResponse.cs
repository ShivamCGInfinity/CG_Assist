namespace CG_Assist_MCPServer.Models
{
    public class ApplyLeaveResponse
    {
        public string Status { get; set; } = "";
        public string LeaveType { get; set; } = "";
        public int DaysApplied { get; set; }
        public int RemainingBalance { get; set; }
        public string Message { get; set; } = "";
    }
}