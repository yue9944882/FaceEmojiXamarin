using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceEmojiClient.Service
{
    public interface ScreenshotService
    {
        byte[] CaptureScreen();
    }

}
