using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Enums;
using SwipeCSAT.Api.Extensions;
using SwipeCSAT.Api.Mapping;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Endpoints;

public static class CriterionsEndpoints
{
    public static RouteGroupBuilder MapCriterionsEdnpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/criterions");

        //Получение критериев
        group.MapGet("/", async (CriterionsRepository criterionsRepository) =>
        {
            var criterions = await criterionsRepository.GetAllCriterions();
            return Results.Ok(criterions.Select(x => x.ToFullDto()).ToList());
        }).RequirePermissions(Permission.Create);

        //Добавление критерия
        group.MapPost("/", async (CriterionsRepository criterionsRepository, CriterionShortDto createCriterionDto) =>
        {
            var NewCriterion = await criterionsRepository.AddCriterion(createCriterionDto.Name);
            return Results.CreatedAtRoute("GetCriterionByName", new { NewCriterion.Name }, NewCriterion);
        }).RequirePermissions(Permission.Create);

        group.MapGet("/{Name}", async (string Name, CriterionsRepository criterionsRepository) =>
        {
            var criterion = await criterionsRepository.GetCriterionByName(Name);
            return Results.Ok(criterion);
        }).WithName("GetCriterionByName").RequirePermissions(Permission.Read);


        group.MapDelete("/{Name}", async (string Name, CriterionsRepository criterionsRepository) =>
        {
            await criterionsRepository.DeleteCriterion(Name);
            return Results.NoContent();
        }).RequirePermissions(Permission.Delete);
        return group;
    }
}