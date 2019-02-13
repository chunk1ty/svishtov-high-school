using System.Threading.Tasks;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Application
{
    public interface ISender
    {
        Task SendMessagesAsync(IDomainMessage domainMessage);
    }
}
