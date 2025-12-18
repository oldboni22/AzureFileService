namespace ApplicationLayer.Contracts;

public readonly struct FileMetadata
{
    public string FileName { get; init; }
    
    public string ContentType { get; init; }
}
