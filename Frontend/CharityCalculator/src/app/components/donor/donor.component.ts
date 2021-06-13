import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventType } from 'src/app/models';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-donor',
  templateUrl: './donor.component.html',
  styleUrls: ['./donor.component.scss']
})
export class DonorComponent implements OnInit {

  form: FormGroup
  error: boolean
  hasSubmitted: boolean

  currentRate: number
  events: EventType[] = []

  amount = 0

  constructor(private data: DataService, fb: FormBuilder) {
    this.data.getCurrentRate().subscribe(
      s=> this.currentRate=s,
      err => {
        console.log(err)
        this.currentRate=20
      }
      )

      this.data.getEventTypes().subscribe(
        s=> this.events = s,
        err => console.log(err)
      )

      this.form=fb.group({
        amount: fb.control("0", [Validators.required, Validators.min(0)]),
        event: fb.control("", Validators.required)
      })
   }

  ngOnInit(): void {
  }

  getDeductible(){
    if(this.form.invalid){
      return;
    }
    var amount = this.form.get("amount").value
    var event = this.form.get("event").value
    this.data.getDeductibleAmount(amount, event).subscribe(s=> this.amount=s)
    
  }

}
