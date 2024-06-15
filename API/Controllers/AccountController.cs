using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpPost]
    public async Task<IActionResult> RegisterAccount(Account user) // 
    {
        if(ModelState.IsValid)
        {
            await _unitOfWork.Accounts.Add(user); // add the user to the database
            await _unitOfWork.CompleteAsync(); // save the changes to the database

            return Created();
        }

        return new JsonResult("Something went wrong"){
            StatusCode = 500
        };
    }

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