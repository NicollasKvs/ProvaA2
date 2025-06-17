using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_avaliacao.Data;
using api_avaliacao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("item")]
[Authorize]
public class ItemController : ControllerBase
{
    private readonly AppDataContext _context;
    public ItemController(AppDataContext context) => _context = context;

    [HttpGet]
    public IActionResult Get() =>
        Ok(_context.Itens.Include(i => i.Categoria).ToList());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) =>
        Ok(_context.Itens.Include(i => i.Categoria).FirstOrDefault(i => i.ItemId == id));
}
