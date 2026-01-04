using System.Text.Json;

namespace CompanyEmployees.ErrorModel
{
    public class ErrorDetails  
    {
        public int StatusCode { get; set; }  //e.g. 404,401(unauth),400(bad request),500(internal server err)
        public string? Message { get; set; }  // x mex errore
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        //converts obj type ErrorDetails (this class) in str json.
        /*
         var error = new ErrorDetails { StatusCode = 404, Message = "Company not found"};
         diventa
         {"StatusCode":404,"Message":"Company not found"}
         */
    }
    //modello di errore standartizzato, per rappresentare un errore http in formato json!, cosi da restituire al client una risposta di errore chiara,coerente,machine-friendly.
}
