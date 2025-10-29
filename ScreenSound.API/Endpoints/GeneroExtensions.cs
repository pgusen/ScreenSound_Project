using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class GeneroExtensions
    {
        public static void AddEndPointsGeneros(this WebApplication app)
        {
            app.MapGet("/Generos", ([FromServices]DAL<Genero> dal) => {
                var generoList = dal.Listar();
                if(generoList is null)
                {
                    return Results.NotFound();
                }
                var responseGeneroList = EntityListToResponseList(generoList);
                return Results.Ok(responseGeneroList);
            });

            app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
            {
                var genero = dal.RecuperarPor(g => g.Nome!.ToUpper().Equals(nome.ToUpper()));
                if (genero is not null)
                {
                    var response = EntityToResponse(genero!);
                    return Results.Ok(response);
                }
                return Results.NotFound("Gênero não encontrado.");
            });

            app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
            {
                dal.Adicionar(RequestToEntity(generoRequest));
                return Results.Ok();
            });

            app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
            {
                var genero = dal.RecuperarPor(g => g.Id == id);
                if(genero is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(genero);
                return Results.Ok();
            });
        }

        private static Genero RequestToEntity(GeneroRequest generoRequest)
        {
            return new Genero
            {
                Nome = generoRequest.Nome,
                Descricao = generoRequest.Descricao,
            };
        }

        private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> generoList)
        {
            return generoList.Select(a => EntityToResponse(a)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!);
        }
    }
}
