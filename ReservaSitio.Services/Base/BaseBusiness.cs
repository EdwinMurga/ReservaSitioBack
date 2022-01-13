using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Base
{
    public abstract class BaseBusiness<T>
    {
        protected async Task<StatusResponse> MessageResponse(Func<Task> callback, string message)
        {
            var response = new StatusResponse();

            try
            {
                await callback();

                response.Message = message;
                response.Success = true;
            }
            catch (CustomException cuix)
            {
                response.SetErrorList(cuix.Errors);
                response.Message = cuix.Title;
            }
            catch (Exception ex)
            {
                response.SetException(ex);
            }

            return response;
        }

        protected async Task<StatusResponse<X>> ComplexResponse<X>(Func<Task<X>> callbackData, string message = "")
        {
            var response = new StatusResponse<X>();

            try
            {
                response.Data = await callbackData();

                response.Message = message;
                response.Success = true;
            }
            catch (CustomException cuix)
            {
                response.SetErrorList(cuix.Errors);
                response.Message = cuix.Title;
            }
            catch (Exception ex)
            {
                response.SetException(ex);
            }

            return response;
        }

        public class CustomException : Exception
        {
            public String Title { get; set; }
            public List<string> Errors { get; set; }
        }
    }
}
