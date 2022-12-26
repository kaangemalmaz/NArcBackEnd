namespace NArcBackEnd.Entities.Dto
{
    public class UserChangePasswordDto //Password Change
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
