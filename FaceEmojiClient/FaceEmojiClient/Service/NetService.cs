using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceEmojiClient.Service
{
    public interface NetService
    {
        string upload(byte[] image);
    }
}
