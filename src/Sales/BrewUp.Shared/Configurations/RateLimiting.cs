namespace BrewUp.Shared.Configurations;

public class RateLimiting
{
    public static string CurrentRateLimiter;
    public FixedWindow FixedWindow { get; set; } = new();
    public SlidingWindow SlidingWindow { get; set; } = new();
    public TokenBucket TokenBucket { get; set; } = new();
    public Concurrency Concurrency { get; set; } = new();
}

public class FixedWindow
{
    public bool Enabled { get; set; }
    public int PermitLimit { get; set; }
    public int TimeWindowInSeconds { get; set; }
    public int QueueLimit { get; set; }
}

public class SlidingWindow
{
    public bool Enabled { get; set; }
    public int PermitLimit { get; set; }
    public int TimeWindowInSeconds { get; set; }
    public int SegmentsPerWindow { get; set; }
    public int QueueLimit { get; set; }
}

public class TokenBucket
{
    public bool Enabled { get; set; }
    public int TokenLimit { get; set; }
    public int QueueLimit { get; set; }
    public int ReplenishmentPeriod { get; set; }
    public int TokensPerPeriod { get; set; }
    public bool AutoReplenishment { get; set; }
}

public class Concurrency
{
    public bool Enabled { get; set; }
    public int PermitLimit { get; set; }
    public int QueueLimit { get; set; }
}