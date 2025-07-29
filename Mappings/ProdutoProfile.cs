using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Dto.DtoProduto;
using AutoMapper;

namespace APIGestaoPedidos.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<CriarProdutoDto, Produto>();
        }
    }
}
