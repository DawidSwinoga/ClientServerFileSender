using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerFileSender
{
    class FileManager
    {
        private static readonly object syncLock = new object();

        public static void SaveFile(String path, byte[] data)
        {
            CreateFileIfDoesNotExist(path);
            File.WriteAllBytes(path, data);
        }

        private static void CreateFileIfDoesNotExist(string path)
        {
            lock (syncLock)
            {
                if (File.Exists(path))
                {
                    throw new FileAlreadyExistException();
                }

                File.Create(path).Close();
            }
        }
    }
}
