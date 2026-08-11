public struct BossStartData
{
    public HealthProvider Health { get; private set; }
    public string Name { get; private set; }

    public BossStartData(HealthProvider health, string name)
    {
        Health = health;
        Name = name;
    }
}
