using System.Collections.Generic;
using Transfer.Domain.Models;

namespace Transfer.Data.Repository
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}