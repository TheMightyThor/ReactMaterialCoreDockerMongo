using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    [Consumes("application/x-www-form-urlencoded")]
    public string Post(IFormCollection form)
    {
        var userName = form["userName"];
        var password = form["password"];
        var limit = _creditCheck.checkCreditAndCalculateLimit();
        _repository.createUser(userName, password, limit);
        return limit.ToString();
    }
    [HttpGet]
    [Route("users")]
    public List<User> Users(){
        return _repository.getUsers();
    }
}