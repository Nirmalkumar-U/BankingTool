namespace BankingTool.Repository
{
    public interface IStoreProcedureRepository
    {
        Task<List<T>> RunStoreProcedures<T>(string sqlQuery, Dictionary<string, string> para);
    }
}
