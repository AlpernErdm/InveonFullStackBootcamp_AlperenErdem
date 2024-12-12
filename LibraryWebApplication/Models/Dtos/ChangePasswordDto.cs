namespace LibraryWebApplication.Models.Dtos
{
    public record ChangePasswordDto(Guid Id,string CurrentPassword,string NewPassword);

}
