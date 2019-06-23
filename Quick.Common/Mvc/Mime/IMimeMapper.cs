namespace Quick.Common.Mvc.Mime
{
    public interface IMimeMapper
    {
        IMimeMapper Extend(params MimeMappingItem[] extensions);
        string GetMimeFromExtension(string fileExtension);
        string GetMimeFromPath(string filePath);
    }
}