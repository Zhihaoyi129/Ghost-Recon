using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;  // 引用 TextMeshPro

public class SceneSwitcher : MonoBehaviour
{
    public Button switchButton;  // 按钮
    public TMP_InputField sceneInputField;  // 使用 TMP_InputField 代替 InputField

    void Start()
    {
        // 给按钮绑定点击事件
        if (switchButton != null)
        {
            switchButton.onClick.AddListener(OnButtonClick);
        }
    }

    // 按钮点击事件
    void OnButtonClick()
    {
        string sceneName = sceneInputField.text;  // 获取输入框中的场景名称

        // 如果输入的场景名称不为空，进行场景加载
        if (!string.IsNullOrEmpty(sceneName))
        {
            // 加载目标场景
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("请输入场景名称");
        }
    }
}
