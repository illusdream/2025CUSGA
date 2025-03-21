using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransition: MonoBehaviour
{
    [SerializeField] private Image fadeImage;    // 拖入TransitionMask
    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }
    [SerializeField] private float fadeDuration = 1.5f;
    private void OnEnable()
    {
        TransitionToScene("TestUIScene2");
    }
    private IEnumerator FadeRoutine(bool isFadeIn)
    {
        float timer = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, isFadeIn ? 1 : 0);

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime; // 使用不受时间缩放影响的DeltaTime
            fadeImage.color = Color.Lerp(startColor, targetColor, timer / fadeDuration);
            yield return null;
        }
    }
    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(TransitionSequence(sceneName));
    }

    private IEnumerator TransitionSequence(string sceneName)
    {
        // 淡入效果
        yield return StartCoroutine(FadeRoutine(true));

        // 异步加载场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 禁止自动跳转

        // 等待加载进度达到90%（allowSceneActivation=false时最大加载到90%）
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // 完成加载并切换场景
        asyncLoad.allowSceneActivation = true;
        UIManager.Instance.GetUIPanel<MenuUI>().Close();
        // 等待场景激活
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 淡出效果
        yield return StartCoroutine(FadeRoutine(false));
        UIManager.Instance.GetUIPanel<FadeImageUI>().Close();
        
    }
}
