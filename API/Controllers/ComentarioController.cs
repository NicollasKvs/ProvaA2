using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using api_avaliacao.Models;
using api_avaliacao.Data;

[ApiController]
[Route("comentario")]
[Authorize]
public class ComentarioController : ControllerBase
{
    private readonly AppDataContext _context;
    private readonly IHttpContextAccessor _accessor;

    public ComentarioController(AppDataContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
    }

    private int UsuarioLogadoId()
    {
        return int.Parse(_accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }

    [HttpGet("item/{itemId}")]
    public IActionResult Listar(int itemId)
    {
        var comentarios = _context.Comentarios
            .Where(c => c.ItemId == itemId)
            .Include(c => c.Usuario)
            .OrderByDescending(c => c.Data)
            .ToList();
        return Ok(comentarios);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Comentario comentario)
    {
        comentario.UsuarioId = UsuarioLogadoId();
        comentario.Data = DateTime.Now;
        _context.Comentarios.Add(comentario);
        _context.SaveChanges();
        return Ok(comentario);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var comentario = _context.Comentarios.Find(id);
        if (comentario == null || comentario.UsuarioId != UsuarioLogadoId())
            return Unauthorized();

        _context.Comentarios.Remove(comentario);
        _context.SaveChanges();
        return Ok();
    }
}
