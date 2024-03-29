﻿using System;

namespace Moneybox.App.Domain;

public class Account
{
    public const decimal PayInLimit = 4000m;

    public const decimal FundsLow = 500m;
    public const decimal ReachPayIn = 500m;
    public Guid Id { get; set; }

    public User User { get; set; }

    public decimal Balance { get; set; }

    public decimal Withdrawn { get; set; }

    public decimal PaidIn { get; set; }
}