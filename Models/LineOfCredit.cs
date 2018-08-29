using MongoDB.Bson;
public class LineOfCredit
{   
    public ObjectId ID { get; set; }
    public decimal Balance { get; set; }
    public decimal Limit { get; set; }
    private decimal AvailableFunds { get; set;}

    public bool withdrawlFunds(decimal amountWishingToWithdrawl){
        if ((Balance + amountWishingToWithdrawl) <= Limit)
        {
            Balance += amountWishingToWithdrawl;
            AvailableFunds = Balance - amountWishingToWithdrawl;
            return true;
        }
        return false;
    }
}