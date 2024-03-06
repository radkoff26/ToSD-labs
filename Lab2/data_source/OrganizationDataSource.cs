using Lab2.entities;
using Lab2.parcel;

namespace Lab2.data_source
{
    internal class OrganizationDataSource
    {
        private static string ORGANIZATION_FILENAME = "organizations.bin";

        private string rootOrganizationDirPath;

        private long nextOrganizationId = 1;

        private List<Organization> organizations = new List<Organization>();

        public OrganizationDataSource(string rootOrganizationDirPath)
        {
            this.rootOrganizationDirPath = rootOrganizationDirPath;
            ProcessPath();
            LoadData();
        }

        public List<Organization> GetOrganizations()
        {
            return organizations;
        }

        public void AddOrganization(Organization organization)
        {
            if (organization != null)
            {
                organization.Id = nextOrganizationId++;
                organizations.Add(organization);
                WriteData();
            }
        }

        public void RemoveOrganization(Organization organization)
        {
            if (organization != null)
            {
                organizations.Remove(organization);
                WriteData();
            }
        }

        private void ProcessPath()
        {
            DirectoryInfo dir = new DirectoryInfo(rootOrganizationDirPath);
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

        private void LoadData()
        {
            using (BinaryReader reader = createOrganizationReader())
            {
                if (reader.BaseStream.Length > 0)
                {
                    ReadableParcel parcel = new ReadableParcel(reader);
                    long count = parcel.getLong();
                    organizations.Clear();
                    for (int i = 0; i < count; i++)
                    {
                        organizations.Add(new Organization(parcel));
                    }
                    nextOrganizationId = parcel.getLong();
                }
            }
        }

        private void WriteData()
        {
            ClearFile();
            using (BinaryWriter writer = createOrganizationWriter())
            {
                writer.Write((long) organizations.Count);
                WritableParcel parcel = new WritableParcel(writer);
                organizations.ForEach(o =>
                {
                    o.writeToParcel(parcel);
                });
                writer.Write(nextOrganizationId);
            }
        }

        private void ClearFile()
        {
            File.WriteAllText(BuildFilePath(), string.Empty);
        }

        private BinaryReader createOrganizationReader()
        {
            return new BinaryReader(new FileStream(BuildFilePath(), FileMode.OpenOrCreate));
        }

        private BinaryWriter createOrganizationWriter()
        {
            return new BinaryWriter(new FileStream(BuildFilePath(), FileMode.OpenOrCreate));
        }

        private string BuildFilePath()
        {
            return rootOrganizationDirPath + "/" + ORGANIZATION_FILENAME;
        }
    }
}
