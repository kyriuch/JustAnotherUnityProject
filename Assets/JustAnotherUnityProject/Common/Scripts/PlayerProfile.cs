using System.Threading.Tasks;
using JetBrains.Annotations;

namespace JustAnotherUnityProject.Common.Scripts
{
    [UsedImplicitly]
    internal class PlayerProfile : IInitializableAsync
    {
        internal PersistentValue<string> ServerPlayerId { get; } = new PersistentValue<string>();
        internal PersistentValue<string> JwtToken { get; } = new PersistentValue<string>();

        public bool IsInitialized { get; private set; }

        public async Task InitializeAsync()
        {
            if (IsInitialized)
            {
                return;
            }
            
            await Initialize();
        }
        
        async Task Initialize()
        {
            Task[] initializeTasks = {
                ServerPlayerId.InitializeAsync("ServerPlayerId"),
                JwtToken.InitializeAsync("JwtToken")
            };

            await Task.WhenAll(initializeTasks);

            IsInitialized = true;
        }
    }
}
