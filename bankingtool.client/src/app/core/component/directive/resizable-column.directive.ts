import { Directive, ElementRef, HostListener, Renderer2, AfterViewInit } from '@angular/core';

@Directive({
  selector: '[appResizableColumn]'
})
export class ResizableColumnDirective implements AfterViewInit {
  private resizer!: HTMLElement;
  private startX!: number;
  private startWidth!: number;

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  ngAfterViewInit(): void {
    const nativeEl = this.el.nativeElement;
    const resizer = this.renderer.createElement('span');
    this.resizer = resizer;

    this.renderer.setStyle(resizer, 'width', '5px');
    this.renderer.setStyle(resizer, 'height', '100%');
    this.renderer.setStyle(resizer, 'position', 'absolute');
    this.renderer.setStyle(resizer, 'top', '0');
    this.renderer.setStyle(resizer, 'right', '0');
    this.renderer.setStyle(resizer, 'cursor', 'col-resize');
    this.renderer.setStyle(resizer, 'user-select', 'none');

    this.renderer.setStyle(nativeEl, 'position', 'relative');
    this.renderer.appendChild(nativeEl, resizer);

    resizer.addEventListener('mousedown', this.onMouseDown.bind(this));
  }

  onMouseDown(event: MouseEvent): void {
    this.startX = event.pageX;
    this.startWidth = this.el.nativeElement.offsetWidth;

    const mouseMove = this.onMouseMove.bind(this);
    const mouseUp = this.onMouseUp.bind(this);

    document.addEventListener('mousemove', mouseMove);
    document.addEventListener('mouseup', mouseUp);
  }

  onMouseMove(event: MouseEvent): void {
    const offset = event.pageX - this.startX;
    const newWidth = this.startWidth + offset;
    this.renderer.setStyle(this.el.nativeElement, 'width', `${newWidth}px`);
  }

  onMouseUp(): void {
    document.removeEventListener('mousemove', this.onMouseMove);
    document.removeEventListener('mouseup', this.onMouseUp);
  }
}
