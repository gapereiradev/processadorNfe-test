namespace INFRA.CloudServices.Interface
{
    public interface IBlobStorageService
    {
        Task UploadBlobAsync(string containerName, string blobName, string json);
        Task<Stream> DownloadBlobAsync(string containerName, string blobName);
        Task DeleteBlobAsync(string containerName, string blobName);
        string GenerateBlobDownloadLink(string containerName, string blobName);

    }
}
