import { RequestNullableId } from "../request-id";

export interface SaveNoteRequestObject {
  request: SaveNoteRequest;
}

export interface SaveNoteRequest {
  note: SaveNoteRequestNote;
}

export interface SaveNoteRequestNote extends RequestNullableId {
  title: string;
  content: string;
}

export const CreateSaveNoteRequestObject = (noteId: number | null, title: string, content: string): SaveNoteRequestObject => {
  let model: SaveNoteRequestObject = {
    request: {
      note: {
        id: noteId,
        content: content,
        title: title
      }
    }
  }
  return model;
}

