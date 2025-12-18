namespace ApplicationLayer.Contracts;

public readonly struct FileOutput
{
    public FileMetadata Metadata { get; init; }
    
    public Stream Content { get; init; }
}
