using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly ICreditCheck _creditCheck;

    public UserController(IRepository repo, ICreditCheck creditCheck)
    {
        _repository = repo;
        _creditCheck = creditCheck;
    }
    [HttpGet]
    public User Get(string userName, string password)
    {
        return _repository.getUser(userName, password);
    }
    [HttpPost]
    public string Post(User newUser)
    {
        // User user = JsonConvert.DeserializeObject<User>(newUser,
        //                     new JsonSerializerSettings { 
        //                         NullValueHandling = NullValueHandling.Ignore,
        //                         MissingMemberHandling = MissingMemberHandling.Ignore
        //                     });

        var limit = _creditCheck.checkCreditAndCalculateLimit();
        var userId = _repository.createUser(newUser, limit);
        return userId;
    }
    [HttpGet]
    [Route("users")]
    public List<User> Users(){
        return _repository.getUsers();
    }
}