import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  private roles: string[] = []

  constructor(private auth: AuthService, private router: Router) {
    this.auth.getRoles().subscribe(s => this.roles = s.map(s => s.toLowerCase()))
  }

  ngOnInit(): void {
  }

  hasRole(role: string): boolean {
    return this.roles.includes(role.toLowerCase())
  }

  logout(){
    this.auth.logout()
    this.router.navigate(["/login"])
  }

}
