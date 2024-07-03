using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Produto;
using api.Models;

namespace api.Mappers
{
    public static class ProdutoMappers
    {
        public static ProdutoDto ToProdutoDto(this Produto produtoModel)
        {
            return new ProdutoDto
            {
                Id = produtoModel.Id,
                Codigo = produtoModel.Codigo,
                Nome = produtoModel.Nome,
                Descricao = produtoModel.Descricao,
                Preco = produtoModel.Preco,
                Ativo = produtoModel.Ativo
            };
        }

        public static Produto ToProdutoFromCreateDTO(this CreateProdutoResquestDto ProdutoDto)
        {
            return new Produto
            {
                Codigo = ProdutoDto.Codigo,
                Nome = ProdutoDto.Nome,
                Descricao = ProdutoDto.Descricao,
                Preco = ProdutoDto.Preco,
                Ativo = ProdutoDto.Ativo
            };
        }
    }
}