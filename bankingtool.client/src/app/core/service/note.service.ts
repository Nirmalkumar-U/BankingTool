import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  constructor(private httpService: HttpService) { }

  getAllNotes(): Observable<any> {
    return this.httpService.get('Note/GetAllNotes');
  }
}
