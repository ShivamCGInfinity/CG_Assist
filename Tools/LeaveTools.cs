using CG_Assist_MCPServer.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace CG_Assist_MCPServer.Tools
{
    [McpServerToolType]
    public sealed class LeaveTools
    {
        private readonly string _filePath =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "leavebalances.json");

        [McpServerTool]
        [Description("Returns employee leave balances.")]
        public LeaveBalances GetLeaveBalances()
        {
            return LoadLeaveBalances();
        }

        [McpServerTool]
        [Description("Apply leave and update balance.")]
        public ApplyLeaveResponse ApplyLeave(string leaveType, int days)
        {
            var currentBalance = LoadLeaveBalances();
            leaveType = leaveType.Trim().ToLower();

            int remainingBalance;

            switch (leaveType)
            {
                case "work from home":

                    if (currentBalance.WorkFromHome < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Work From Home",
                            DaysApplied = days,
                            RemainingBalance = currentBalance.WorkFromHome,
                            Message = "Insufficient Work From Home balance."
                        };
                    }

                    currentBalance.WorkFromHome -= days;
                    remainingBalance = currentBalance.WorkFromHome;
                    break;

                case "casual leave":

                    if (currentBalance.CasualLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Casual Leave",
                            DaysApplied = days,
                            RemainingBalance = currentBalance.CasualLeave,
                            Message = "Insufficient Casual Leave balance."
                        };
                    }

                    currentBalance.CasualLeave -= days;
                    remainingBalance = currentBalance.CasualLeave;
                    break;

                case "sick leave":

                    if (currentBalance.SickLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Sick Leave",
                            DaysApplied = days,
                            RemainingBalance = currentBalance.SickLeave,
                            Message = "Insufficient Sick Leave balance."
                        };
                    }

                    currentBalance.SickLeave -= days;
                    remainingBalance = currentBalance.SickLeave;
                    break;

                case "earned leave":

                    if (currentBalance.EarnedLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Earned Leave",
                            DaysApplied = days,
                            RemainingBalance = currentBalance.EarnedLeave,
                            Message = "Insufficient Earned Leave balance."
                        };
                    }

                    currentBalance.EarnedLeave -= days;
                    remainingBalance = currentBalance.EarnedLeave;
                    break;

                case "leave without pay":

                    if (currentBalance.LeaveWithoutPay < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Leave Without Pay",
                            DaysApplied = days,
                            RemainingBalance = currentBalance.LeaveWithoutPay,
                            Message = "Insufficient Leave Without Pay balance."
                        };
                    }

                    currentBalance.LeaveWithoutPay -= days;
                    remainingBalance = currentBalance.LeaveWithoutPay;
                    break;

                case "marital leave":

                    if (currentBalance.MaritalLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Marital Leave",
                            DaysApplied = days,
                            RemainingBalance = currentBalance.MaritalLeave,
                            Message = "Insufficient Marital Leave balance."
                        };
                    }

                    currentBalance.MaritalLeave -= days;
                    remainingBalance = currentBalance.MaritalLeave;
                    break;

                case "bereavement leave":

                    if (currentBalance.BereavementLeave < days)
                    {
                        return new ApplyLeaveResponse
                        {
                            Status = "Failed",
                            LeaveType = "Bereavement Leave",
                            DaysApplied = days,
                            RemainingBalance = currentBalance.BereavementLeave,
                            Message = "Insufficient Bereavement Leave balance."
                        };
                    }

                    currentBalance.BereavementLeave -= days;
                    remainingBalance = currentBalance.BereavementLeave;
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

            SaveLeaveBalances(currentBalance);

            return new ApplyLeaveResponse
            {
                Status = "Success",
                LeaveType = leaveType,
                DaysApplied = days,
                RemainingBalance = remainingBalance,
                Message = $"{days} day(s) of {leaveType} applied successfully."
            };
        }

        [McpServerTool]
        [Description("Reset leave balances to default values.")]
        public string ResetLeaveBalances()
        {
            SaveLeaveBalances(new LeaveBalances
            {
                WorkFromHome = 0,
                CasualLeave = 6,
                SickLeave = 5,
                EarnedLeave = 11,
                LeaveWithoutPay = 0,
                MaritalLeave = 5,
                BereavementLeave = 3
            });

            return "Leave balances have been reset successfully.";
        }

        private LeaveBalances LoadLeaveBalances()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<LeaveBalances>(json)!;
        }

        private void SaveLeaveBalances(LeaveBalances balances)
        {
            var json = JsonSerializer.Serialize(balances, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
    }
}