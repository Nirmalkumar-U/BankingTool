import { Component } from '@angular/core';
import { LoaderService } from '../../core/service/loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css']
})
export class LoaderComponent {
  isLoading = false;

  constructor(private loaderService: LoaderService) { }

  ngOnInit(): void {
    this.loaderService.loader$.subscribe(value => {
      this.isLoading = value;
    });
  }
}
