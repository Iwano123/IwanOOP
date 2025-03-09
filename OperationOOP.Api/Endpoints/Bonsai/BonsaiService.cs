using OperationOOP.Core.Data;
using OperationOOP.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace OperationOOP.Core.Services
{   //BonsaiService hanterar operationer på bonsaiträd
    public class BonsaiService
    {  //lagra en referens till databasen
        private readonly IDatabase _database;
        //konstruktor som tar emot IDatabase-objekt via dependency injection
        public BonsaiService(IDatabase database)
        {
            //sparar referensen till databasen
            _database = database;
        }

        // Algoritm som filtrerar bonsaiträd efter skötselnivå
        public IEnumerable<Bonsai> FilterBonsaisByCareLevel(CareLevel careLevel)
        {
            // Använder LINQ för att filtrera bonsaiträd där CareLevel matchar det angivna värd
            return _database.Bonsais.Where(b => b.CareLevel == careLevel);
        }

        // Algoritm som sorterar bonsaiträd efter ålder i stigande ordning
        public IEnumerable<Bonsai> SortBonsaisByAge()
        {
            //Använder LINQ för att sortera bonsaiträd efter AgeYears i stigande ordning
            return _database.Bonsais.OrderBy(b => b.AgeYears);
        }

        // Sökalgoritm bonsaiträd efter art
        public IEnumerable<Bonsai> SearchBonsaisBySpecies(string species)
        {
            //Använder LINQ för att söka efter bonsaiträd där Species innehåller den angivna strängen, oavsett skiftläge
            return _database.Bonsais.Where(b => b.Species.Contains(species, StringComparison.OrdinalIgnoreCase));
        }

        // Algoritm som hittar bonsaiträd när dem behöver vattnas (ex. senast vattnad för mer än 7 dagar sedan)
        public IEnumerable<Bonsai> FindBonsaisNeedingWatering()
        {
            //Använder LINQ för att hitta bonsaiträd där LastWatered är mer än 7 dagar sedan
            return _database.Bonsais.Where(b => (DateTime.Now - b.LastWatered).TotalDays > 7);
        }
    }
}