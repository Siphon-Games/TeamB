using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public SceneEnum sceneToLoad;
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindFirstObjectByType<SceneLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        sceneLoader.LoadScene(sceneToLoad, loadSceneMode);
    }
}
