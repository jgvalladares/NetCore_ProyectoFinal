using Curso.ComercioElectronico.Aplicacion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IDeliveryMethodAppService
    {
        Task<ICollection<DeliveryMethodDto>> GetAllAsync();
        Task<DeliveryMethodDto> GetAsync(string codigo);
        Task CreateAsync(CreateDeliveryMethodDto deliveryMethodDto);
        Task UpdateAsync(string id, CreateDeliveryMethodDto putDeliveryMethodDto);
        Task<bool> DeleteAsync(string id);
    }
}
