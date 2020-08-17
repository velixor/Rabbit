using System.Threading.Tasks;
using Rabbit.Mvc.Models.Dto;

namespace Rabbit.Mvc.Services
{
    public interface ITransferService
    {
        Task Transfer(TransferDto transferDto);
    }
}