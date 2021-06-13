import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {

  form: FormGroup
  currentRate: number = 0

  constructor(private data: DataService, fb: FormBuilder) {
    // Setup form
    this.form=fb.group({
      amount: fb.control(null, [Validators.required, Validators.min(0)])
    })

    // Get current rate
    this.data.getCurrentRate().subscribe(s=> this.currentRate=s)
   }

  setRate(){
    if(this.form.invalid)return;
    var amount = this.form.get("amount").value
    this.data.setCurrentRate(amount).subscribe(s=> this.currentRate=s)
  }

}
