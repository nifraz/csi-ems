import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Employee } from '../employee.model';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  list: Employee[] = [];

  constructor(private employeeService: EmployeeService, 
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.onFetch();
  }

  onFetch() {
    this.spinner.show();
    this.employeeService.getEmployees()
      .subscribe({
        next: value => {
          this.spinner.hide();
          this.list = value;
        },
        error: (err: HttpErrorResponse) => {
          this.spinner.hide();
          this.toastr.error(`Employee data retrieve failed. ${err.message}`, 'ERROR');
        },
      });
  }

  onDelete(id: string){
    if (confirm('Are you sure you want to delete this record?')) {
      this.employeeService.deleteEmployee(id)
        .subscribe({
          next: value => {
            this.spinner.hide();
            this.toastr.success('Employee record deleted.', 'SUCCESS');
            this.onFetch();
          },
          error: (err: HttpErrorResponse) => {
            this.spinner.hide();
            this.toastr.error(`Employee delete failed. ${err.message}`, 'ERROR');
          },
        });
    }
  }
}
