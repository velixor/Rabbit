using System.Collections.Generic;
using Transfer.Domain.Models;

namespace Transfer.Application.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}