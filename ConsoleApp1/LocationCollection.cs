using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class LocationCollection
    {
        private List<Location> locations = new List<Location>();

        public bool LocationExists(Location location) => locations.Contains(location);
        public List<Location> GetLocations() => locations;

        public void AddLocation(Location location) => locations.Add(location);
        public void RemoveLocation(Location location) => locations.Remove(location);
    }
}
