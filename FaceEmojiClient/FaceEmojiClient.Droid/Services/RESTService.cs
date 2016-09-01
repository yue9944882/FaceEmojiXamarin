using Android.Content;
using FaceEmojiClient.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


[assembly: Xamarin.Forms.Dependency(typeof(FaceEmojiClient.Droid.RESTService))]
namespace FaceEmojiClient.Droid
{
    public class RESTService : NetService
    {
        public string upload(byte[] image)
        {
            if (AppGlobalData.context == null) return null;
            ContextWrapper cw = new ContextWrapper(AppGlobalData.context);
            Java.IO.File directory = cw.GetDir("imgDir", FileCreationMode.WorldReadable);
            Java.IO.File myPath = new Java.IO.File(directory, "tmpimgfile");
            FileStream fs = new FileStream(myPath.AbsolutePath, FileMode.OpenOrCreate);
            //fs.Write(image, 0, image.Length);
            string ret = UploadFilesToRemoteUrl("http://joi-testp.chinacloudapp.cn:8090/image/upload/jinmin", fs);
            fs.Close();
            return ret;
        }



        public static string UploadFilesToRemoteUrl(string url, FileStream fs)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" +
                                    boundary;
            request.Method = "POST";
            request.KeepAlive = true;

            Stream memStream = new System.IO.MemoryStream();

            var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
                                                                    boundary + "\r\n");
            var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
                                                                        boundary + "--");


            string formdataTemplate = "\r\n--" + boundary +
                                        "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            string headerTemplate =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                "Content-Type: application/octet-stream\r\n\r\n";

            memStream.Write(boundarybytes, 0, boundarybytes.Length);
            var header = string.Format(headerTemplate, "file", fs.Name);
            var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

            memStream.Write(headerbytes, 0, headerbytes.Length);

            using (var fileStream = fs)
            {
                var buffer = new byte[1024];
                var bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
            }

            memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            request.ContentLength = memStream.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            }

            request.Method = "POST";
            string cont = request.ToString();
            using (var response = request.GetResponse())
            {
                Stream stream2 = response.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                return reader2.ReadToEnd();
            }
            fs.Close();
        }

        public static string[] getMyPhoto(string username)
        {
            string url = "http://joi-testp.chinacloudapp.cn:8090/image/enum/" + username;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            using(var response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                JArray root = JArray.Parse(json);
                string[] ret = new string[root.Count];
                for(int i = 0; i < root.Count; i++)
                {
                    ret[i] = (string)root[i];
                }
                return ret;
            }
        }


    }




}
