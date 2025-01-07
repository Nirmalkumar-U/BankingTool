import { APP_INITIALIZER, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InitializerService } from '../core/initailizerService/initializer.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
  ],
  providers: [{
    provide: APP_INITIALIZER,
    useFactory: (appInitializer: InitializerService) =>
      appInitializer.init(),
    deps: [InitializerService],
    multi: true,
  }]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule?: CoreModule) {
    // Do not allow multiple injections
    if (parentModule) {
      throw new Error(
        'CoreModule has already been loaded. Import this module in the AppModule only.'
      );
    }
  }
}
