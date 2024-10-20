using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private SceneInstance loadedScene;

    public void LoadScene(SceneEnum scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        string sceneAddress = SceneMapping.GetSceneAddress(scene);
        Addressables
            .LoadSceneAsync(sceneAddress, new LoadSceneParameters(loadSceneMode))
            .Completed += handle =>
        {
            loadedScene = handle.Result;
        };
    }

    public void UnloadScene()
    {
        Addressables.UnloadSceneAsync(loadedScene);
    }
}
