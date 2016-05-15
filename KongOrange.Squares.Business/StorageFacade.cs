using System;
using System.IO;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace KongOrange.Squares.Business
{
    public class StorageFacade
    {
        private const string ContainerName = "square-sets";
        private readonly CloudBlobContainer _container;

        public StorageFacade()
        {
            var connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            _container = blobClient.GetContainerReference(ContainerName);
            _container.CreateIfNotExists();
            _container.SetPermissions(new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob});
        }

        public string Store(string fileName, Stream inputStream, string userId)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(userId + "/" + fileName);
            blockBlob.UploadFromStream(inputStream);

            var uri = blockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
            return uri;
        }
    }
}
