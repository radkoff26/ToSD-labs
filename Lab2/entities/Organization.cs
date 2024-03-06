using Lab2.parcel;
using System.Text;

namespace Lab2.entities
{
    internal sealed class Organization : Parcelable, IComparable<Organization>, IEquatable<Organization>
    {
        public long Id { get; set; }
        public string Name { get; }
        public float Longitude { get; }
        public float Latitude { get; }

        public Organization(ReadableParcel parcel) {
            Id = parcel.getLong();
            Name = parcel.getString();
            Longitude = parcel.getFloat();
            Latitude = parcel.getFloat();
        }

        public Organization(long id, string name, float longitude, float latitude)
        {
            Id = id;
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
        }

        public void writeToParcel(WritableParcel parcel)
        {
            parcel.putLong(Id);
            parcel.putString(Name);
            parcel.putFloat(Longitude);
            parcel.putFloat(Latitude);
        }

        public override string ToString()
        {
            return new StringBuilder("Organization {")
                .Append("Id = ").Append(Id).Append(", ")
                .Append("Name = ").Append(Name).Append(", ")
                .Append("Longitude = ").Append(Longitude).Append(", ")
                .Append("Latitude = ").Append(Latitude).Append("}").ToString();
        }

        public int CompareTo(Organization? other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }
            return (int) (Id - other.Id);
        }

        public bool Equals(Organization? other)
        {
            if (other == null) { return false; }
            if (this == other) { return true; }
            if (
                Id != other.Id || Name != other.Name || Longitude != other.Longitude || Latitude != other.Latitude)
            {
                return false;
            }
            return true;
        }

        public static bool operator >(Organization? left, Organization? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Id > right.Id;
        }

        public static bool operator <(Organization? left, Organization? right)
        {
            if (left == null || right == null) {
                return false;
            }
            return left.Id < right.Id;
        }

        public static bool operator ==(Organization? left, Organization? right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) 
            {
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(Organization? left, Organization? right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return true;
            }
            return !left.Equals(right);
        }
    }
}
