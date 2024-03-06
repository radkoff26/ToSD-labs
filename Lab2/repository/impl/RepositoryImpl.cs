using Lab2.data_source;
using Lab2.entities;

namespace Lab2.repository.impl
{
    internal class RepositoryImpl : Repository
    {
        private static string DIRECTORY = "./data";

        private HouseDataSource houseDataSource = new HouseDataSource(DIRECTORY);
        private OrganizationDataSource organizationDataSource = new OrganizationDataSource(DIRECTORY);

        public void AddHouse(House house)
        {
            houseDataSource.AddHouse(house);
        }

        public void AddOrganization(Organization organization)
        {
            organizationDataSource.AddOrganization(organization);
        }

        public long CountHouses()
        {
            return houseDataSource.GetHouses().Count();
        }

        public long CountOrganizations()
        {
            return organizationDataSource.GetOrganizations().Count();
        }

        public bool DeleteHouseById(long id)
        {
            var house = findHouseById(id);
            if (house != null)
            {
                houseDataSource.RemoveHouse(house);
                return true;
            }
            return false;
        }

        public bool DeleteOrganizationById(long id)
        {
            var organization = findOrganizationById(id);
            if (organization != null)
            {
                organizationDataSource.RemoveOrganization(organization);
                var index = houseDataSource.getOrganizationIdIndex();
                if (index.ContainsKey(id))
                {
                    var houseIndices = index[id];
                    var indices = new List<long>();
                    indices.AddRange(houseIndices);
                    indices.ForEach(id =>
                    {
                        // Cascade removal
                        DeleteHouseById(id);
                    });
                }
                return true;
            }
            return false;
        }

        public float GetAverageHousesRating()
        {
            var houses = houseDataSource.GetHouses();
            var sum = 0;
            houses.ForEach(h =>
            {
                sum += h.Rate;
            });
            return ((float)sum) / houses.Count;
        }

        public House? GetHouseById(long id)
        {
            return findHouseById(id);
        }

        public List<House> GetHouses()
        {
            return houseDataSource.GetHouses();
        }

        public List<House> GetHousesByCity(string city)
        {
            var houses = new List<House>();
            var cityIndex = houseDataSource.getCityIndex();
            if (!cityIndex.ContainsKey(city))
            {
                return houses;
            }
            var indices = cityIndex[city];
            foreach (var id in indices)
            {
                houses.Add(findHouseById(id)!);
            }
            return houses;
        }

        public List<OrganizationWithHouses> GetOrganizationsFilteredByLocationRange(float fromLatitude, float toLatitude, float fromLongitude, float toLongitude)
        {
            var organizations = GetOrganizations();
            return (
                from o in organizations
                    where o.Organization.Latitude >= fromLatitude && o.Organization.Latitude <= toLatitude
                    && o.Organization.Longitude >= fromLongitude && o.Organization.Longitude <= toLongitude
                    select o
                ).ToList();
        }

        public List<House> GetHousesSortedByName(bool descending)
        {

            var houses = houseDataSource.GetHouses();
            if (descending)
            {
                return (
                    from h in houses
                    orderby h.Name descending
                    select h
                ).ToList();
            }
            return (
                from h in houses
                orderby h.Name ascending
                select h
            ).ToList();
        }

        public List<House> GetHousesSortedByRate(bool descending)
        {

            var houses = houseDataSource.GetHouses();
            if (descending)
            {
                return (
                    from h in houses
                    orderby h.Rate descending
                    select h
                ).ToList();
            }
            return (
                from h in houses
                orderby h.Rate ascending
                select h
            ).ToList();
        }

        public OrganizationWithHouses? GetOrganizationById(long id)
        {
            var organization = findOrganizationById(id);
            if (organization == null)
            {
                return null;
            }
            var houses = new List<House>();
            var index = houseDataSource.getOrganizationIdIndex();
            if (!index.ContainsKey(id))
            {
                return new OrganizationWithHouses(organization, houses);
            }
            var indices = index[id];
            indices.ForEach(id =>
            {
                houses.Add(findHouseById(id)!);
            });
            return new OrganizationWithHouses(organization, houses);
        }

        public List<OrganizationWithHouses> GetOrganizations()
        {
            var organizations = organizationDataSource.GetOrganizations();
            var result = new List<OrganizationWithHouses>();
            organizations.ForEach(o =>
            {
                var withHouses = GetOrganizationById(o.Id);
                if (withHouses != null)
                {
                    result.Add(withHouses);
                }
            });
            return result;
        }

        public List<OrganizationWithHouses> GetOrganizationsSortedByName(bool descending)
        {
            var organizations = GetOrganizations();
            if (descending)
            {
                return (
                    from o in organizations
                    orderby o.Organization.Name descending
                    select o
                ).ToList();
            }
            return (
                from o in organizations
                orderby o.Organization.Name ascending
                select o
            ).ToList();
        }

        private House? findHouseById(long id)
        {
            var houses = houseDataSource.GetHouses();
            int l = 0;
            int r = houses.Count;
            while (l < r)
            {
                var midIndex = (l + r) >> 1;
                var house = houses[midIndex];
                var midId = house.Id;
                if (midId == id)
                {
                    return house;
                } else if (id > midId)
                {
                    l = midIndex + 1;
                } else
                {
                    r = midIndex;
                }
            }
            return null;
        }

        private Organization? findOrganizationById(long id)
        {
            var organizations = organizationDataSource.GetOrganizations();
            int l = 0;
            int r = organizations.Count;
            while (l < r)
            {
                var midIndex = (l + r) >> 1;
                var organization = organizations[midIndex];
                var midId = organization.Id;
                if (midId == id)
                {
                    return organization;
                } else if (id > midId)
                {
                    l = midIndex + 1;
                } else
                {
                    r = midIndex;
                }
            }
            return null;
        }
    }
}
