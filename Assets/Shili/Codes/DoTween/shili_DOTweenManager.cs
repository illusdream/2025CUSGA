using DG.Tweening;
using ilsFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shili_DOTweenManager : MonoBehaviour
{
    private static Shili_DOTweenManager instance;
    public static Shili_DOTweenManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Shili_DOTweenManager").AddComponent<Shili_DOTweenManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    /// <summary>
    /// 渐入
    /// </summary>
    /// <param name="group"></param>
    /// <param name="rect"></param>
    public void FadePanel(CanvasGroup group, RectTransform rect)
    {
        Sequence seq = DOTween.Sequence();

        // 透明渐入（防止视觉断帧）
        seq.Append(group.DOFade(1, 1f).From(0));
        seq.SetUpdate(true);
    }
    public void PlayButtonPress(RectTransform btn)
    {
        // 3D 空间按压动画（仿 Material Design）
        btn.DOPunchScale(new Vector3(-0.5f, -0.5f, 1f), 0.2f, 2, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() => btn.DOScale(Vector3.one, 0.2f))
            .SetUpdate(true);
    }
    public Dictionary<RectTransform, bool> _panelLockStates = new Dictionary<RectTransform, bool>();
    /// <summary>
    /// 播放面板从下向上的入场动画
    /// </summary>
    /// <param name="panel">目标面板的RectTransform</param>
    /// <param name="canvasGroup">关联的CanvasGroup</param>
    /// <param name="duration">动画总时长（默认1秒）</param>
    /// <param name="easeType">位移缓动类型（默认OutExpo）</param>
    public void PlayPanelEnter(
        RectTransform panel,
        CanvasGroup canvasGroup,
        float duration = 1f,
        Ease easeType = Ease.OutExpo)
    {
        // 空引用保护
        if (panel == null || canvasGroup == null)
        {
            Debug.LogError("动画目标不能为空!");
            return;
        }
        // 初始化面板锁状态
        if (!_panelLockStates.ContainsKey(panel))
        {
            _panelLockStates.Add(panel, false);
        }
        // 检查当前面板是否已被锁定
        if (_panelLockStates[panel]) return;
        // 锁定面板状态
        _panelLockStates[panel] = true;
        // 终止旧动画防止叠加
        DOTween.Kill(panel);
        DOTween.Kill(canvasGroup);

        // 计算动态起始位置
        Vector2 originalPos = panel.anchoredPosition;
        // 设置初始状态
        panel.anchoredPosition = new Vector2(originalPos.x, -100);
        canvasGroup.alpha = 0;
        // 创建受控动画序列
        Sequence seq = DOTween.Sequence()
            .Join(panel.DOAnchorPosY(0, duration)
                .SetEase(easeType)
            )
            .Join(canvasGroup.DOFade(1, duration * 0.8f)) // 透明度稍快完成
            .SetUpdate(true) // 无视Time.timeScale
            .OnKill(() => // 动画被强制终止时恢复最终状态
            {
                _panelLockStates[panel] = false;
                panel.anchoredPosition = originalPos;
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            });

        // 绑定对象引用便于后续管理
        seq.SetLink(panel.gameObject);
    }
    /// <summary>
    /// 播放面板从上向下的出场动画
    /// </summary>
    /// <param name="panel">目标面板的RectTransform</param>
    /// <param name="canvasGroup">关联的CanvasGroup</param>
    /// <param name="duration">动画总时长（默认0.5秒）</param>
    /// <param name="easeType">位移缓动类型（默认OutExpo）</param>
    public void PlayPanelExit(
        RectTransform panel,
        CanvasGroup canvasGroup,
        float duration = 0.5f,
        Ease easeType = Ease.OutExpo)
    {
        // 空引用保护
        if (panel == null || canvasGroup == null)
        {
            Debug.LogError("动画目标不能为空!");
            return;
        }
        // 初始化面板锁状态
        if (!_panelLockStates.ContainsKey(panel))
        {
            _panelLockStates.Add(panel, false);
        }
        // 检查当前面板是否已被锁定
        if (_panelLockStates[panel]) return;
        // 锁定面板状态
        _panelLockStates[panel] = true;
        // 终止旧动画防止叠加
        DOTween.Kill(panel);
        DOTween.Kill(canvasGroup);

        // 计算动态起始位置
        Vector2 originalPos = panel.anchoredPosition;
        canvasGroup.alpha = 1;

        // 创建受控动画序列
        Sequence seq = DOTween.Sequence()
            .Join(panel.DOAnchorPosY(-100, duration)
                .SetEase(easeType)
            )
            .Join(canvasGroup.DOFade(0, duration * 0.8f)) // 透明度稍快完成
            .SetUpdate(true) // 无视Time.timeScale
            .OnKill(() => // 动画被强制终止时恢复最终状态
            {
                _panelLockStates[panel] = false;
                panel.anchoredPosition = originalPos;
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            });

        // 绑定对象引用便于后续管理
        seq.SetLink(panel.gameObject);
    }
}
/*    public class PanelAnimator : MonoBehaviour
    {
        private void PlayAnimation(RectTransform _rectTransform, CanvasGroup _canvasGroup)
        {
            // 保存原始位置
            Vector2 _originalPosition = _rectTransform.anchoredPosition;
            // 设置初始位置在屏幕下方（Y轴偏移自身高度的两倍）
            Vector2 startPosition = _originalPosition - new Vector2(0, _rectTransform.rect.height * 2);

            _rectTransform.anchoredPosition = startPosition;
            _canvasGroup.alpha = 0;
            // 创建动画序列
            Sequence sequence = DOTween.Sequence();

            // 位置动画
            sequence.Join(_rectTransform.DOAnchorPos(_originalPosition, 1));

            // 透明度动画
            sequence.Join(_canvasGroup.DOFade(1, 1));

            // 设置动画参数
            sequence
                .SetUpdate(true) // 忽略时间缩放
                .SetEase(Ease.OutQuad) // 使用缓动函数使动画更自然
                .OnComplete(() =>
                {
                    _rectTransform.anchoredPosition = _originalPosition;
                    _canvasGroup.alpha = 1;
                });
        }
    }
}*/