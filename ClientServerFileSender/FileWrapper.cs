using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerFileSender
{
    [Serializable]
    public class FileWrapper
    {
        public String Filename { get; set; }
        public byte[] Data { get; set; }
    }
}