using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using INFRA.CloudServices.Interface;
using System.Text;

namespace INFRA.CloudServices
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task UploadBlobAsync(string containerName, string blobName, string json)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                await blobClient.UploadAsync(stream, true);
            }

        }

        public async Task<Stream> DownloadBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();
            return blobDownloadInfo.Content;
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteAsync();
        }


        public string GenerateBlobDownloadLink(string containerName, string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            // Defina as permissões para o SAS token (pode ser "r" para leitura)
            BlobSasBuilder sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b", // "b" para blobs individuais
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddYears(100)
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);
                // Gere o SAS token
            string sasToken = blobClient.GenerateSasUri(sasBuilder).Query;

            // Crie a URL de download
            Uri blobUri = new Uri(blobClient.Uri, sasToken);
            return blobUri.ToString();
        }
    }
}
