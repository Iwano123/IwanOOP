namespace OperationOOP.Core.Models
{   //bonsai för utomhusbruk ärver från Bonsai
    public class OutdoorBonsai : Bonsai
    {  //klimategenskaper för utomhusbonsai
        public string ClimateRequirement { get; set; }
    }
}