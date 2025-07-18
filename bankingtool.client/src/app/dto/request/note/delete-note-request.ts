import { RequestId } from "../request-id";

export interface DeleteNoteRequestObject {
  request: DeleteNoteRequest;
}

export interface DeleteNoteRequest {
  note: DeleteNoteRequestNote;
}

export interface DeleteNoteRequestNote extends RequestId {

}

export const CreateDeleteNoteRequestObject = (noteId: number): DeleteNoteRequestObject => {
  let model: DeleteNoteRequestObject = {
    request: {
      note: {
        id: noteId
      }
    }
  }
  return model;
}
