using Services.Models.Base;
using Services.Models.Request;
using Services.Models.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPersonaService
    {
        Task<ResponseDTO<PersonaResponse>> SelPersonas(CancellationToken ct);

        Task<ResponseDTO<PersonaResponse>> SelPersonaPorId(int id, CancellationToken ct);

        Task<BaseResponse> ActualizarPersona(PersonaUpdateRequest request, CancellationToken ct);

        Task<BaseResponse> AgregarPersona(PersonaNewRequest request, CancellationToken ct);

        Task<BaseResponse> EliminarPersona(int id, CancellationToken ct);
    }
}
