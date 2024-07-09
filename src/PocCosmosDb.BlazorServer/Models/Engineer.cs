namespace PocCosmosDb.BlazorServer.Models;

public class Engineer
{
    public Guid id { get; set; } = Guid.NewGuid();
    public string name { get; set; } = string.Empty;
    public DateTime? birthDay { get; set; }
    public DateTime admission { get; set; }
    public string speciality { get; set; }
}