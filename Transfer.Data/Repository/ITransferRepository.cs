using System.Collections.Generic;
using System.Threading.Tasks;
using Transfer.Domain.Models;

namespace Transfer.Data.Repository
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
        Task AddTransferLog(TransferLog transferLog);
    }
}