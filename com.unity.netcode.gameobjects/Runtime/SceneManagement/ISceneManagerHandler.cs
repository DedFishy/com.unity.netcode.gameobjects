using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.Netcode
{
    /// <summary>
    /// Used to override the LoadSceneAsync and UnloadSceneAsync methods called
    /// within the NetworkSceneManager.
    /// </summary>
    public interface ISceneManagerHandler
    {
        // Generic action to call when a scene is finished loading/unloading
        struct SceneEventAction
        {
            internal uint SceneEventId;
            internal Scene Scene;
            internal Action<uint, Scene> EventAction;
            /// <summary>
            /// Used server-side for integration testing in order to
            /// invoke the SceneEventProgress once done loading
            /// </summary>
            internal Action Completed;
            internal void Invoke()
            {
                Completed?.Invoke();
                EventAction.Invoke(SceneEventId, Scene);
            }
        }

        AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode, SceneEventAction sceneEventAction);

        AsyncOperation UnloadSceneAsync(Scene scene, SceneEventAction sceneEventAction);
    }
}
