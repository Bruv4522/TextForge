namespace TextForge.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TextForge.Data;

[ApiController]
[Route("api/text")]
public class TextController(AppDbContext db, TextComb service) : ControllerBase
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

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] Text text)
    {
        if (string.IsNullOrWhiteSpace(text.Content))
        {
            return BadRequest(new Error("Content must not be null or whitespace"));
        }

        await db.Texts.AddAsync(text);
        await db.SaveChangesAsync();

        return Ok(text);
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Text text)
    {
        if (string.IsNullOrWhiteSpace(text.Content))
        {
            return BadRequest(new Error("Content must not be null or whitespace"));
        }

        var existingText = await db.Texts.FindAsync(id);

        if (existingText == null)
        {
            return NotFound(new Error($"Text of id {id} not found"));
        }

        existingText.Content = text.Content;

        await db.SaveChangesAsync();

        return Ok(existingText);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var text = await db.Texts.FindAsync(id);

        if (text == null)
        {
            return NotFound(new Error($"Text of id {id} not found"));
        }

        db.Texts.Remove(text);
        await db.SaveChangesAsync();

        return Ok(text);
    }

    [HttpGet]
    [Route("combine")]
    public async Task<IActionResult> Combine([FromQuery] int a, [FromQuery] int b)
    {
        Text? aText = await db.Texts.FindAsync(a);
        Text? bText = await db.Texts.FindAsync(b);

        if (aText == null || bText == null)
        {
            return NotFound(new Text($"Text of id {a} or text of id {b} not found"));
        }


        var result = await service.Combine(aText.Content, bText.Content);
        return Ok(new Text(result));
    }
}
