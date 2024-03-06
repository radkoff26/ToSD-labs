using Lab2.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.repository
{
    internal interface Repository
    {

        void AddOrganization(Organization organization);

        void AddHouse(House house);

        bool DeleteOrganizationById(long id);

        bool DeleteHouseById(long id);

        List<House> GetHousesSortedByRate(bool descending);

        List<House> GetHousesSortedByName(bool descending);

        List<OrganizationWithHouses> GetOrganizationsSortedByName(bool descending);

        long CountHouses();

        long CountOrganizations();

        List<OrganizationWithHouses> GetOrganizations();

        List<House> GetHouses();

        List<House> GetHousesByCity(string city);

        House? GetHouseById(long id);

        OrganizationWithHouses? GetOrganizationById(long id);

        List<OrganizationWithHouses> GetOrganizationsFilteredByLocationRange(float fromLatitude, float toLatitude, float fromLongitude, float toLongitude);

        float GetAverageHousesRating();
    }
}
