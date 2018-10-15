using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using InnoCVApi.Core.Common.Serialization;

namespace InnoCVApi.API.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public void LoadJson(string json)
        {
            var serializer = new JsonNetSerializer();
            var dictionary = serializer.Deserialize<Dictionary<string, object>>(json);

            foreach (var pair in dictionary)
            {
                ConfigurationManager.AppSettings[pair.Key] = pair.Value?.ToString();
            }
        }

        public void LoadFile(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);

            using (var memoryStream = new MemoryStream(bytes))
            {
                memoryStream.Position = 0;

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    var json = reader.ReadToEnd();
                    LoadJson(json);
                }
            }
        }

        public string DatabaseConnectionString => ConfigurationManager.AppSettings["Api:Database:ConnectionString"];
    }
}
