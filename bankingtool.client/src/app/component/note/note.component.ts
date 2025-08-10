import { Component, OnInit } from "@angular/core";
import { debounceTime, Subject } from "rxjs";
import { NotificationService } from "../../core/notificationService/notification-service";
import { NoteService } from "../../core/service/note.service";
import { ResponseDto } from "../../dto/internal-dto/response-dto";
import { CreateDeleteNoteRequestObject } from "../../dto/request/note/delete-note-request";
import { CreateSaveNoteRequestObject } from "../../dto/request/note/save-note-request";
import { NoteDto } from "../../dto/response/notes-dto";

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {
  isPopupOpen = false;
  notes: NoteDto[] = [];
  selectedNote: NoteDto | null = null;
  newNoteMode = false;
  private autoSave$ = new Subject<void>();
  private debounceTimeMs = 1000; // 1 second delay


  constructor(private noteService: NoteService, private notificationService: NotificationService) {
    this.noteService.getAllNotes().subscribe((response: ResponseDto<NoteDto[]>) => {
      if (response.status == true) {
        this.notes = response.response;
      }
    });
  }

  ngOnInit(): void {
    this.autoSave$
      .pipe(debounceTime(this.debounceTimeMs))
      .subscribe(() => this.saveNote(false)); 
  }

  togglePopup(): void {
    if (this.selectedNote) {
      // If editing a note, go back to list
      this.selectedNote = null;
      this.newNoteMode = false;
    } else {
      // If already at list view, toggle the popup
      this.isPopupOpen = !this.isPopupOpen;
    }
  }


  openNote(note: NoteDto): void {
    this.selectedNote = { ...note };
    this.newNoteMode = false;
  }

  addNewNote(): void {
    let model = CreateSaveNoteRequestObject(null, '', '');
    this.noteService.saveNote(model).subscribe((response: ResponseDto<number>) => {
      if (response.status == true) {
        const newNote: NoteDto = {
          noteId: response.response,
          title: '',
          content: '',
          modifiedDate: new Date(),
          isEditing: true
        };

        this.selectedNote = newNote;
        this.newNoteMode = true;

        this.notes.unshift(newNote);

      } else {
        let messageList: string[] = [];
        if (Array.isArray(response?.errors) && response?.errors)
          messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)];
        messageList.push("Note save failed.");
        messageList.forEach((msg) => {
          this.notificationService.show(msg, 'error')
        });
      }
    });
  }


  saveNote(closeAfterSave = true): void {
    if (!this.selectedNote) return;

    const model = CreateSaveNoteRequestObject(
      this.selectedNote.noteId,
      this.selectedNote.title,
      this.selectedNote.content
    );

    this.noteService.saveNote(model).subscribe((response: ResponseDto<number>) => {
      if (response.status == true) {
        const index = this.notes.findIndex(n => n.noteId === this.selectedNote?.noteId);
        if (index > -1) {
          this.notes[index] = {
            ...this.selectedNote!,
            modifiedDate: new Date()
          };
        }

        if (closeAfterSave) {
          this.selectedNote = null;
          this.newNoteMode = false;
        }
      } else {
        let messageList: string[] = [];
        if (Array.isArray(response?.errors) && response?.errors)
          messageList = [
            ...response?.errors?.map(x => x.errorMessage),
            ...response?.validationErrors?.map(x => x.errorMessage)
          ];
        messageList.push("Note save failed.");
        messageList.forEach((msg) => {
          this.notificationService.show(msg, 'error');
        });
      }
    });
  }
  triggerAutoSave(): void {
    if (this.selectedNote) {
      this.autoSave$.next(); // debounce logic will handle the actual save
    }
  }


  closeEditor(): void {
    this.selectedNote = null;
    this.newNoteMode = false;
  }
  deleteNote(noteId: number) {
    let model = CreateDeleteNoteRequestObject(noteId);
    this.noteService.deleteNote(model).subscribe((response: ResponseDto<boolean>) => {
      if (response.status == true) {
        this.notes = this.notes.filter(n => n.noteId !== noteId);

        if (this.selectedNote?.noteId === noteId) {
          this.selectedNote = null;
          this.newNoteMode = false;
        }

        this.notificationService.show("Note Deleted successfully.", 'success');
      } else {
        let messageList: string[] = [];
        if (Array.isArray(response?.errors) && response?.errors)
          messageList = [
            ...response?.errors?.map(x => x.errorMessage),
            ...response?.validationErrors?.map(x => x.errorMessage)
          ];
        messageList.push("Failed to delete note.");
        messageList.forEach((msg) => {
          this.notificationService.show(msg, 'error');
        });
      }
    });
  }

}
