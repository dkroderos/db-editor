namespace MerrMail.Maui.Models;

public class EmailContext
{
    public required string Subject { get; set; }
    public required string Response { get; set; }
    public required string DateCreated { get; set; }
    public required string LastUpdated { get; set; }
    public required string Editor { get; set; }
}