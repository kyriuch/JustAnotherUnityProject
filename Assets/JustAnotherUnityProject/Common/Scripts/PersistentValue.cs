using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustAnotherUnityProject.Common.Scripts.StaticExtensions;
using Newtonsoft.Json;
using Sirenix.Utilities;
using UnityEngine;

namespace JustAnotherUnityProject.Common.Scripts
{
    internal class PersistentValue <T>
    {
        [JsonProperty] internal  T CachedValue { get; private set; }

        [JsonIgnore] int _cryptoKey;
        [JsonIgnore] string _filePath;
        [JsonIgnore] readonly byte[] _cacheArray = new byte[1024];

        internal async Task InitializeAsync(string valueId)
        {
            string filePath = $"{Application.persistentDataPath}/{valueId}";
            
            PersistentValue<T> persistentValue = new PersistentValue<T>
            {
                _filePath = filePath,
                _cryptoKey = Encoding.ASCII.GetBytes(filePath).Sum(b => b)
            };

            await persistentValue.GetValueFromFileAsync();
        }

        internal async Task SetAsync(T newValue, bool saveImmediately)
        {
            CachedValue = newValue;

            if (saveImmediately)
            {
                await SaveValueToFile();
            }
        }

        internal async Task<T> GetAsync(bool ignoreCachedValue = false)
        {
            if (ignoreCachedValue == false)
            {
                return CachedValue;
            }

            CachedValue = await GetValueFromFileAsync();

            return CachedValue;
        }

        async Task SaveValueToFile()
        {
            using FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate);

            byte[] serializedArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(this));
            serializedArray.EncryptDecryptXOR(_cryptoKey);

            await fileStream.WriteAsync(serializedArray, 0, serializedArray.Length);
        }

        async Task<T> GetValueFromFileAsync()
        {
            if (File.Exists(_filePath) == false)
            {
                return default;
            }

            using FileStream fileStream = new FileStream(_filePath, FileMode.Open);

            await fileStream.ReadAsync(_cacheArray, 0, (int) fileStream.Length);

            _cacheArray.EncryptDecryptXOR(_cryptoKey);

            string decryptedString = Encoding.ASCII.GetString(_cacheArray, 0, (int) fileStream.Length);

            if (decryptedString.IsNullOrWhitespace())
            {
                return CachedValue;
            }

            PersistentValue<T> deserializeObject = JsonConvert.DeserializeObject<PersistentValue<T>>(decryptedString);

            CachedValue = deserializeObject.CachedValue;
            
            return CachedValue;
        }
    }
}
