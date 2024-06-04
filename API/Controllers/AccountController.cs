using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services.IRepositories;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController: ControllerBase {
    private readonly ILogger<AccountController> _logger; // ILogger takes the type of the class as a parameter
    private readonly IUnitOfWork _unitOfWork; // readonly means that the variable can only be assigned a value in the constructor

    public AccountController(
        ILogger<AccountController> logger,
        IUnitOfWork unitOfWork
        )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    // create a new user
    [HttpPost]
    public async Task<IActionResult> RegisterAccount(Account user) // 
    {
        if(ModelState.IsValid)
        {
            await _unitOfWork.Accounts.Add(user); // add the user to the database
            await _unitOfWork.CompleteAsync(); // save the changes to the database

            return CreatedAtAction("GetItem", new { id = user.Id }, user);
        }

        return new JsonResult("Something went wrong"){
            StatusCode = 500
        };
    }

    //update a user
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(int id, Account user)
    {
        if(id != user.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.Accounts.Upsert(user.Id, user);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    //delete a user
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        var user = await _unitOfWork.Accounts.GetById(id);
        if(user == null)
        {
            return NotFound();
        }

        await _unitOfWork.Accounts.Delete(id);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}