
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ReservaSitio.Abstraction.IService.AzureBlobStorage;
using Azure.Storage.Blobs;
using ReservaSitio.DTOs;
using Microsoft.AspNetCore.Http;

namespace ReservaSitio.Services.AzureBlodStorage
{
    public class AzureBlobClientService: IAzureBlobClientService
    {
        private readonly IConfiguration Configuration;

        public AzureBlobClientService(IConfiguration configuration){
            this.Configuration = configuration;
        }
        /*
           "containerImagenesAzureStorage": "imagenes",
          "containerAdjuntosAzureStorage": "adjuntos",
          "containerBannersAzureStorage": "banner",
        Configuration["containerBannersAzureStorage"]
         */
        public async Task<ResultDTO<MemoryStream>> DonwloadBlob(string containerName, string fileToDonwload)
        {
            ResultDTO<MemoryStream> res = new ResultDTO<MemoryStream>();
            string connectionString = Configuration["cnAzureStorage"];
           
            //string containerName = Configuration["containerBannersAzureStorage"];
            //string blobName = Configuration["containerAdjuntosAzureStorage"];

            try {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(fileToDonwload);

                var memStream = new MemoryStream();

                await blockBlob.DownloadToStreamAsync(memStream);
                res.file = memStream.ToArray();
                res.IsSuccess = true;
                res.Message = "Archivo Descargado";
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.Message = "Archivo no Descargado";
                res.MessageExeption = ex.Message;

            }
           return res;
        }

        public async Task<ResultDTO<Uri>> UploadBlob(string containerName, IFormFile fileToUpload,string newNameFile)
        {
            ResultDTO<Uri> res = new ResultDTO<Uri>();
            //string azure_ContainerName;
            /**/
            var connectionString = Configuration["cnAzureStorage"];///    
            
            try {
                var serviceClient = new BlobServiceClient(connectionString);
                var containerClient = serviceClient.GetBlobContainerClient(containerName);
                var path = saveFileToDisk(fileToUpload, "");// fileToUpload.FileName;// @"c:\temp";
                var fileName =    fileToUpload.FileName;// "Testfile.txt";
                var localFile = Path.Combine(path, fileName);
               // await File.WriteAllTextAsync(localFile, "This is a test message");
                var blobClient = containerClient.GetBlobClient(newNameFile);
                Console.WriteLine("Uploading to Blob storage");
                using FileStream uploadFileStream = File.OpenRead(localFile);
                await blobClient.UploadAsync(uploadFileStream, true);
                uploadFileStream.Close();

                res.IsSuccess = true;
                res.Message = "Archivo Cargado";   
                res.item = blobClient.Uri;
                deleteFileToDisk(fileToUpload, "");
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Archivo no Cargado";
                res.MessageExeption = ex.Message;

            }
            return res;
        }

        public string saveFileToDisk(IFormFile file, string newNameFile)
        {
            string workingDirectory = Environment.CurrentDirectory + "\\"+ Configuration["UploadFileTemp"];
            newNameFile = (newNameFile == "" ? file.FileName : newNameFile);
            //deleteFileToDisk(file, newNameFile);

            string filePath = "";
            string ruta = workingDirectory;
            if (file.Length > 0)
            {
                filePath = Path.Combine(ruta, newNameFile);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return workingDirectory;
        }


        public string deleteFileToDisk(IFormFile file, string newNameFile)
        {
            string workingDirectory = Environment.CurrentDirectory + "\\"+ Configuration["UploadFileTemp"];
            newNameFile = (newNameFile == "" ? file.FileName : newNameFile);
            string filePath = "";
            string ruta = workingDirectory;
            if (file.Length > 0)
            {
                filePath = Path.Combine(ruta, newNameFile);
                File.Delete(filePath);
            }

            return filePath;
        }

    }
}
