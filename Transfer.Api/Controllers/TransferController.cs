using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Transfer.Application.Interfaces;
using Transfer.Domain.Models;

namespace Transfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService ?? throw new ArgumentNullException(nameof(transferService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TransferLog>> GetTransferLogs()
        {
            return Ok(_transferService.GetTransferLogs());
        }
    }
}