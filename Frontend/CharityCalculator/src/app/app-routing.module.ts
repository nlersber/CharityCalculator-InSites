import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { LoggedInCheckGuard } from './auth/logged-in-check.guard';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';

const routes: Routes = [
  { path: "login", component: LoginComponent, canActivate: [LoggedInCheckGuard]},
  { path: "main", component: MainComponent, canActivate: [AuthGuard]},
  { path: "", redirectTo: "main", pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
