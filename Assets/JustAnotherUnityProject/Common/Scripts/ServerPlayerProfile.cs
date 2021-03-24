using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace JustAnotherUnityProject.Common.Scripts
{
    [UsedImplicitly]
    internal class ServerPlayerProfile : IInitializableAsync
    {
        [Inject] PlayerProfile _playerProfile;

        const string apiKey = "BLoREC5grLPvRth2WEfHc28nf$3z!F^v";

        public bool IsInitialized { get; private set; }
        
        public async Task InitializeAsync()
        {
            await Initialize();
        }

        async Task Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            if (_playerProfile.ServerPlayerId.CachedValue.IsNullOrWhitespace())
            {
                UnityWebRequest request = UnityWebRequest.Post("https://localhost:5001/Player", "");
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Api-Key", apiKey);

                UnityWebRequest response = await request.SendWebRequest().ToUniTask().AsTask();

                Debug.Log(response.downloadHandler.text);
            }

            IsInitialized = true;
        }
    }
}