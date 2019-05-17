import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'benefits-calculator-component',
  templateUrl: './benefits-calculator.component.html'
})

export class BenefitsCalculatorComponent {
  public benefitsCost = 0;


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  public calculateCosts() {
    this.http.get<number>(this.baseUrl + 'api/BenefitsCalculation/BenefitsCost').subscribe(result => {
      this.benefitsCost = result;
    }, error => console.error(error));
  }

}
