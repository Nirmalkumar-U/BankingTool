using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllNotes();
    }
}
