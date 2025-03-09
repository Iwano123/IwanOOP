using OperationOOP.Core.Services;

namespace OperationOOP.Api.Endpoints;
//FilterByCareLevel klassen är en IEndpoint som filtrerar bonsaiträd efter skötselnivå
public class FilterByCareLevel : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app //Mappar endpointen och länkar till en specifik 
        .MapGet("/bonsais/care-level/{careLevel}", Handle) // GET-endpoint för att filtrera bonsai-träd efter skötselnivå
        .WithSummary("Filter bonsais by care level"); //Beskrivning av endpointen

    //Request som representerar en förfrågan om att filtrera bonsaiträd efter skötselnivå till endpointen
    public record Request(CareLevel CareLevel);
    //Response som representerar utdata från endpointen
    public record Response(
        int Id,
        string Name,
        string Species,
        int AgeYears,
        DateTime LastWatered,
        DateTime LastPruned,
        BonsaiStyle Style,
        CareLevel CareLevel
    );
    //Metod som hanterar inkommande förfrågan om att filtrera bonsaiträd efter skötselnivå och returnerar bonsaiträd som matchar skötselnivån
    private static IEnumerable<Response> Handle([AsParameters] Request request, BonsaiService service)
    {   //använder mig av FilterBonsaisByCareLevel-metoden i BonsaiService för att filtrera bonsaiträd efter skötselnivå
        return service.FilterBonsaisByCareLevel(request.CareLevel)
            .Select(b => new Response(
                Id: b.Id,
                Name: b.Name,
                Species: b.Species,
                AgeYears: b.AgeYears,
                LastWatered: b.LastWatered,
                LastPruned: b.LastPruned,
                Style: b.Style,
                CareLevel: b.CareLevel
            ));
    }
}