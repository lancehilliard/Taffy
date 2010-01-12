using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Caching;
using System.Web.UI;
using Taffy.Configuration;
using Taffy.Sys;
using Taffy.Transform;
using Taffy.Transform.WavTransformers;

namespace Taffy.Web {
    public partial class File : Page {
        protected void Page_Load(object sender, EventArgs e) {
            var fileUrl = Request[Constants.FileSourceParameterName] ?? "http://www.theonion.com/content/files/radionews/W09_001_World_Sun.mp3";
            //var transformedBytes = Cache.Get(fileUrl) as byte[];
            //if (transformedBytes == null) {
                var downloadFileName = Path.GetTempFileName();
                DownloadFile(fileUrl, downloadFileName);
            var transformerType = Settings.TransformerType;
            var mp3Transformer = Mp3TransformerFactory.GetTransformer(transformerType);
            var wavTransformer = WavTransformerFactory.GetTransformer(transformerType);
            var transformedBytes = GetTransformedBytes(downloadFileName, mp3Transformer, wavTransformer);
                //Cache.Add(fileUrl, transformedBytes, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, Settings.NumberOfHoursToCachePodcastDownloads, 0), CacheItemPriority.Default, null);
                System.IO.File.Delete(downloadFileName);
            //}
            //else {
            //    Cache.Remove(fileUrl);
            //}
            var originalFileName = Url.GetFileName(fileUrl);
            WriteBytesToResponse(transformedBytes, "audio/mpeg", originalFileName);
            Response.End();
        }

        private static byte[] GetTransformedBytes(string sourceFileName, ITransformer mp3Transformer, ITransformer wavTransformer) {
            string wavTempFileName;
            string stretchedWavTempFileName;
            string stretchedMp3TempFileName;
            Files.CreateTemporaryFiles(out wavTempFileName, out stretchedWavTempFileName, out stretchedMp3TempFileName);
            mp3Transformer.ToWav(sourceFileName, wavTempFileName);
            wavTransformer.Stretch(wavTempFileName, stretchedWavTempFileName);
            wavTransformer.ToMp3(stretchedWavTempFileName, stretchedMp3TempFileName);
            var sourceBytes = System.IO.File.ReadAllBytes(stretchedMp3TempFileName);
            Files.DeleteFiles(new List<string> { wavTempFileName, stretchedWavTempFileName, stretchedMp3TempFileName });
            return sourceBytes;
        }

        private void WriteBytesToResponse(byte[] sourceBytes, string contentType, string outputFileName) {
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + outputFileName);
            using (var sourceStream = new MemoryStream(sourceBytes, false)) {
                sourceStream.WriteTo(Response.OutputStream);
            }
        }

        private static void DownloadFile(string url, string destinationFileName) {
            var client = new WebClient();
            client.DownloadFile(url, destinationFileName);
        }
    }
}