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

public interface IRepository{
    User getUser(string userName, string passowrd);
    void createUser(string userName, string password, decimal limit);
    List<User> getUsers();

}
public class Repository : IRepository
{
    public Repository(){
        getConnected();
    }
    private static readonly string connectionString = "mongodb://localhost:27017";
    private IMongoDatabase _db {get; set;}
    private void getConnected(){
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase("commandcenter");

    }
    public void createUser(string userName, string password, decimal limit)
    {
        var existingUser = _db.GetCollection<User>("user").Find(u => u.UserName == userName).First();
        if(existingUser == null)
        {
            var hashedPw = hashPassword(password);
            var user = new User{
                UserName = userName,
                Password = hashedPw,
                LineOfCredit = new LineOfCredit(){
                    Limit = limit,
                    Balance = 0,
                }
            };
            _db.GetCollection<User>("user").InsertOne(user);
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


    
}