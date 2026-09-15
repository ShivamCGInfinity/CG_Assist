using CG_Assist_MCPServer.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace CG_Assist_MCPServer.Tools
{
    [McpServerToolType]
    public sealed class LeaveTools
    {
        private static LeaveBalances _currentBalance =
            new LeaveBalances
            {
                WorkFromHome = 0,
                CasualLeave = 6,
                SickLeave = 5,
                EarnedLeave = 11,
                LeaveWithoutPay = 0,
                MaritalLeave = 5,
                BereavementLeave = 3
            };

        [McpServerTool]
        [Description("Returns employee leave balances.")]
        public LeaveBalances GetLeaveBalances()
        {
            return _currentBalance;
        }

        [McpServerTool]
        [Description("Apply leave and update balance.")]
        public ApplyLeaveResponse ApplyLeave(string leaveType, int days)
        {
            leaveType = leaveType.Trim().ToLower();

            int remainingBalance;

            switch (leaveType)
            {
                case "work from home":

                    if (_currentBalance.WorkFromHome < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Work From Home",
                            DaysApplied = days,
                            RemainingBalance = _currentBalance.WorkFromHome,
                            Message = "Insufficient Work From Home balance."
                        };
                    }

                    _currentBalance.WorkFromHome -= days;
                    remainingBalance = _currentBalance.WorkFromHome;
                    break;

                case "casual leave":

                    if (_currentBalance.CasualLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Casual Leave",
                            DaysApplied = days,
                            RemainingBalance = _currentBalance.CasualLeave,
                            Message = "Insufficient Casual Leave balance."
                        };
                    }

                    _currentBalance.CasualLeave -= days;
                    remainingBalance = _currentBalance.CasualLeave;
                    break;

                case "sick leave":

                    if (_currentBalance.SickLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Sick Leave",
                            DaysApplied = days,
                            RemainingBalance = _currentBalance.SickLeave,
                            Message = "Insufficient Sick Leave balance."
                        };
                    }

                    _currentBalance.SickLeave -= days;
                    remainingBalance = _currentBalance.SickLeave;
                    break;

                case "earned leave":

                    if (_currentBalance.EarnedLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Earned Leave",
                            DaysApplied = days,
                            RemainingBalance = _currentBalance.EarnedLeave,
                            Message = "Insufficient Earned Leave balance."
                        };
                    }

                    _currentBalance.EarnedLeave -= days;
                    remainingBalance = _currentBalance.EarnedLeave;
                    break;

                case "leave without pay":

                    if (_currentBalance.LeaveWithoutPay < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Leave Without Pay",
                            DaysApplied = days,
                            RemainingBalance = _currentBalance.LeaveWithoutPay,
                            Message = "Insufficient Leave Without Pay balance."
                        };
                    }

                    _currentBalance.LeaveWithoutPay -= days;
                    remainingBalance = _currentBalance.LeaveWithoutPay;
                    break;

                case "marital leave":

                    if (_currentBalance.MaritalLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Marital Leave",
                            DaysApplied = days,
                            RemainingBalance = _currentBalance.MaritalLeave,
                            Message = "Insufficient Marital Leave balance."
                        };
                    }

                    _currentBalance.MaritalLeave -= days;
                    remainingBalance = _currentBalance.MaritalLeave;
                    break;

                case "bereavement leave":

                    if (_currentBalance.BereavementLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Bereavement Leave",
                            DaysApplied = days,
                            RemainingBalance = _currentBalance.BereavementLeave,
                            Message = "Insufficient Bereavement Leave balance."
                        };
                    }

                    _currentBalance.BereavementLeave -= days;
                    remainingBalance = _currentBalance.BereavementLeave;
                    break;

                default:

                    return new ApplyLeaveResponse
                    {
                        Status = "Failed",
                        LeaveType = leaveType,
                        DaysApplied = days,
                        RemainingBalance = 0,
                        Message = $"Leave type '{leaveType}' is not supported."
                    };
            }

            return new ApplyLeaveResponse
            {
                Status = "Success",
                LeaveType = leaveType,
                DaysApplied = days,
                RemainingBalance = remainingBalance,
                Message = $"{days} day(s) of {leaveType} applied successfully."
            };
        }
    }
}