namespace MonsterCouchTest.Infrastructure
{
    public static class ServiceLocator
    {
        public static IServiceResolver Current { get; internal set; }
    }
}
