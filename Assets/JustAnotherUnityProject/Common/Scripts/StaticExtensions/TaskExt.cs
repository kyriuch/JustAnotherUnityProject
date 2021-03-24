using System;
using System.Threading.Tasks;

namespace JustAnotherUnityProject.Common.Scripts.StaticExtensions
{
    internal static  class TaskExt
    {
        internal static async Task WaitUntil(Func<bool> condition)
        {
            Task waitTask = Task.Run(async () =>
            {
                while (condition() == false) await Task.Delay(25);
            });

            await waitTask;
        }
    }
}