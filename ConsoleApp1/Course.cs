using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Course
    {
        private string title;
        private string dept;
        private string courseCode;
        private string courseType;
        private List<Section> sections = new List<Section>();

        public Course(string title, string dept, string courseCode, string type)
        {
            this.title = title;
            this.dept = dept;
            this.courseCode = courseCode;
            this.courseType = type;
        }

        public string GetTitle() => title;
        public string GetDept() => dept;
        public string GetCourseCode() => courseCode;
        public string getCourseType() => courseType;
        public List<Section> GetSections() => sections;

        public void AddSection(Section section) => sections.Add(section);
        public void RemoveSection(Section section) => sections.Remove(section);
    }
}
