import { Component, OnInit } from "@angular/core";


interface Note {
  noteId: number;
  title: string;
  content: string;
  createdDate: Date;
  isEditing?: boolean;
}

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {
  isPopupOpen = false;
  notes: Note[] = [];
  selectedNote: Note | null = null;
  newNoteMode = false;

  htmlContent: string = '';
  quillModules = {
    toolbar: [
      ['bold', 'italic', 'underline', 'strike'],
      [{ list: 'ordered' }, { list: 'bullet' }],
      [{ align: [] }],
      ['link', 'code-block'],
      ['clean']
    ]
  };

  ngOnInit(): void {
    this.loadNotes();
  }


  togglePopup(): void {
    this.isPopupOpen = !this.isPopupOpen;
    if (this.isPopupOpen) {
      this.selectedNote = null;
      this.newNoteMode = false;
    }
  }

  loadNotes(): void {
    // Replace this with your service API call
    this.notes = [
      { noteId: 1, title: 'Grocery Budget', content: 'Plan to spend ₹3000 this month.', createdDate: new Date() },
      { noteId: 2, title: 'Vacation Fund', content: 'Save ₹5000 before December.', createdDate: new Date() }
    ];
  }

  openNote(note: Note): void {
    this.selectedNote = { ...note };
    this.newNoteMode = false;
  }

  addNewNote(): void {
    this.selectedNote = { noteId: 0, title: '', content: '', createdDate: new Date(), isEditing: true };
    this.newNoteMode = true;
  }

  saveNote(): void {
    if (!this.selectedNote) return;

    if (this.selectedNote.noteId === 0) {
      // Create logic
      this.selectedNote.noteId = Math.floor(Math.random() * 10000); // mock ID
      this.notes.unshift(this.selectedNote);
    } else {
      // Update logic
      const index = this.notes.findIndex(n => n.noteId === this.selectedNote?.noteId);
      if (index > -1) this.notes[index] = { ...this.selectedNote };
    }

    this.selectedNote = null;
    this.newNoteMode = false;
  }

  closeEditor(): void {
    this.selectedNote = null;
    this.newNoteMode = false;
  }
  deleteNote(noteToDelete: any) {
    
  }

}
