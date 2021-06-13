import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventType } from 'src/app/models/models';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-optimal',
  templateUrl: './optimal.component.html',
  styleUrls: ['./optimal.component.scss']
})
export class OptimalComponent {

  form: FormGroup
  events: EventType[] = []
  
  split: number[] = []

  constructor(private data: DataService, fb: FormBuilder) {

    this.form=fb.group({
      amount: fb.control("0", [Validators.required, Validators.min(0)]),
      event: fb.control("", Validators.required)
    })

    this.data.getEventTypes().subscribe(
      s=> this.events = s,
      err => console.log(err)
    )

   }

  calculateSplit(){
    if(this.form.invalid) return;
    var amount = this.form.get("amount").value
    var event = this.form.get("event").value

    this.data.getOptimalSplit(amount, event).subscribe(s=> this.split=s)
  }

}
