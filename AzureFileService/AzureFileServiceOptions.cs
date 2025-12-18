namespace AzureFileService;

public class AzureFileServiceOptions
{
    public const string SectionName = "AzureFileService";
    public const string ConnectionStringName = "ConnectionString";
    
    public required string ConnectionString { get; set; }
    
    public required string ContainerName { get; set; }
}
