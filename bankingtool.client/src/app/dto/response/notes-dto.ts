export interface NoteDto {
  noteId: number;
  title: string;
  content: string;
  modifiedDate: Date;
  isEditing?: boolean;
}
