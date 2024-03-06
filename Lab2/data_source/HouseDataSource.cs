using Lab2.entities;
using Lab2.parcel;

namespace Lab2.data_source
{
    internal class HouseDataSource
    {
        private static string HOUSES_FILENAME = "houses.bin";
        private static string CITY_INDEX_FILENAME = "houses_city_index.bin";
        private static string ORGANIZATION_ID_INDEX_FILENAME = "houses_organization_id_index.bin";

        private string routeHouseDirPath;

        private List<House> houses = new List<House>();

        private long nextHouseId = 1;

        private Dictionary<string, List<long>> cityIndex = new Dictionary<string, List<long>>();

        private Dictionary<long, List<long>> organizationIdIndex = new Dictionary<long, List<long>>();

        public HouseDataSource(string routeHouseDirPath) {
            this.routeHouseDirPath = routeHouseDirPath;
            ProcessPath();
            ReloadData();
        }

        public void AddHouse(House house)
        {
            if (house != null)
            {
                house.Id = nextHouseId++;
                houses.Add(house);
                IndexHouse(house);
                RewriteData();
            }
        }

        public List<House> GetHouses()
        {
            return houses;
        }

        public Dictionary<string, List<long>> getCityIndex()
        {
            return cityIndex;
        }

        public Dictionary<long, List<long>> getOrganizationIdIndex()
        {
            return organizationIdIndex;
        }

        public void RemoveHouse(House house)
        {
            if (house != null)
            {
                houses.Remove(house);
                DeindexHouse(house);
                RewriteData();
            }
        }

        private void DeindexHouse(House house)
        {
            DeindexHouseCity(house);
            DeindexHouseOrganization(house);
        }

        private void DeindexHouseCity(House house)
        {
            var indices = cityIndex[house.City];
            if (indices.Count == 0)
            {
                cityIndex.Remove(house.City);
                return;
            }
            indices.Remove(house.Id);
        }

        private void DeindexHouseOrganization(House house)
        {
            var indices = organizationIdIndex[house.OrganizationId];
            if (indices.Count == 0)
            {
                organizationIdIndex.Remove(house.OrganizationId);
                return;
            }
            indices.Remove(house.Id);
        }

        private void IndexHouse(House house)
        {
            UpdateCityIndexForHouse(house);
            UpdateOrganizationIdIndexForHouse(house);
        }

        private void UpdateCityIndexForHouse(House house)
        {
            if (!cityIndex.ContainsKey(house.City))
            {
                cityIndex.Add(house.City, new List<long>());
            }
            List<long> indices = cityIndex[house.City];
            indices.Add(house.Id);
        }

        private void UpdateOrganizationIdIndexForHouse(House house)
        {
            if (!organizationIdIndex.ContainsKey(house.OrganizationId))
            {
                organizationIdIndex.Add(house.OrganizationId, new List<long>());
            }
            List<long> indices = organizationIdIndex[house.OrganizationId];
            indices.Add(house.Id);
        }

        private void ProcessPath()
        {
            DirectoryInfo dir = new DirectoryInfo(routeHouseDirPath);
            RecreateDirHierarchy(dir);
        }

        private void RecreateDirHierarchy(DirectoryInfo? dir)
        {
            if (dir == null || dir.Exists)
            {
                return;
            }
            RecreateDirHierarchy(dir.Parent);
            dir.Create();
        }

        private void RewriteData()
        {
            WriteHouses();
            WriteCityIndex();
            WriteOrganizationIdIndex();
        }

        private void ReloadData()
        {
            LoadHouses();
            LoadCityIndex();
            LoadOrganizationIdIndex();
        }

        private void LoadHouses()
        {
            string filename = BuildHousesFilename();
            using (BinaryReader housesReader = CreateReader(filename))
            {
                if (housesReader.BaseStream.Length > 0)
                {
                    ReadableParcel parcel = new ReadableParcel(housesReader);
                    long housesCount = parcel.getLong();
                    houses.Clear();
                    for (int i = 0; i < housesCount; i++)
                    {
                        houses.Add(new House(parcel));
                    }
                    nextHouseId = parcel.getLong();
                }
            }
        }

        private void LoadCityIndex()
        {
            string filename = BuildCityIndexFilename();
            using (BinaryReader cityIndexReader = CreateReader(filename))
            {
                if (cityIndexReader.BaseStream.Length > 0)
                {
                    long entryCount = cityIndexReader.ReadInt32();
                    cityIndex.Clear();
                    for (int i = 0; i < entryCount; i++)
                    {
                        string city = cityIndexReader.ReadString();
                        int indicesCount = cityIndexReader.ReadInt32();
                        List<long> indices = new List<long>();
                        for (int j = 0; j < indicesCount; j++)
                        {
                            indices.Add(cityIndexReader.ReadInt64());
                        }
                        cityIndex.Add(city, indices);
                    }
                }
            }
        }

        private void LoadOrganizationIdIndex()
        {
            string filename = BuildOrganizationIdIndexFilename();
            using (BinaryReader organizationIdIndexReader = CreateReader(filename))
            {
                if (organizationIdIndexReader.BaseStream.Length > 0)
                {
                    long entryCount = organizationIdIndexReader.ReadInt32();
                    organizationIdIndex.Clear();
                    for (int i = 0; i < entryCount; i++)
                    {
                        long organizationId = organizationIdIndexReader.ReadInt64();
                        int indicesCount = organizationIdIndexReader.ReadInt32();
                        List<long> indices = new List<long>();
                        for (int j = 0; j < indicesCount; j++)
                        {
                            indices.Add(organizationIdIndexReader.ReadInt64());
                        }
                        organizationIdIndex.Add(organizationId, indices);
                    }
                }
            }
        }

        private void WriteHouses()
        {
            string filename = BuildHousesFilename();
            // Clearing file to completely rewrite it
            File.WriteAllText(filename, string.Empty);
            using (BinaryWriter housesWriter = CreateWriter(filename))
            {
                housesWriter.Write((long) houses.Count);
                WritableParcel parcel = new WritableParcel(housesWriter);
                houses.ForEach(house =>
                {
                    house.writeToParcel(parcel);
                });
                housesWriter.Write(nextHouseId);
            }
        }

        private void WriteCityIndex()
        {
            string filename = BuildCityIndexFilename();
            // Clearing file to completely rewrite it
            File.WriteAllText(filename, string.Empty);
            using (BinaryWriter cityIndexWriter = CreateWriter(filename))
            {
                cityIndexWriter.Write(cityIndex.Count);
                foreach (var entry in cityIndex)
                {
                    cityIndexWriter.Write(entry.Key);
                    cityIndexWriter.Write(entry.Value.Count);
                    entry.Value.ForEach(cityIndexWriter.Write);
                }
            }
        }

        private void WriteOrganizationIdIndex()
        {
            string filename = BuildOrganizationIdIndexFilename();
            // Clearing file to completely rewrite it
            File.WriteAllText(filename, string.Empty);
            using (BinaryWriter organizationIdIndexWriter = CreateWriter(filename))
            {
                organizationIdIndexWriter.Write(organizationIdIndex.Count);
                foreach (var entry in organizationIdIndex)
                {
                    organizationIdIndexWriter.Write(entry.Key);
                    organizationIdIndexWriter.Write(entry.Value.Count);
                    entry.Value.ForEach(organizationIdIndexWriter.Write);
                }
            }
        }

        private string BuildHousesFilename()
        {
            return routeHouseDirPath + "/" + HOUSES_FILENAME;
        }

        private string BuildCityIndexFilename()
        {
            return routeHouseDirPath + "/" + CITY_INDEX_FILENAME;
        }

        private string BuildOrganizationIdIndexFilename()
        {
            return routeHouseDirPath + "/" + ORGANIZATION_ID_INDEX_FILENAME;
        }

        private BinaryReader CreateReader(string path)
        {
            return new BinaryReader(new FileStream(path, FileMode.OpenOrCreate));
        }

        private BinaryWriter CreateWriter(string path)
        {
            return new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate));
        }
    }
}
