using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    public sealed class MultipartFormData : IDisposable
    {
        public const string Boundary = "--4e47557a-d24f-4d99-a45d-09b121533989";

        public static string ContentType
        {
            get { return "multipart/form-data; boundary=" + Boundary; }
        }

        private MemoryStream _stream;
        public MemoryStream Stream
        {
            get { return _stream ?? (_stream = new MemoryStream()); }
        }

        public MultipartFormData()
        {
        }

        public void AddContent(string name, string value)
        {
            var sp = string.Format("--{0}\r\n", Boundary);
            sp += string.Format(
                "Content-Disposition: form-data; name=\"{0}\"; \r\n\r\n{1}",
                name,
                value);
            var data = Encoding.UTF8.GetBytes(sp);
            Stream.Write(data, 0, data.Length);
        }

        public void AddContent(string name, string fileName, byte[] fileData)
        {
            var sp = string.Format("--{0}\r\n", Boundary);
            sp += string.Format(
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n",
                name,
                fileName);
            var data = Encoding.UTF8.GetBytes(sp);
            Stream.Write(data, 0, data.Length);
            Stream.Write(fileData, 0, fileData.Length);
            data = Encoding.UTF8.GetBytes("\r\n");
            Stream.Write(data, 0, data.Length);
        }

        public byte[] GetPostData()
        {
            var sp = string.Format("--{0}\r\n", Boundary);
            var data = Encoding.UTF8.GetBytes(sp);
            Stream.Write(data, 0, data.Length);

            Stream.Position = 0;
            return Stream.ToArray();
        }

        public void Dispose()
        {
            if (_stream != null) _stream.Dispose();
            _stream = null;
        }
    }
}
