namespace LibraryWebApplication.Models.Dtos
{
    public record LoginDto(
        string UserNameOrEmail,
        string Password);
}
