using System.Text.Json;

namespace VituraHealthBackend.Services
{
    public static class JsonReaderService
    {
        public static async Task<T> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}
