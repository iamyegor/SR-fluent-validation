using System.Linq;
using DomainModel;

namespace Api.Repositories
{
    public sealed class CourseRepository
    {
        private static readonly Course[] AllCourses =
        {
            new Course(1, "Calculus", 5),
            new Course(2, "History", 4),
            new Course(3, "Literature", 4)
        };

        public Course GetByName(string name)
        {
            return AllCourses.SingleOrDefault(x => x.Name == name);
        }
    }
}
