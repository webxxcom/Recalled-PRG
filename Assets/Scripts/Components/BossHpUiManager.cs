using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BossHpUiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _bossText;
    [SerializeField] BarScript _fillHpBar;
    [SerializeField] CanvasGroup _canvasGroup;

    [Header("Listens to")]
    [SerializeField] BossStartDataGameEvent OnBossStarted;

    private void OnEnable()
    {
        OnBossStarted.OnEventRaised += StartBoss;
    }

    private void OnDisable()
    {
        OnBossStarted.OnEventRaised -= StartBoss;
    }

    public void StartBoss(BossStartData bossStartData)
    {
        _canvasGroup.alpha = 1;

        _fillHpBar.Init(bossStartData.Health);
        _bossText.text = bossStartData.Name;
    }
}
