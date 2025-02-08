using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Section : IEquatable<Section>
    {
        private int sectionNumber;
        private int CRN;
        private Course course;
        private int term;
        private MeetingInfo meetingInfo;

        public Section(int sectionNumber, int CRN, Course course, int term, MeetingInfo meetingInfo)
        {
            this.sectionNumber = sectionNumber;
            this.CRN = CRN;
            this.course = course;
            this.term = term;
            this.meetingInfo = meetingInfo;
        }

        public int GetSectionNumber() => sectionNumber;
        public int GetCRN() => CRN;
        public Course GetCourse() => course;
        public int GetTerm() => term;
        public MeetingInfo GetMeetingInfo() => meetingInfo;

        // Implement IEquatable<Section>
        public bool Equals(Section other)
        {
            if (other == null) return false;

            // Assuming CRN is unique per section
            return this.CRN == other.CRN;
        }

        // Override Equals for object type
        public override bool Equals(object obj)
        {
            return Equals(obj as Section);
        }

        // Override GetHashCode for correct behavior in collections
        public override int GetHashCode()
        {
            return CRN.GetHashCode();
        }
    }
}
