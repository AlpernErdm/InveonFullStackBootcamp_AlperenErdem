namespace LibraryWebApplication.Models.Dtos
{
    public record ChangePasswordUsingDto(string Email, string NewPassword, string Token);
}
