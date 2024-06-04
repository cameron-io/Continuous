using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services.IRepositories;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase {
    private readonly ILogger<UserController> _logger; // ILogger takes the type of the class as a parameter
    private readonly IUnitOfWork _unitOfWork; // readonly means that the variable can only be assigned a value in the constructor

    public UserController(
        ILogger<UserController> logger,
        IUnitOfWork unitOfWork
        )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    // create a new user
    [HttpPost]
    public async Task<IActionResult> RegisterUser(User user) // 
    {
        if(ModelState.IsValid)
        {
            await _unitOfWork.Users.Add(user); // add the user to the database
            await _unitOfWork.CompleteAsync(); // save the changes to the database

            return CreatedAtAction("GetItem", new { id = user.Id }, user);
        }

        return new JsonResult("Something went wrong"){
            StatusCode = 500
        };
    }

    //get a single user
    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var user = await _unitOfWork.Users.GetById(id);
        if(user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    //get all users
    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        var users = await _unitOfWork.Users.All();
        if(users == null)
        {
            return NotFound();
        }

        return Ok(users);
    }

    //update a user
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, User user)
    {
        if(id != user.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.Users.Upsert(user.Id, user);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    //delete a user
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var user = await _unitOfWork.Users.GetById(id);
        if(user == null)
        {
            return NotFound();
        }

        await _unitOfWork.Users.Delete(id);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}