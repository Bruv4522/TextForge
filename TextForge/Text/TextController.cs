namespace TextForge.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TextForge.Data;

[ApiController]
[Route("api/text")]
public class TextController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Texts()
    {
        var texts = await db.Texts.ToListAsync();
        return Ok(texts);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Text([FromRoute] int id)
    {
        var text = await db.Texts.FindAsync(id);

        if (text == null)
        {
            return NotFound(new Error($"Text of id {id} not found"));
        }

        return Ok(text);
    }
}
