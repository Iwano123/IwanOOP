using OperationOOP.Core.Services;

namespace OperationOOP.Api.Endpoints;

// FindBonsaisNeedingWatering-klassen implementerar ett API-endpoint för att hitta bonsai-träd som behöver vattnas
public class FindBonsaisNeedingWatering : IEndpoint
{
    // MapEndpoint-metoden konfigurerar endpointen och länkar den till en specifik URL
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/bonsais/needs-watering", Handle) // GET-endpoint för att hitta bonsaiträd som behöver vattnas
        .WithSummary("Find bonsais that need watering"); // Sammanfattning av endpointen för API-dokumentation

    // Response-record som representerar utdata från endpointen
    public record Response(
        int Id,
        string Name,
        DateTime LastWatered
    );

    // Handle-metoden hanterar inkommande förfrågningar och returnerar bonsaiträd som behöver vattnas
    private static IEnumerable<Response> Handle(BonsaiService service)
    {
        // Använder BonsaiService för att hitta bonsaiträd som inte har vattnats på mer än 7 dagar
        return service.FindBonsaisNeedingWatering()
            .Select(b => new Response(
                Id: b.Id,
                Name: b.Name,
                LastWatered: b.LastWatered
            ));
    }
}