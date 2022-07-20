using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //CreateMap(Origen, destino)

            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
            CreateMap<CreateBrandDto, Brand>();

            CreateMap<TypeProduct,TypeProductDto>();
            CreateMap<TypeProductDto, TypeProduct>();
            CreateMap<CreateTypeProductDto, TypeProduct>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<CreateOrderItemDto, OrderItem>();

            CreateMap<UpdateOrderItemDto, OrderItem>();

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();


        }
    }
}
