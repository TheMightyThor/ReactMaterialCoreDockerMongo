using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
[BsonIgnoreExtraElements]
public class Quote {

    public string Symbol {get; set;}
    public double close {get; set;}
    public double rsi {get; set;}
}