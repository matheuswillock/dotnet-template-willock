using CleanArchTemplate.Application.Common.Outputs;

namespace CleanArchTemplate.Api.Extensions;

/// <summary>
/// Converte o padrao IOutput em respostas HTTP. Ajuste aqui a convencao de retorno da sua API.
/// </summary>
public static class OutputExtensions
{
    public static IResult ToOkResult<T>(this IOutput<T> output)
    {
        if (!output.IsValid)
            return Results.BadRequest(new { errors = output.GetErrorMessages() });

        return Results.Ok(new { data = output.Result, messages = output.GetSuccessMessages() });
    }

    public static IResult ToCreatedResult<T>(this IOutput<T> output, string location)
    {
        if (!output.IsValid)
            return Results.BadRequest(new { errors = output.GetErrorMessages() });

        return Results.Created(location, new { data = output.Result, messages = output.GetSuccessMessages() });
    }
}
