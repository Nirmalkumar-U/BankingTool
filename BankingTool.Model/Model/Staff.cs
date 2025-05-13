namespace BankingTool.Model
{
    public class Staff : BaseSchema
    {
        public int StaffId { get; set; }
        public int UserId { get; set; }
        public int StaffLevel { get; set; }


        // Optional Navigation Property
        public virtual Users User { get; set; }
    }
}
