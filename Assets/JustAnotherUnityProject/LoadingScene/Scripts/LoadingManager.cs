using DG.Tweening;
using JustAnotherUnityProject.Common.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JustAnotherUnityProject.LoadingScene.Scripts
{
    internal class LoadingManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _loadingProgressText;
        [SerializeField] Slider _loadingSlider;
        
        [Inject] PlayerProfile _playerProfile;
        [Inject] ServerPlayerProfile _serverPlayerProfile;
        
        async void Start()
        {
            IInitializableAsync[] initializables = {
                _playerProfile,
                _serverPlayerProfile
            };
            
            for (int i = 0; i < initializables.Length; i++)
            {
                UpdateProgress(i, initializables.Length);
                await initializables[i].InitializeAsync();
            }
            
            UpdateProgress(initializables.Length, initializables.Length);
        }

        void UpdateProgress(float currentStep, float maxSteps)
        {
            _loadingProgressText.text = $"Loading {(int) (currentStep / maxSteps * 100)}%";
            _loadingSlider.DOKill();
            _loadingSlider.DOValue(currentStep / maxSteps, 0.25f);
        }
    }
}