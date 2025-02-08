using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SectionsCollection
    {
        private List<Section> sections = new List<Section>();

        public bool SectionExists(Section section) => sections.Contains(section);
        public List<Section> GetSections() => sections;

        public void AddSection(Section section) => sections.Add(section);
        public void RemoveSection(Section section) => sections.Remove(section);
        public List<Section> SearchSection(Location location)
        {
            return sections
                .Where(s => s.GetMeetingInfo().GetLocation().Equals(location))
                .ToList();
        }
    }
}
