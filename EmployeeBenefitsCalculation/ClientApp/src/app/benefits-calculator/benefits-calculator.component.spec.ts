import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BenefitsCalculatorComponent } from './benefits-calculator.component';
import { HttpClient } from '@angular/common/http';

describe('BenefitsCalculatorComponent', () => {

  let component: BenefitsCalculatorComponent;
  let fixture: ComponentFixture<BenefitsCalculatorComponent>;

  function getBaseUrl() {
    return 'path';
  }

  beforeEach(() => {

    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientModule,
        BrowserModule,
      ],
      declarations: [BenefitsCalculatorComponent],
      providers: [
        HttpClient,
        { provide: 'BASE_URL', useFactory: getBaseUrl, deps: []},
      ],
      schemas: [NO_ERRORS_SCHEMA],
    });

    fixture = TestBed.createComponent(BenefitsCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();     
    
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('validateModel', () => {

    it('should set validation error and employee validation error when employee name is missing', () => {

      // arrange
      component.employee = {
        name: null,
        spouse: null,
        dependents: []
      }

      // act
      component.validateModel();

      // assert
      expect(component.validationError).toBeTruthy();
      expect(component.employeeValidationError).toBeTruthy();
    });

    it('should set validation error and spouse validation error when spouse name is missing', () => {

      component.spouseCovered = true;
      component.employee = {
        name: 'some name',
        spouse: { name: null },
        dependents: []
      }

      component.validateModel();

      expect(component.validationError).toBeTruthy();
      expect(component.spouseValidationError).toBeTruthy();

    });

    it('should set validation error and dependent validation error when dependent name is missing', () => {

      component.spouseCovered = true;
      component.employee = {
        name: 'some name',
        spouse: { name: 'some other name' },
        dependents: [{ name: null }]
      }

      component.validateModel();

      expect(component.validationError).toBeTruthy();
      expect(component.dependentValidationError).toBeTruthy();

    });

    it('should set validation error and employee validation error to false when employee name present', () => {

      component.validationError = true;
      component.employeeValidationError = true;

      component.employee = {
        name: 'some name',
        spouse: { name: 'some other name' },
        dependents: [{ name: 'depepdent name' }]
      }

      component.validateModel();

      expect(component.validationError).toBeFalsy();
      expect(component.employeeValidationError).toBeFalsy();     

    });

    it('should set validation error and spouse validation error to false when spouse name present', () => {

      component.validationError = true;
      component.spouseValidationError = true;

      component.employee = {
        name: 'some name',
        spouse: { name: 'some other name' },
        dependents: [{ name: 'depepdent name' }]
      }

      component.validateModel();

      expect(component.validationError).toBeFalsy();
      expect(component.spouseValidationError).toBeFalsy();

    });

    it('should set validation error and dependent validation error to false when dependent name present', () => {

      component.validationError = true;
      component.dependentValidationError = true;

      component.employee = {
        name: 'some name',
        spouse: { name: 'some other name' },
        dependents: [{ name: 'depepdent name' }]
      }

      component.validateModel();

      expect(component.validationError).toBeFalsy();
      expect(component.dependentValidationError).toBeFalsy();
    });

  });


});
