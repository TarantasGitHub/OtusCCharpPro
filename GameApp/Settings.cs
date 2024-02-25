using System;

namespace GameApp;

public sealed class Settings
{
    public required int MaxAttemptCount { get; set; }
    public required int From { get; set; }
    public required int To { get; set; }
}
