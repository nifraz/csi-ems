import { Component, OnInit } from '@angular/core';
import { formatDate } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Employee } from '../employee.model';
import { EmployeeService } from '../employee.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent implements OnInit {
  isEditMode = false;
  employeeId!: string;
  employee!: Employee;
  modelForm!: FormGroup;
  designations = ["Manager", "Accountant", "Sales Rep"]

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private employeeService: EmployeeService,
    private formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params) => {
        this.employeeId = params['id'];
        this.isEditMode = this.employeeId != null;
        this.initForm();
      }
    );
  }

  initForm() {
    this.modelForm = this.formBuilder.group(
      {
        'id': [{ value: '(auto generated)', disabled: true }],
        'name': [null, [Validators.required, Validators.maxLength(128)]],
        'email': [null, [Validators.required, Validators.maxLength(128), Validators.email]],
        'dateOfBirth': [formatDate(new Date(), 'yyyy-MM-dd', 'en')],
        'sex': ['Male'],
        'designation': [null],
        'phone': [null, [Validators.pattern('[- +()0-9]+')]],
        'address': [null],
      }
    );

    if (this.isEditMode) {
      this.spinner.show();
      this.employeeService.getEmployee(this.employeeId)
        .subscribe({
          next: value => {
            this.spinner.hide();
            this.employee = value;
            this.modelForm.setValue(
              {
                'id': this.employee.id,
                'name': this.employee.name,
                'email': this.employee.email,
                'dateOfBirth': formatDate(this.employee.dateOfBirth, 'yyyy-MM-dd', 'en'),
                'sex': this.employee.sex,
                'designation': this.employee.designation,
                'phone': this.employee.phone,
                'address': this.employee.address,
              }
            );
            this.toastr.info('Employee data retrieved.', 'INFO');
          },
          error: (err: HttpErrorResponse) => {
            this.spinner.hide();
            this.toastr.error(`Employee data retrieve failed. ${err.message}`, 'ERROR');
          },
        });
    }
  }

  onSubmit() {
    const newEmployee: Employee = this.modelForm.value;
    newEmployee.dateOfBirth = new Date(newEmployee.dateOfBirth);

    this.spinner.show();
    if (this.isEditMode) {
      this.employeeService.updateEmployee(this.employeeId, newEmployee)
        .subscribe({
          next: value => {
            this.spinner.hide();
            this.toastr.success('Employee data added.', 'SUCCESS');
            this.onCancel();
          },
          error: (err: HttpErrorResponse) => {
            this.spinner.hide();
            this.toastr.error(`Employee update failed. ${err.message}`, 'ERROR');
          },
        });

    }
    else {
      this.employeeService.createEmployee(newEmployee)
        .subscribe({
          next: value => {
            this.spinner.hide();
            this.toastr.success('Employee data updated.', 'SUCCESS');
            this.router.navigate(['/employees', value.id]);
          },
          error: (err: HttpErrorResponse) => {
            this.spinner.hide();
            this.toastr.error(`Employee update failed. ${err.message}`, 'ERROR');
          },
        });
    }
  }

  onCancel() {
    this.router.navigate(['../'], { relativeTo: this.activatedRoute });
  }

  get name() { return this.modelForm.get('name'); }
  get email() { return this.modelForm.get('email'); }
  get phone() { return this.modelForm.get('phone'); }

}
