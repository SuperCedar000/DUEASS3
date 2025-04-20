using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // 场景名称
    public string sceneName;

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
