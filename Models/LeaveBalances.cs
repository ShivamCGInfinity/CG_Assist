namespace CG_Assist_MCPServer.Models
{
    public class LeaveBalances
    {
        public int WorkFromHome { get; set; }
        public int CasualLeave { get; set; }
        public int SickLeave { get; set; }
        public int EarnedLeave { get; set; }
        public int LeaveWithoutPay { get; set; }
        public int MaritalLeave { get; set; }
        public int BereavementLeave { get; set; }
    }
}