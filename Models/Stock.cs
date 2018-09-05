using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
[BsonIgnoreExtraElements]
public class Stock {

    [BsonId]
    ObjectId ID {get; set;}
    [BsonElement("symbol")]
    public string Symbol {get; set;}
    [BsonElement("companyName")]
    public string CompanyName {get; set;}
    public string Sector { get; set;}
    [BsonElement("quotes")]
    public List<Quote> quotes {get; set;}
}