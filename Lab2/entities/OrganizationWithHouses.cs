using System.Text;
using System.Xml.Linq;

namespace Lab2.entities
{
    internal struct OrganizationWithHouses(Organization organization, List<House> houses) : IComparable<OrganizationWithHouses>, IEquatable<OrganizationWithHouses>
    {
        public Organization Organization { get; } = organization;
        public List<House> Houses { get; } = houses;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Organization.ToString()).Append(":");
            Houses.ForEach(h =>
            {
                sb.Append("\n\t").Append(h.ToString());
            });
            return sb.ToString();
        }

        public int CompareTo(OrganizationWithHouses other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }
            return (int)(Organization.Id - other.Organization.Id);
        }

        public bool Equals(OrganizationWithHouses other)
        {
            if (other != null) { return false; }
            if (this == other) { return true; }
            if (
                Organization != other.Organization
                ||
                Houses != other.Houses
                )
            {
                return false;
            }
            return true;
        }

        public static bool operator >(OrganizationWithHouses? left, OrganizationWithHouses? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Value.Organization.Id > right.Value.Organization.Id;
        }

        public static bool operator <(OrganizationWithHouses? left, OrganizationWithHouses? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Value.Organization.Id < right.Value.Organization.Id;
        }

        public static bool operator ==(OrganizationWithHouses? left, OrganizationWithHouses? right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(OrganizationWithHouses? left, OrganizationWithHouses? right)
        {
            if (left == null || right == null)
            {
                return true;
            }
            return !left.Equals(right);
        }
    }
}
