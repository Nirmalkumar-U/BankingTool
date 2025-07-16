using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class NoteRepository(DataContext dataContext) : EntityRepository<Note>(dataContext), INoteRepository
    {
        public async Task<List<Note>> GetAllNotes()
        {
            return await dataContext.Note.Where(x => x.UserId == dataContext.UserId).ToListAsync();
        }
    }
}
