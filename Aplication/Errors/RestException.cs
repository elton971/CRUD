using System.Net;

namespace Aplication.Errors;
//vai funcionar como uma classe modelo para os nossos erros
public class RestException:Exception
{

    public HttpStatusCode Code { get; set; }
    public object Errors { get; set; }

    public RestException(HttpStatusCode code,object errors=null)
    {
        Code=code;
        Errors = errors;
    }
}