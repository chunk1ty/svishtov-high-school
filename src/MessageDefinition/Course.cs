using System;
using System.IO;
using ProtoBuf;

namespace MessageDefinition
{
    [ProtoContract]
    public class Course
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public DateTime CreatedOn { get; set; }

        [ProtoMember(3)]
        public int SomeInt { get; set; }
    }
}
