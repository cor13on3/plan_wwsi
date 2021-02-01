namespace Plan.Core.IDatabase
{
    public interface IBazaDanych
    {
        IRepozytorium<T> DajRepozytorium<T>() where T : class;
        void Zapisz();
    }
}
