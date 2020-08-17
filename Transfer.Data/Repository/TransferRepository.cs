using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transfer.Data.Context;
using Transfer.Domain.Models;

namespace Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDbContext _context;

        public TransferRepository(TransferDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _context.TransferLogs;
        }

        public async Task AddTransferLog(TransferLog transferLog)
        {
            await _context.TransferLogs.AddAsync(transferLog);
            await _context.SaveChangesAsync();
        }
    }
}