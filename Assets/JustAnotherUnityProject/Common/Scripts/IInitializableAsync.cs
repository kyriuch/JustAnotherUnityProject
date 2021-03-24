using System.Threading.Tasks;

namespace JustAnotherUnityProject.Common.Scripts
{
    internal interface IInitializableAsync
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
    }
}