using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Produto;
using api.Mappers;
using api.Models;

//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProdutoController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var produtos = await _context.Produto.ToListAsync();
            var produtoDto = produtos.Select(s => s.ToProdutoDto());

            return Ok(produtos);
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var produto = await _context.Produto.FindAsync(id);

            if(produto == null)
            {
                return NotFound();
            }

            return Ok(produto.ToProdutoDto());
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProdutoResquestDto produtoDto)
        {
         
            var produtoModel = produtoDto.ToProdutoFromCreateDTO();
            await _context.Produto.AddAsync(produtoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = produtoModel.Id }, produtoModel.ToProdutoDto());
        }

        // PUT: api/produtos/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProdutoRequestDto updateDto)
        {
            var produtoModel = await _context.Produto.FirstOrDefaultAsync(x => x.Id == id);

            if (produtoModel == null || !produtoModel.Ativo)
            {
                return NotFound();
            }
            produtoModel.Codigo = updateDto.Codigo;
            produtoModel.Nome = updateDto.Nome;
            produtoModel.Descricao = updateDto.Descricao;
            produtoModel.Preco = updateDto.Preco;
            produtoModel.Ativo = updateDto.Ativo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        return NoContent();

        //return Ok(produtoModel.ToProdutoDto());
        }

        // DELETE: api/produtos/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var produtoModel = await _context.Produto.FirstOrDefaultAsync(x => x.Id == id);

            if (produtoModel == null || !produtoModel.Ativo)
            {
                return NotFound();
            }

            produtoModel.Ativo = false;

            await _context.SaveChangesAsync();


            return NoContent();
        }

        // Search: api/search/query
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Produto>>> SearchProdutos(string query)
        {
            return await _context.Produto
                .Where(p => p.Ativo && (p.Codigo.Contains(query) || p.Nome.Contains(query)))
                .ToListAsync();
        }


        // Método auxiliar para verificar se o produto existe
        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }
    }
}