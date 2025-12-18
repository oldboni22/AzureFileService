namespace ApplicationLayer.Contracts;

public class FileOutput
{
    public required string ContentType { get; init; }
    
    public required Stream Content { get; init; }
}
