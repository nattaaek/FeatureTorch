namespace Models;

public class Feature 
{
    public string? Title { get; set; }
    public List<Scenario> Scenarios { get; set; } = [];
}