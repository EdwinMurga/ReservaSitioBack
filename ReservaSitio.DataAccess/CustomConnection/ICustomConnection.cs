using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ReservaSitio.DataAccess.CustomConnection
{
    public interface ICustomConnection
    {
        Task<IDbConnection> BeginConnection(bool useTransaction = false);

        Task Complete();

        Task Rollback();

        Task CloseConnection();

        IDbTransaction GetTransaction();

        Task ExecuteCommandAsync(string commandText, params DbParameter[] values);

        Task ExecuteCommandAsync(string commandText, string name, object value);


        Task<IDataReader> ExecuteReaderAsync(string commandText, params DbParameter[] values);

        Task<IDataReader> ExecuteReaderAsync(string commandText, string name, object value);
    }
}
