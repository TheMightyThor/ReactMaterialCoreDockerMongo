using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class User
{
    [BsonId]
    public ObjectId ID { get; set;}   
    public string UserName { get; set; }
    public string EmailAddress{get; set;}
    public string FirstName {get; set;}
    public string LastName { get; set;}
    public string Address {get; set;}
    public string City { get; set;}
    public string Country { get; set;}
    public string ZipCode { get; set;}
    public string SocialSecurityNumber { get; set;}
    public string Password { get; set; }
    public LineOfCredit LineOfCredit { get; set;}
}