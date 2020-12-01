namespace Plan.Core.IDatabase
{
    public interface IBazaDanych
    {
        IRepozytorium<T> DajTabele<T>() where T : class;
        void Zapisz();
    }
}
