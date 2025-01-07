import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AppPaths } from './constant/constant';

const routes: Routes = [
  { path: '', redirectTo: AppPaths.home, pathMatch: 'full' },
  {
    path: '',
    loadChildren: () =>
      import('../app/component/component.module').then(m => m.ComponentModule),
    pathMatch: 'prefix',
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules,
      scrollPositionRestoration: 'enabled',
      useHash: false,
      onSameUrlNavigation: 'reload',
      enableTracing: false
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule { }
