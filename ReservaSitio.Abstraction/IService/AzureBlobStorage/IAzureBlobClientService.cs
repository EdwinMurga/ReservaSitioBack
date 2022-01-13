using Microsoft.AspNetCore.Http;
using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IService.AzureBlobStorage
{
   public  interface IAzureBlobClientService
    {

        Task<ResultDTO<Uri>> UploadBlob(string containerName, IFormFile fileToUpload, string newNameFile);
        Task<ResultDTO<MemoryStream>> DonwloadBlob(string containerName, string fileToDonwload);
        string deleteFileToDisk(IFormFile file, string newNameFile );
        string saveFileToDisk(IFormFile file, string newNameFile);
    }
}
