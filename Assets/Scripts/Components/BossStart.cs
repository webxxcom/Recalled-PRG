using UnityEngine;

public class BossStart : MonoBehaviour
{
    [SerializeField] GolemBossController _boss;

    [Header("Boadcasts to")]
    [SerializeField] BossStartDataGameEvent OnPlayerEntered;

    public void BeginBoss()
    {
        BossStartData bsd = new(_boss.GetComponentInChildren<HealthResource>(), _boss.name);
        
        OnPlayerEntered.Invoke(bsd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            BeginBoss();
    }
}
