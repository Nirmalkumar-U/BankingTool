import { formatDate } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';

export interface TableColumn {
  field: string;
  header: string;
  sortable?: boolean;
  width?: string;
  format?: 'date' | 'currency' | 'custom'; // extendable
  dateFormat?: string; // for Angular date pipe format like 'dd/MM/yyyy'
}

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css']
})
export class DataTableComponent {
  @Input() columns: TableColumn[] = [];
  @Input() data: any[] = [];
  @Input() darkMode: boolean = false;


  @Output() pageChange = new EventEmitter<number>();

  currentPage = 1;
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';
  math = Math;
  pageSizeOptions = [10, 25, 50, 100];
  pageSize:number = 10;

  changePageSize() {
    this.currentPage = 1;
  }
  get visibleData() {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.sortedData.slice(start, end);
  }
  searchTerm: string = '';
  filteredData: any[] = [];

  ngOnChanges() {
    this.applyFilter(); // when @Input() data changes
  }
  ngOnInit() {
    this.filteredData = [...this.data];
  }

  onSearchChange(): void {
    this.currentPage = 1;
    this.applyFilter();
  }

  applyFilter(): void {
    if (!this.searchTerm || this.searchTerm.trim() === '') {
      this.filteredData = [...this.data];
    } else {
      const lowerTerm = this.searchTerm.toLowerCase();
      this.filteredData = this.data.filter(row =>
        this.columns.some(col => {
          const cellValue = row[col.field];
          return cellValue && cellValue.toString().toLowerCase().includes(lowerTerm);
        })
      );
    }
  }
  get sortedData(): any[] {
    let sorted = [...this.filteredData];
    if (this.sortColumn) {
      sorted.sort((a, b) => {
        const valueA = a[this.sortColumn];
        const valueB = b[this.sortColumn];
        return (valueA > valueB ? 1 : -1) * (this.sortDirection === 'asc' ? 1 : -1);
      });
    }
    return sorted;
  }

  changeSort(col: TableColumn) {
    if (!col.sortable) return;
    if (this.sortColumn === col.field) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = col.field;
      this.sortDirection = 'asc';
    }
  }

  totalPages(): number {
    return Math.ceil(this.data.length / this.pageSize);
  }

  changePage(page: number) {
    this.currentPage = page;
    this.pageChange.emit(this.currentPage);
  }

  formatCell(value: any, col: TableColumn): any {
    if (col.format === 'date' && value) {
      return formatDate(value, col.dateFormat || 'dd/MM/yyyy', 'en-US');
    }
    return value;
  }
  exportToCSV(): void {
    const rows = [this.columns.map(col => col.header)]; // headers
    for (const row of this.data) {
      const rowData = this.columns.map(col => {
        const raw = row[col.field];
        const formatted = this.formatCell(raw, col);
        return `"${(formatted ?? '').toString().replace(/"/g, '""')}"`; // Escape quotes
      });
      rows.push(rowData);
    }

    const csvContent = rows.map(e => e.join(',')).join('\n');
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });

    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    link.href = url;
    link.download = 'transactions.csv';
    link.click();
    URL.revokeObjectURL(url);
  }

}
