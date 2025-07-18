import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DeleteNoteRequestObject } from '../../dto/request/note/delete-note-request';
import { SaveNoteRequestObject } from '../../dto/request/note/save-note-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  constructor(private httpService: HttpService) { }

  getAllNotes(): Observable<any> {
    return this.httpService.get('Note/GetAllNotes');
  }
  saveNote(model: SaveNoteRequestObject): Observable<any> {
    return this.httpService.post('Note/SaveNote', model, false);
  }

  deleteNote(model: DeleteNoteRequestObject): Observable<any> {
    return this.httpService.post('Note/DeleteNote', model);
  }
}
