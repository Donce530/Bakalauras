using System;

namespace Models.Restaurants.Models.Data
{
    public class TableLink : IEquatable<TableLink>
    {
        public int FirstTableId { get; init; }
        public int SecondTableId { get; init; }
        
        public PlanTable FirstTable { get; }
        public PlanTable SecondTable { get; }

        public bool Equals(TableLink other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FirstTableId == other.FirstTableId && SecondTableId == other.SecondTableId ||
                          SecondTableId == other.FirstTableId && FirstTableId == other.SecondTableId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TableLink) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstTableId, SecondTableId, FirstTable, SecondTable);
        }
    }
}