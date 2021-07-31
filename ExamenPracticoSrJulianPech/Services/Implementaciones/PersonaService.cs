using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.Base;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Implementaciones
{
    public class PersonaService : IPersonaService
    {
        private readonly ExamenDBContext dbContext;
        private readonly IMapper mapper;

        public PersonaService(ExamenDBContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public async Task<ResponseDTO<PersonaResponse>> SelPersonaPorId(int id, CancellationToken ct)
        {
            var Response = new ResponseDTO<PersonaResponse>();

            try
            {
                var persona = await dbContext.Persona.FirstOrDefaultAsync(x => x.Id == id);
                Response.Data = mapper.Map<Persona, PersonaResponse>(persona);

                if (Response.Data != null)
                {
                    Response.Type = "Success";
                    Response.Success = true;
                }
                else
                {
                    Response.Type = "Error";
                    Response.Success = true;
                    Response.Message = "No se encontró ningún registro con el Id solicitado";
                }

            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;
            }

            return Response;
        }

        public async Task<ResponseDTO<PersonaResponse>> SelPersonas(CancellationToken ct)
        {
            var Response = new ResponseDTO<PersonaResponse>();

            try
            {
                var personas = await dbContext.Persona.ToListAsync(ct);
                Response.ListData = mapper.Map<List<Persona>, List<PersonaResponse>>(personas);
                
                if(Response.ListData.Any())
                {
                    Response.Type = "Success";
                    Response.Success = true;
                }
                else
                {
                    Response.Type = "Error";
                    Response.Success = true;
                    Response.Message = "No se encontraron datos";
                }
            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;
            }

            return Response;
        }

        public async Task<BaseResponse> ActualizarPersona(PersonaUpdateRequest request, CancellationToken ct)
        {
            var Response = new ResponseDTO<PersonaResponse>();

            try
            {
                var persona = await dbContext.Persona.FindAsync(request.Id);
                if (persona == null)
                {
                    Response.Success = true;
                    Response.Type = "Error";
                    Response.Message = "No se encontró el registro a actualizar con el Id " + request.Id;

                }
                else
                {
                    var existeEmail = await dbContext.Persona.Where(x => x.Correo == request.Correo && x.Id != request.Id).AnyAsync();
                    if (existeEmail)
                    {
                        Response.Success = true;
                        Response.Type = "Error";
                        Response.Message = "El correo electrónico ya se encuentra registrado.";
                    }
                    else
                    {
                        persona.Nombre = request.Nombre ?? request.Nombre;
                        persona.Apellido = request.Apellido ?? request.Apellido;
                        persona.Correo = request.Correo ?? request.Correo;
                        persona.Edad = request.Edad;

                        var resp = await dbContext.SaveChangesAsync();

                        if (resp > 0)
                        {
                            Response.Success = true;
                            Response.Type = "Success";
                            Response.Message = "Registro actualizado exitosamente";
                        }
                        else
                        {
                            Response.Success = true;
                            Response.Type = "Error";
                            Response.Message = "Error al actualizar registro";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;
            }

            return Response;
        }

        public async Task<BaseResponse> AgregarPersona(PersonaNewRequest request, CancellationToken ct)
        {
            var Response = new ResponseDTO<PersonaResponse>();

            try
            {
                var existeEmail = await dbContext.Persona.Where(x => x.Correo == request.Correo).AnyAsync();
                if (existeEmail)
                {
                    Response.Success = true;
                    Response.Type = "Error";
                    Response.Message = "El correo electrónico ya se encuentra registrado.";
                }
                else
                {
                    var persona = new Persona
                    {
                        Nombre = request.Nombre,
                        Apellido = request.Apellido,
                        Correo = request.Correo,
                        Edad = request.Edad
                    };

                    dbContext.Persona.Add(persona);
                    var resp = await dbContext.SaveChangesAsync();
                    if (resp > 0)
                    {
                        Response.Success = true;
                        Response.Type = "Success";
                        Response.Message = "Registro agregado exitosamente";
                    }
                    else
                    {
                        Response.Success = true;
                        Response.Type = "Error";
                        Response.Message = "Error al crear registro";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;
            }

            return Response;
        }

        public async Task<BaseResponse> EliminarPersona(int id, CancellationToken ct)
        {
            var Response = new ResponseDTO<PersonaResponse>();

            try
            {
                var persona = await dbContext.Persona.FindAsync(id);
                if (persona == null)
                {
                    Response.Success = true;
                    Response.Type = "Error";
                    Response.Message = "No se encontró el registro a eliminar con el Id " + id;
                }
                else
                {
                    dbContext.Remove(persona);
                    var valor = await dbContext.SaveChangesAsync();
                    if (valor > 0)
                    {
                        Response.Success = true;
                        Response.Type = "Success";
                        Response.Message = "Registro eliminado exitosamente";
                    }
                    else
                    {
                        Response.Success = true;
                        Response.Type = "Error";
                        Response.Message = "Error al eliminar el registro con el Id: " + id;
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;
            }

            return Response;
        }
    }
}
