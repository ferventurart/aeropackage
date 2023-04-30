using System;
namespace AeroPackage.Api.Handlers.Interfaces;

public interface IFileHandler
{
    Task MoveFilesToDestinationFolder(string temporalFolder, string destinationFolder);
    Task<List<string>> UploadFilesToApi(List<IFormFile> files, string folderName);
    void DeleteFolderWithPackageAttachments(string folderName);
    void DeleteFile(string fileName, string folderName);
}

