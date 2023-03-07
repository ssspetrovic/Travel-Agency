
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers;

[Controller]
[Route("api/[controller]")]
public class DoodController : Controller
{

    private readonly DoodRepository _doodRepository = new DoodRepository();
    

    [HttpGet]
    public async Task<List<Dood>> Get()
    {
        
        return await _doodRepository.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Dood dood)
    {
        await _doodRepository.CreateAsync(dood);
        return CreatedAtAction(nameof(Get), new { id = dood.Id }, dood);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToDood(string id, [FromBody] string Id)
    {
        await _doodRepository.AddToList(id, Id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _doodRepository.DeleteAsync(id);
        return NoContent();
    }
    
}