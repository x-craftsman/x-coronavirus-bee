using Craftsman.Core.Infrastructure.Config;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Craftsman.Core.Infrastructure.FileManager
{
    public class SftpFileManager : IFileManager
    {
        private readonly IConfigManager _configManager;
        private readonly ConnectionInfo _connectionInfo;
        public SftpFileManager(
            IConfigManager configManager
        )
        {
            this._configManager = configManager;
            this._connectionInfo = LoadSftpInfo(configManager);
        }

        private ConnectionInfo LoadSftpInfo(IConfigManager configManager)
        {
            //设置sftp 信息
            var fileServer = _configManager.FileServer;
            if (fileServer == null)
            {
                throw new Exception("[配置信息加载失败]: 无法加载fileServer配置信息！");
            }
            var authMethod = new PasswordAuthenticationMethod(fileServer.UserName, fileServer.Password);
            return new ConnectionInfo(fileServer.Host, fileServer.UserName, authMethod);
        }

        public FileStream LoadFileStream(string filePath, Action<ulong> downloadCallback = null)
        {
            FileStream fileStream = null;
            var rootDir = _configManager.FileServer.RootDirectory;
            using (var sftp = new SftpClient(this._connectionInfo))
            {
                Stream output = null;
                sftp.Connect();
                sftp.DownloadFile($"{rootDir}/{filePath}", output, downloadCallback);
                if (output != null)
                {
                    output.CopyTo(fileStream);
                }
                sftp.Disconnect();
            }
            return fileStream;
        }

        public void UploadFile(Stream stream, string folderPath, string fileName, bool canOverride, Action<ulong> uploadCallback = null)
        {
            var rootDir = _configManager.FileServer.RootDirectory;
            using (var sftp = new SftpClient(this._connectionInfo))
            {
                sftp.Connect();
                sftp.ChangeDirectory($"{rootDir}/{folderPath}");
                stream.Position = 0;
                sftp.UploadFile(stream, fileName, canOverride, uploadCallback);
                sftp.Disconnect();
            }
        }

        public void UploadFile(Stream stream, string folderPath, string fileName)
        {
            UploadFile(stream, folderPath, fileName, true);
        }
    }
}
