using System;

public interface ICreditCheck
{
    decimal checkCreditAndCalculateLimit();
}
public class CreditCheck : ICreditCheck
{
    public decimal checkCreditAndCalculateLimit(){
        var rand = new Random();
        return rand.Next(1000,10000);
    }
}