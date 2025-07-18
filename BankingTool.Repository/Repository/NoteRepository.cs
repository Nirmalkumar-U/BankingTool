using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class NoteRepository(DataContext dataContext) : EntityRepository<Note>(dataContext), INoteRepository
    {
        public async Task<List<Note>> GetAllNotes()
        {
            return await dataContext.Note.Where(x => x.UserId == dataContext.UserId && !x.IsDeleted).ToListAsync();
        }
        public async Task<Note> GetNoteById(int noteId)
        {
            return await dataContext.Note.FirstOrDefaultAsync(x => x.NoteId == noteId);
        }
        public async Task<int?> AddNote(Note note)
        {
            note.UserId = dataContext.UserId;
            return await Task.FromResult(InsertNote(note));
        }
        public async Task<bool> SaveNote(Note model)
        {
            return await Task.FromResult(UpdateNote(model));
        }
        public async Task<bool> DeleteNote(Note model)
        {
            return await Task.FromResult(UpdateNote(model));
        }


        private int? InsertNote(Note model)
        {
            model.CreatedBy = dataContext.UserEmail;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = null;
            model.ModifiedDate = null;
            model.IsDeleted = false;
            var isInserted = Insert(model);
            return isInserted ? model.NoteId : null;
        }
        private bool UpdateNote(Note model)
        {
            model.ModifiedBy = dataContext.UserEmail;
            model.ModifiedDate = DateTime.Now;
            Update(model);
            return true;
        }
    }
}
