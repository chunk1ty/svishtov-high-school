using Google.Protobuf;

namespace SvishtovHighSchool.Integration.Converters
{
    public interface IMessageConverter
    {
        IMessage ToPayload(object @event);
    }
}