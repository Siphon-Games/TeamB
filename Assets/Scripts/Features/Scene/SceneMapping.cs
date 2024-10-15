using System.Collections.Generic;

public static class SceneMapping
{
    private static readonly Dictionary<SceneEnum, string> sceneMap = new Dictionary<
        SceneEnum,
        string
    >()
    {
        { SceneEnum.Scene1, "Scene1" },
        { SceneEnum.Scene2, "Scene2" },
    };

    public static string GetSceneAddress(SceneEnum scene)
    {
        return sceneMap[scene];
    }
}
