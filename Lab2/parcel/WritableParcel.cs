namespace Lab2.parcel
{
    public class WritableParcel
    {
        private BinaryWriter writer;

        public WritableParcel(string path)
        {
            writer = new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate));
        }

        public WritableParcel(BinaryWriter writer)
        {
            this.writer = writer;
        }

        public void putString(string s)
        {
            writer.Write(s);
        }

        public void putInt(int i)
        {
            writer.Write(i);
        }

        public void putLong(long l) {
            writer.Write(l);
        }

        public void putFloat(float f)
        {
            writer.Write(f);
        }
    }
}
