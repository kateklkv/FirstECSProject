using System;
using System.Text;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

namespace Kulikova
{
    public static class GoogleDriveTools
    {
        public static void FileList(Action<FileList> onDone)
        {
            GoogleDriveFiles.List().Send().OnDone += onDone;
        }

        public static void Upload(string fileId, string obj, Action<File> onDone)
        {
            var file = new File { Name = "GameData.json", Content = Encoding.UTF8.GetBytes(obj) };
            GoogleDriveFiles.Update(fileId, file).Send().OnDone += onDone;
        }

        public static void Download(string fileId, Action<File> onDone)
        {
            GoogleDriveFiles.Download(fileId).Send().OnDone += onDone;
        }
    }
}
