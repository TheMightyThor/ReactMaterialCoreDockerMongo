using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

public interface IRepository{
    User getUser(string userName, string passowrd);
    string createUser(User newUser, decimal limit);
    List<User> getUsers();
    List<Stock> getStocks();

}
public class Repository : IRepository
{
    private readonly string connectionString;
    private readonly string database;
    public Repository(IConfiguration config){
        connectionString = config.GetSection("DbConfig").GetSection("connectionString").Value.ToString();
        database = config.GetSection("DbConfig").GetSection("dbName").Value.ToString();
        getConnected();
    }
    
    private IMongoDatabase _db {get; set;}
    private void getConnected(){
        
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase(database);

    }
    public string createUser(User newUser, decimal limit)
    {
        var existingUser = _db.GetCollection<User>("user").Find(u => u.UserName == newUser.UserName).FirstOrDefault();
        if(existingUser == null)
        {
            var hashedPw = hashPassword(newUser.Password);
           
            var lineOfCredit = new LineOfCredit(){
                    Limit = limit,
                    Balance = 0
            };
            newUser.LineOfCredit = lineOfCredit;
            _db.GetCollection<User>("user").InsertOne(newUser);
            return newUser.ID.ToString();
        }
        else
        {
            return existingUser.ID.ToString();
        }
    }
    public User getUser(string userName, string passowrd)
    {
        var user = _db.GetCollection<User>("user").Find(x => x.UserName == userName).First();
        var hashedPw = hashPassword(passowrd);
        if(hashedPw.Equals(user.Password)){
            return user;
        }
        return null;
    }
    public List<User> getUsers(){
        return _db.GetCollection<User>("user").Find(_ => true).ToList();
    }
    
    private string hashPassword(string password)
    {
        var sb = new StringBuilder();
        using (var hash = SHA256.Create())            
    {
        Encoding enc = Encoding.UTF8;
        Byte[] result = hash.ComputeHash(enc.GetBytes(password));

        foreach (Byte b in result)
            sb.Append(b.ToString("x2"));
    }

        return sb.ToString();
    }
    public List<Stock> getStocks(){
        var b = Builders<Stock>.Projection.Slice(s => s.quotes,0, 1);
        var stocks = _db.GetCollection<Stock>("stocks").Find(_=> true).Project<Stock>(b)
        .ToList();
        return stocks;
    }

    
}