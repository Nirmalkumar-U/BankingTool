using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllNotes();
        Task<Note> GetNoteById(int noteId);
        Task<int?> AddNote(Note note);
        Task<bool> SaveNote(Note model);
    }
}
