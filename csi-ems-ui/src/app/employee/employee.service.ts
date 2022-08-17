import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Employee } from './employee.model';

const apiUrl = 'http://localhost:5001/api';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpClient: HttpClient) { }

  getEmployees() {
    return this.httpClient.get<any>(apiUrl);
  }

  getEmployee(id: string){
    return this.httpClient.get<Employee>(apiUrl, 
      { params: {'id': id }});
  }

  createEmployee(newEmployee: Employee) {
    return this.httpClient.post<Employee>(apiUrl, newEmployee);
  }

  updateEmployee(id: string, updatedEmployee: Employee){
    return this.httpClient.put<Employee>(apiUrl, updatedEmployee,
      { params: {'id': id }});
  }

  deleteEmployee(id: string){
    return this.httpClient.delete(apiUrl, 
      { params: {'id': id }});
  }
  
}
