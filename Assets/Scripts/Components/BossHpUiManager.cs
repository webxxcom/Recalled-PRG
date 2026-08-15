using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BossHpUiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _bossText;
    [SerializeField] BarScriptUI _fillHpBar;
    [SerializeField] CanvasGroup _canvasGroup;

    [Header("Listens to")]
    [SerializeField] BossStartDataGameEvent OnBossStarted;
    [SerializeField] BossStartDataGameEvent OnBossDefeat;

    private void OnEnable()
    {
        OnBossStarted.OnEventRaised += StartBoss;
        OnBossDefeat.OnEventRaised += EndBoss;
    }
    private void OnDisable()
    {
        OnBossStarted.OnEventRaised -= StartBoss;
        OnBossDefeat.OnEventRaised -= EndBoss;
    }

    void StartBoss(BossData bossStartData)
    {
        _canvasGroup.alpha = 1;

        _fillHpBar.Init(bossStartData.Health, bossStartData.Health.Value);
        _bossText.text = bossStartData.Name;
    }

    void EndBoss(BossData bossStartData)
    {
        _canvasGroup.alpha = 0;
    }
}
