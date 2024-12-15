namespace WebAutomation.Models.Dtos
{
    public record CreateBookDto(
         string Title,
         string Author,
         int PublicationYear,
         string ISBN,
         string Genre,
         string Publisher,
         int PageCount,
         string Language,
         string Summary,
         int AvailableCopies
     );
}
