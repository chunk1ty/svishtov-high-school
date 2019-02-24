namespace SvishtovHighSchool.ReadModel.Contracts
{
    public class CourseDto
    {
        public CourseDto(string aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
        }

        public string AggregateId { get; }

        public string Name { get; set; }
    }
}
