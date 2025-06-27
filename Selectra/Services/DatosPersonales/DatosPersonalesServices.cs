using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.DatosPersonales
{
    public class DatosPersonalesServices : IDatosPersonalesService
    {
        private readonly SelectraContext _context;
        public DatosPersonalesServices(SelectraContext context)
        {
            _context = context;
        }
        public async Task<List<ListaTiposDocumentoDto>> GetListaTiposDocumentoAsync()
        {
            return await _context.TiposDocumentos
                .Select(td => new ListaTiposDocumentoDto
                {
                    tipoDocumentoId = td.tipoDocumentoId,
                    nombreTipoDocumento = td.nombreTipoDocumento
                })
                .ToListAsync();
        }

        public async Task<List<ListaUbigeoDto>> GetListaDepartamentosAsync()
        {
            return await _context.Ubigeos
                .Select(u => new ListaUbigeoDto
                {
                    ubigeoId = u.departamentoId,
                    nombre = u.departamento
                })
                .Distinct()
                .OrderBy(u => u.nombre)
                .ToListAsync();
        }
        public async Task<List<ListaUbigeoDto>> GetListaDistritosAsync(string departamentoId)
        {
            return await _context.Ubigeos
             .Where(u => u.distritoId == departamentoId && !string.IsNullOrEmpty(u.provincia))
             .Select(u => new ListaUbigeoDto
             {
                 ubigeoId = u.ubigeoId,
                 nombre = u.provincia
             })
             .Distinct()
             .OrderBy(u => u.nombre)
             .ToListAsync();
        }
        public async Task<List<ListaUbigeoDto>> GetListaProvinciasAsync(string distritoId)
        {
            return await _context.Ubigeos
               .Where(u => u.departamentoId == distritoId && !string.IsNullOrEmpty(u.distrito))
               .Select(u => new ListaUbigeoDto
               {
                   ubigeoId = u.distritoId,
                   nombre = u.distrito
               })
               .Distinct()
               .OrderBy(u => u.nombre)
               .ToListAsync();


        }


    }
}
