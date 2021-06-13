import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  form: FormGroup
  currentRate: number = 0

  constructor(private data: DataService, fb: FormBuilder) {

    this.form=fb.group({
      amount: fb.control(null, [Validators.required, Validators.min(0)])
    })

    this.data.getCurrentRate().subscribe(s=> this.currentRate=s)

    
   }

  ngOnInit(): void {
  }

  setRate(){
    if(this.form.invalid)return;
    var amount = this.form.get("amount").value
    this.data.setCurrentRate(amount).subscribe(s=> this.currentRate=s)
  }

}
