using System;
using System.Net.Mail;
using AeroPackage.Api.Handlers.Interfaces;

namespace AeroPackage.Api.Handlers;

public class FileHandler : IFileHandler
{
    public void DeleteFolderWithPackageAttachments(string folderName)
    {
        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/{folderName}");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.Delete(uploadsFolder);
        }
    }

    public async Task MoveFilesToDestinationFolder(string temporalFolder, string destinationFolder)
    {
        await Task.CompletedTask;

        temporalFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/{temporalFolder}");
        destinationFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/{destinationFolder}");

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        if (Directory.Exists(temporalFolder))
        {
            string[] files = Directory.GetFiles(temporalFolder);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string completeFileRoute = Path.Combine(destinationFolder, fileName);
                File.Move(file, completeFileRoute);
            }
        }
    }

    public async Task<List<string>> UploadFilesToApi(List<IFormFile> files, string folderName)
    {
        List<string> attachments = new();
        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/{folderName}");

        if(!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        foreach (var file in files)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                attachments.Add(fileName);
            }
        }

        return attachments;
    }
}

