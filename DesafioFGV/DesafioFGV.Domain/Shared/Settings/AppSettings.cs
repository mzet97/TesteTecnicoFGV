﻿namespace DesafioFGV.Domain.Shared.Settings;

public class AppSettings
{
    public string Secret { get; set; } = string.Empty;
    public int ExpirationHours { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string ValidOn { get; set; } = string.Empty;
}

