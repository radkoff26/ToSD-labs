using Lab2.parcel;
using System.Text;

namespace Lab2.entities
{
    internal sealed class House : Parcelable, IComparable<House>, IEquatable<House>
    {
        public long Id { get; set; }
        public string City { get; }
        public int Rate { get; }
        public string Name { get; }
        public long OrganizationId { get; }

        public House(long id, string city, int rate, string name, long organizationId)
        {
            Id = id;
            City = city;
            Rate = rate;
            Name = name;
            OrganizationId = organizationId;
        }

        public House(ReadableParcel parcel) {
            Id = parcel.getLong();
            City = parcel.getString();
            Rate = parcel.getInt();
            Name = parcel.getString();
            OrganizationId = parcel.getLong();
        }

        public void writeToParcel(WritableParcel parcel)
        {
            parcel.putLong(Id);
            parcel.putString(City);
            parcel.putInt(Rate);
            parcel.putString(Name);
            parcel.putLong(OrganizationId);
        }

        public override string ToString()
        {
            return new StringBuilder("House {")
                .Append("Id = ").Append(Id).Append(", ")
                .Append("City = ").Append(City).Append(", ")
                .Append("Rate = ").Append(Rate).Append(", ")
                .Append("OrganizationId = ").Append(OrganizationId)
                .Append("}").ToString();
        }

        public int CompareTo(House? other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }
            return (int)(Id - other.Id);
        }

        public bool Equals(House? other)
        {
            if (other == null) { return false; }
            if (this == other) { return true; }
            if (
                Id != other.Id || Name != other.Name || City != other.City || Rate != other.Rate || OrganizationId != other.OrganizationId)
            {
                return false;
            }
            return true;
        }

        public static bool operator >(House? left, House? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Id > right.Id;
        }

        public static bool operator <(House? left, House? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Id < right.Id;
        }

        public static bool operator ==(House? left, House? right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(House? left, House? right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return true;
            }
            return !left.Equals(right);
        }
    }
}
