using System;
using System.Collections.Generic;
using Transfer.Application.Interfaces;
using Transfer.Data.Repository;
using Transfer.Domain.Models;

namespace Transfer.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _repository;

        public TransferService(ITransferRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _repository.GetTransferLogs();
        }
    }
}