using System.Threading.Tasks;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Application
{
    public interface ISender
    {
        Task SendMessagesAsync<T>(T domainMessage) where T : IDomainMessage;
    }
}
