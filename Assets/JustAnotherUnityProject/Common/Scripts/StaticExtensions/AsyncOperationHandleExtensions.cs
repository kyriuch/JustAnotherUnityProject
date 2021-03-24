using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace JustAnotherUnityProject.Common.Scripts.StaticExtensions
{
    internal static class AsyncOperationHandleExtensions
    {
        internal static void Release(this AsyncOperationHandle operationHandle)
        {
            Addressables.Release(operationHandle);
        }
        
        internal static void Release(this AsyncOperationHandle<GameObject> operationHandle)
        {
            Addressables.Release(operationHandle);
        }
    }
}