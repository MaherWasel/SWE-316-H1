using System;

namespace ConsoleApp1
{
    public class Location : IEquatable<Location>
    {
        private string building;
        private string room;

        public Location(string building, string room)
        {
            this.building = building ?? "";
            this.room = room ?? "";
        }

        public string GetBuilding() => building;
        public string GetRoom() => room;

        // Implement IEquatable<Location>
        public bool Equals(Location other)
        {
            if (other == null) return false;

            return string.Equals(building, other.building, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(room, other.room, StringComparison.OrdinalIgnoreCase);
        }

        // Override Equals for object type
        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        // Override GetHashCode to ensure correct behavior in collections
        public override int GetHashCode()
        {
            return HashCode.Combine(building.ToLower(), room.ToLower());
        }
    }
}
