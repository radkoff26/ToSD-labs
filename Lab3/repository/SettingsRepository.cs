using Lab3.data;

namespace Lab3.repository
{
    internal class SettingsRepository
    {
        private const string PATH_TO_FILE = "./settings/settings.bin";
        private Color DEFAULT_POINT_COLOR = Color.FromArgb(50, 0, 25, 49);
        private Color DEFAULT_LINE_COLOR = Color.Green;

        public Settings GetCurrentSettings()
        {
            if (!File.Exists(PATH_TO_FILE)) {
                return new Settings(DEFAULT_POINT_COLOR, DEFAULT_LINE_COLOR);
            }
            using BinaryReader reader = new BinaryReader(new FileStream(PATH_TO_FILE, FileMode.Open));
            Color pointColor = Color.FromArgb(reader.ReadInt32());
            Color lineColor = Color.FromArgb(reader.ReadInt32());
            reader.Close();
            return new Settings(pointColor, lineColor);
        }

        public void SetCurrentSettings(Settings settings)
        {
            RecreatePathIfNecessary();
            using BinaryWriter writer = new BinaryWriter(new FileStream(PATH_TO_FILE, FileMode.OpenOrCreate));
            writer.Write(settings.pointColor.ToArgb());
            writer.Write(settings.lineColor.ToArgb());
            writer.Close();
        }

        private void RecreatePathIfNecessary()
        {
            if (!Directory.Exists("./settings"))
            {
                Directory.CreateDirectory("./settings");
            }
        }
    }
}
