import { Injectable } from '@angular/core';
import { ClaimKey } from '../../constant/constant';
import { LocalStorageService } from '../local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class IndexedDbServiceService {
  constructor(private localstorage: LocalStorageService) {
    let idb = this.localstorage.getItem(ClaimKey.idb);
    if (idb) {
      this.db = JSON.parse(idb);
    }
  }
  private db: IDBDatabase | undefined;

  public initDb(dbName: string, dbVersion: number, primaryKey: string, tableName: string, columns: CreateIndexedDb[]): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      const request = indexedDB.open(dbName, dbVersion);

      request.onerror = (event) => {
        reject(`Createing ${dbName}: IndexedDB error: ${event}`);
      };

      request.onupgradeneeded = (event) => {
        const db = (event.target as any).result;
        const objectStore = db.createObjectStore(tableName, { keyPath: primaryKey, autoIncrement: true });
        columns.forEach(({ columnName, isUnique }) => objectStore.createIndex(columnName, columnName, { unique: isUnique }));
      };

      request.onsuccess = (event) => {
        this.db = (event.target as any).result;
        this.localstorage.setItem(ClaimKey.idb, JSON.stringify(this.db))
        resolve();
      };
    });
  }

  public getAllData(tableName: string): Promise<any[]> {
    return new Promise<any[]>((resolve, reject) => {
      const transaction = this.db!.transaction([tableName], 'readonly');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.getAll();
      request.onerror = (event) => {
        reject(`Get ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = (event) => {
        resolve(request.result);
      };
    });
  }

  public addData(tableName: string, data: any): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      const transaction = this.db!.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.add(data);
      request.onerror = (event) => {
        reject(`Add ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = (event) => {
        resolve();
      };
    });
  }

  public deleteData(tableName: string, id: number): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      const transaction = this.db!.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.delete(id);
      request.onerror = (event) => {
        reject(`Delete ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = (event) => {
        resolve();
      };
    });
  }

  public updateData(tableName: string, data: any): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      const transaction = this.db!.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.put(data);
      request.onerror = (event) => {
        reject(`Update ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = (event) => {
        resolve();
      };
    });
  }

  public clearData(tableName: string): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      const transaction = this.db!.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.clear();
      request.onerror = (event) => {
        reject(`Clear ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = (event) => {
        resolve();
      };
    });
  }
  public deleteDatabase(databaseName: string): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      const deleteRequest = indexedDB.deleteDatabase(databaseName);

      deleteRequest.onsuccess = () => {
        console.log('Database deleted successfully');
        resolve();
      };

      deleteRequest.onerror = (event) => {
        console.error('Error deleting database:');
        reject(`Error deleting database: `);
      };
    });
  }
}
export interface CreateIndexedDb {
  columnName: string;
  isUnique: boolean;
}
