using System;
using System.Collections.Generic;

namespace ReservaSitio.Services.Base
{
    public class StatusResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public string Exception { get; set; }

        public StatusResponse ReturnMessage(string message)
        {
            this.Success = false;
            this.Message = message;

            return this;
        }

        public void SetException(Exception ex)
        {
            this.Success = false;
            this.Message = string.Format("[{0}] {1}", ex.HResult, ex.Message);
            this.Exception = ex.Message + " | " + ex.StackTrace;
        }

        public void SetErrorList(List<string> errors)
        {
            this.Success = false;
            this.Message = null;
            this.Exception = null;
            this.Errors = errors;
        }
    }

    public class StatusResponse<T> : StatusResponse
    {
        public new bool Success { get; set; }
        public T Data { get; set; }

        public new StatusResponse<T> ReturnMessage(string message)
        {
            this.Success = false;
            this.Message = message;

            this.Data = default(T);

            return this;
        }

        public new void SetException(Exception ex)
        {
            base.SetException(ex);

            this.Success = base.Success;
            this.Data = default(T);
        }

        public new void SetErrorList(List<string> errors)
        {
            base.SetErrorList(errors);

            this.Success = base.Success;
            this.Data = default(T);
        }
    }
}
