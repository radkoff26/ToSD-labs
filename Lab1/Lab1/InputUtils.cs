namespace Lab1
{
    internal class InputUtils
    {
        public static char GetCharBlocking()
        {
            return char.ToLower(Console.ReadKey(true).KeyChar);
        }
    }
}
