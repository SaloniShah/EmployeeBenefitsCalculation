import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Spouse, Dependent, Employee } from '../models/employee-model'
import { BenefitsCost } from '../models/benefits-cost'

@Component({
  selector: 'benefits-calculator-component',
  templateUrl: './benefits-calculator.component.html',
  styleUrls: ['./benefits-calculator.component.scss']
})

export class BenefitsCalculatorComponent {

  public benefitsCost: BenefitsCost;
  public employee: Employee = {
    name: null,
    spouse: { name: null },
    dependents: []
  };
  public spouseCovered: boolean = false;
  public numberOfDependents: number = 0;
  public showDependentsArray: boolean = false;
  public validationError: boolean = false;
  public employeeValidationError: boolean = false;
  public spouseValidationError: boolean = false;
  public dependentValidationError: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  public addDependents() {
    this.employee.dependents = [];
     if (this.numberOfDependents > 0) {
      for (let i = 0; i < this.numberOfDependents; i++) {
        this.employee.dependents.push({
          name: null
        })
      }
      this.showDependentsArray = true;
    }
    else {
      this.showDependentsArray = false;
    }
  }

  public calculateCosts() {
    this.validateModel();
    console.log(this.employee);
    if (!this.validationError) {
      this.http.post<BenefitsCost>(this.baseUrl + 'api/BenefitsCalculation/BenefitsCost', this.employee).subscribe(result => {
        this.benefitsCost = result;
      }, error => console.error(error));
    }
  }

  public validateModel() {

    if (this.employee.name === null || this.employee.name === '') {
      this.employeeValidationError = true;
    }
    else {
      this.employeeValidationError = false;
    }

    if (this.spouseCovered && (this.employee.spouse.name === null || this.employee.spouse.name === '')) {
      this.spouseValidationError = true;
    }
    else {
      this.spouseValidationError = false;
    }

    if ((this.numberOfDependents > 0 && this.employee.dependents.length === 0) || this.anyDependentsHaveInvalidName()) {
      this.dependentValidationError = true;
    }
    else {
      this.dependentValidationError = false;
    }

    if (this.employeeValidationError || this.spouseValidationError || this.dependentValidationError) {
      this.validationError = true;
    }
    else {
      this.validationError = false;
    }
  
  }

  anyDependentsHaveInvalidName() {
    let anyValidationError = false;
    this.employee.dependents.forEach(d => {
      if (d.name === null || d.name === '') {
        anyValidationError = true;
      }
    });
    return anyValidationError;
  }
    
}
