public struct BossStartData
{
    public HealthResource Health { get; private set; }
    public string Name { get; private set; }

    public BossStartData(HealthResource health, string name)
    {
        Health = health;
        Name = name;
    }
}
