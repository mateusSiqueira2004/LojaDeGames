namespace LojaDeGames.Security
{
    public class Settings
    {
        private static string secret = "5b36501a29a211a3fc25d27c2761fa2a540108f6a67a963b7ac2e4b4bc466adb";
        public static string Secret { get => secret; set => secret = value; }
    }
}
