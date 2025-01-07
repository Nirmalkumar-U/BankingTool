namespace BankingTool.Repository
{
    public interface IEntityRepository<T1>
    {
        bool Insert<T>(T entity, bool isSave = true) where T : class;
        void Insert<T>(IEnumerable<T> entities, bool isSave = true) where T : class;
        void Insert<T>(List<T> entities, bool isSave = true) where T : class;

        bool Delete<T>(T entity, bool isSave = true) where T : class;
        void Delete<T>(IEnumerable<T> entities, bool isSave = true) where T : class;
        void Delete<T>(List<T> entities, bool isSave = true) where T : class;

        void Update<T>(T entity, bool isSave = true) where T : class;
        void Update<T>(IEnumerable<T> entities, bool isSave = true) where T : class;
        void Update<T>(List<T> entities, bool isSave = true) where T : class;

        bool SaveData();
    }
}
