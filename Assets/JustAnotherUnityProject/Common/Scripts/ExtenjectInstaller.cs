using Zenject;

namespace JustAnotherUnityProject.Common.Scripts
{
    internal class ExtenjectInstaller : MonoInstaller<ExtenjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerProfile>().AsSingle();
            Container.Bind<ServerPlayerProfile>().AsSingle();
        }
    }
}
