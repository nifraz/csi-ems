import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Employee } from '../employee.model';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {
  employeeId!: string;
  employee: Employee = {
    id: '',
    name: '',
    email: '',
    dateOfBirth: new Date(),
    sex: '',
    designation: '',
    phone: '',
    address: '',
  };

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private employeeService: EmployeeService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params) => {
        this.employeeId = params['id'];
        this.spinner.show();
        this.employeeService.getEmployee(this.employeeId)
        .subscribe({
          next: value => {
            this.spinner.hide();
            this.employee = value;
            //this.toastr.info('Employee data retrieved.', 'INFO');
          },
          error: (err: HttpErrorResponse) => {
            this.spinner.hide();
            this.toastr.error(`Employee data retrieve failed. ${err.message}`, 'ERROR');
          },
        });
      }
    );
  }

  onDelete(){
    if (confirm('Are you sure you want to delete this record?')) {
      this.employeeService.deleteEmployee(this.employeeId)
        .subscribe({
          next: () => {
            console.log('succ');
            this.spinner.hide();
            this.toastr.success('Employee record deleted.', 'SUCCESS');
            this.router.navigate(['/employees']);
          },
          error: (err: HttpErrorResponse) => {
            console.log(err);
            this.spinner.hide();
            this.toastr.error(`Employee delete failed. ${err.message}`, 'ERROR');
          },
        });
    }
  }
}
