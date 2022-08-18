import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from './employee.model';

const apiUrl = 'http://localhost:7000/api/employees';
// const apiUrl = 'https://localhost:44373/api/employees';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  
  constructor(private httpClient: HttpClient) { }

  getEmployees() {
    return this.httpClient.get<Employee[]>(apiUrl);
  }

  getEmployee(id: string){
    return this.httpClient.get<Employee>(`${apiUrl}/${id}`);
  }

  createEmployee(newEmployee: Employee) {
    return this.httpClient.post<Employee>(apiUrl, newEmployee);
  }

  updateEmployee(id: string, updatedEmployee: Employee){
    return this.httpClient.put<Employee>(`${apiUrl}/${id}`, updatedEmployee);
  }

  deleteEmployee(id: string){
    return this.httpClient.delete(`${apiUrl}/${id}`);
  }
  
}
