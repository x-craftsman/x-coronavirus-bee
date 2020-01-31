using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Craftsman.Core.Infrastructure.FileManager
{
    /// <summary>
    /// 提供文件管理能力
    /// </summary>
    public interface IFileManager
    {
        FileStream LoadFileStream(string filePath, Action<ulong> downloadCallback = null);

        void UploadFile(Stream stream, string folderPath, string fileName, bool canOverride, Action<ulong> uploadCallback = null);
        void UploadFile(Stream stream, string folderPath, string fileName);
    }
}
