using System.Net;

namespace Sales.WEB.Repositories
{
        public class HttpResponseWrapper<T>
        {
            public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
            {
                Error = error;
                Response = response;
                HttpResponseMessage = httpResponseMessage;
            }

            public bool Error { get; set; }

            public T? Response { get; set; }

            public HttpResponseMessage HttpResponseMessage { get; set; }



            public async Task<string?> GetErrorMessageAsync()
            {
                if (!Error)
                {
                    return null;
                }

                var codigoEstatus = HttpResponseMessage.StatusCode;
                if (codigoEstatus == HttpStatusCode.NotFound)
                {
                    return "No se encontró en recurso";
                }
                else if (codigoEstatus == HttpStatusCode.BadRequest)
                {
                    return await HttpResponseMessage.Content.ReadAsStringAsync();
                }
                else if (codigoEstatus == HttpStatusCode.Unauthorized)
                {
                    return "Debes que logearte para realizar esta acción";
                }
                else if (codigoEstatus == HttpStatusCode.Forbidden)
                {
                    return "No tienes permisos para hacer esto";
                }

                return "Ocurrió un error no específicado ";
            }
        }
    }



