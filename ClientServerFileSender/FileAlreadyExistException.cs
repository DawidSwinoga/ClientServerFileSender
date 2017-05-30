using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerFileSender
{
    class FileAlreadyExistException : Exception
    {
        public FileAlreadyExistException()
        {
        }

        public FileAlreadyExistException(string message) : base(message)
        {
        }

        public FileAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}