using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;
using Moneybox.App.Domain;

namespace Moneybox.App.Features;

public class TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
{
    public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        WithdrawMoney wdm = new WithdrawMoney(accountRepository, notificationService);
        wdm.Execute(fromAccountId, amount);
        var to = accountRepository.GetAccountById(toAccountId);

        var paidIn = to.PaidIn + amount;
        if (paidIn > Account.PayInLimit)
        {
            throw new InvalidOperationException("Account pay in limit reached");
        }

        if (Account.PayInLimit - paidIn < Account.ReachPayIn)
        {
            notificationService.NotifyApproachingPayInLimit(to.User.Email);
        }

        to.Balance += amount;
        to.PaidIn += amount;
        
        accountRepository.Update(to);
    }
}