namespace Kulikova
{
    public class ConfDummyClass : IConfiguration
    {
        int _health = 2000;

        public int Health()
        {
            return _health;
        }
    }
}