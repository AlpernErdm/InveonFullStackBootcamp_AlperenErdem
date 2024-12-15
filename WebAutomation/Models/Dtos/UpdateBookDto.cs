namespace WebAutomation.Models.Dtos
{
    public record UpdateBookDto(
            int Id,
            string? Title,
            string? Author,
            int PublicationYear,
            string? ISBN,
            string? Genre,
            string? Publisher,
            int PageCount,
            string? Language,
            string? Summary,
            int AvailableCopies
        );
}
