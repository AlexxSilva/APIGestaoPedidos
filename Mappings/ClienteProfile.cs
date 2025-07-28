using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Dto.DtoCliente;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIGestaoPedidos.Mappings
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<CriarClienteDto, Cliente>();
        }
    }
}
