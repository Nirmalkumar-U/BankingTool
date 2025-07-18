using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.RequestDto.Note;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class NoteService(INoteRepository noteRepository) : INoteService
    {
        private readonly INoteRepository _noteRepository = noteRepository;

        public async Task<ResponseDto<List<NoteDto>>> GetAllNotes()
        {
            var notes = await _noteRepository.GetAllNotes();
            return new ResponseDto<List<NoteDto>>
            {
                DropDownList = [],
                Errors = [],
                Response = [.. notes.Select(x=> new NoteDto{
                    NoteId = x.NoteId,
                    Content = x.Content,
                    Title = x.Title,
                    ModifiedDate = x.ModifiedDate ?? x.CreatedDate
                })],
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
        }
        public async Task<ResponseDto<int?>> SaveNote(SaveNoteRequestNote model)
        {
            if (model.Id.HasValue && model.Id.Value > 0)
            {
                List<ValidationResults> validationResults = [];
                var existingNote = await _noteRepository.GetNoteById(model.Id.Value);
                if (existingNote != null)
                {
                    existingNote.Content = model.Content ?? "";
                    existingNote.Title = model.Title;
                    await _noteRepository.SaveNote(existingNote);
                }
                else
                {
                    validationResults.Add(new ValidationResults
                    {
                        ErrorMessage = "Note not found.",
                        PropertyName = "Note",
                        PropertyPath = "Request.Note"
                    });
                }
                return new ResponseDto<int?>
                {
                    DropDownList = [],
                    Errors = [],
                    Response = model.Id,
                    StatuCode = validationResults.Count() > 0 ? 400 : 200,
                    Status = validationResults.Count() == 0,
                    ValidationErrors = validationResults
                };
            }
            else
            {
                Note note = new Note()
                {
                    Content = model.Content ?? "",
                    Title = model.Title,
                };
                var noteId = await _noteRepository.AddNote(note);
                return new ResponseDto<int?>
                {
                    DropDownList = [],
                    Errors = noteId == null ? [new Errors { ErrorMessage = "Failed to save note.", PropertyName = "Note" }] : [],
                    Response = noteId,
                    StatuCode = noteId == null ? 400 : 200,
                    Status = noteId != null,
                    ValidationErrors = []
                };
            }
        }
        public async Task<ResponseDto<bool>> DeleteNote(int noteId)
        {
            var note = await _noteRepository.GetNoteById(noteId);
            if (note == null)
            {
                return new ResponseDto<bool>
                {
                    DropDownList = [],
                    Errors = [new Errors { ErrorMessage = "Note not found.", PropertyName = "Note" }],
                    Response = false,
                    StatuCode = 400,
                    Status = false,
                    ValidationErrors = []
                };
            }
            note.IsDeleted = true;
            var isDeleted = await _noteRepository.SaveNote(note);
            return new ResponseDto<bool>
            {
                DropDownList = [],
                Errors = isDeleted ? [new Errors { ErrorMessage = "Failed to save note.", PropertyName = "Note" }] : [],
                Response = isDeleted,
                StatuCode = isDeleted ? 200 : 400,
                Status = isDeleted,
                ValidationErrors = []
            };
        }
    }
}

