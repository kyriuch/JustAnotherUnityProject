using System.Threading.Tasks;
using JustAnotherUnityProject.Common.Scripts.StaticExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JustAnotherUnityProject.IntroScene.Scripts
{
    internal class GameBootstrap : MonoBehaviour
    {
        [SerializeField] string _introSceneName;
        [SerializeField] string _nextSceneName;
        
        bool _bootstrapAnimationFinished;
        
        async void Awake()
        {
            await BootstrapApplication();
        }

        async Task BootstrapApplication()
        {
            await Task.Delay(10);
            Task readyToLoadNextSceneTask = TaskExt.WaitUntil(IsBootstrapAnimationFinished);
            AsyncOperation loadSceneOp = SceneManager.LoadSceneAsync(_nextSceneName);
            loadSceneOp.allowSceneActivation = false;

            await readyToLoadNextSceneTask;

            loadSceneOp.allowSceneActivation = true;
        }

        bool IsBootstrapAnimationFinished()
        {
            return _bootstrapAnimationFinished;
        }
        
        internal async void MarkBootstrapAnimationFinished()
        {
            await Task.Delay(100);

            _bootstrapAnimationFinished = true;
        }

        internal void RestartApplication()
        {
            DestroyImmediate(gameObject);
            SceneManager.LoadSceneAsync(_introSceneName);
        }
    }
}
