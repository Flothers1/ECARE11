﻿using ECARE.Interface.FileStorage;
using ECARE.ViewModel;

namespace ECARE.Services.FileStorage
{
    public class FileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : IFileStorageService
    {
        private readonly IWebHostEnvironment env = env;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

        public  Task DeleteFile(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return Task.CompletedTask;
            }
            var fileName = Path.GetFileName(fileRoute);
            var fileDirectory = Path.Combine(env.WebRootPath, containerName, fileName);

            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }
            return Task.CompletedTask;
        }

        public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(containerName, file);
        }
        //save
        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            var fileName =$"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            string folder = Path.Combine(env.WebRootPath, containerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/{containerName}/{fileName}";
        }
    }
}
