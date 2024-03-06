namespace Lab2.parcel
{
    public class ReadableParcel
    {
        private BinaryReader reader;

        public ReadableParcel(string path)
        {
            reader = new BinaryReader(new FileStream(path, FileMode.OpenOrCreate));
        }

        public ReadableParcel(BinaryReader reader) {
            this.reader = reader;
        }

        public string getString()
        {
            return reader.ReadString();
        }

        public int getInt()
        {
            return reader.ReadInt32();
        }

        public long getLong() {
            return reader.ReadInt64();
        }

        public float getFloat()
        {
            return (float)reader.ReadSingle();
        }
    }
}
