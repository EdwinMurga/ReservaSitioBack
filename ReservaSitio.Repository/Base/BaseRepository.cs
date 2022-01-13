using ReservaSitio.DataAccess.CustomConnection;
using System;
using System.Transactions;

namespace ReservaSitio.Repository.Base
{
    public abstract class BaseRepository
    {
        protected readonly ICustomConnection mConnection;

        protected const StringComparison IgnoreCase = StringComparison.OrdinalIgnoreCase;
        protected const bool ACTIVO = true;

        public BaseRepository(ICustomConnection connection)
        {
            mConnection = connection;
        }

        protected object IsNull(object value)
        {
            if (value == null)
                return DBNull.Value;
            else if (value.GetType() == typeof(String) && string.IsNullOrEmpty(value.ToString()))
                return DBNull.Value;
            else
                return value;
        }

        //protected void EmptyInputCrash(object input, string message = "No se ha enviado la información a procesar")
        //{
        //    if (input == null)
        //        Crash(message);
        //}

        //protected void ErrorsExistCrash(List<string> errores, string message = "Los siguientes campos son requeridos")
        //{
        //    if (errores.Count > 0)
        //        Crash(message, errores);
        //}

        //protected void Crash(string message, List<string> errors = null)
        //{
        //    throw new CustomException { Title = message, Errors = errors };
        //}

        //protected void CrashFormat(string message, List<string> errors, params object[] values)
        //{
        //    throw new CustomException { Title = string.Format(message, values), Errors = errors };
        //}

        protected TransactionScope GetTransaction() => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    }
}
