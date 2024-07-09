﻿using Moneybox.App.DataAccess;
using Moneybox.App.Domain;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features;

public class WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
{
    public void Execute(Guid fromAccountId, decimal amount)
    {
      
        var from = accountRepository.GetAccountById(fromAccountId);

        var fromNewBalance = from.Balance - amount;
        if (fromNewBalance < 0m)
        {
            throw new InvalidOperationException("Insufficient funds to make withdraw");
        }

        if (fromNewBalance < Account.FundsLow)
        {
            notificationService.NotifyFundsLow(from.User.Email);
        }

        from.Balance = fromNewBalance;
        from.Withdrawn -=  amount;

        accountRepository.Update(from);
    }
}