import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {  ClassificationResult } from '../models/classification-result';
import { Observable, of, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ClassificationService {

    constructor(private http: HttpClient) { }
    readonly  apiUrl : string  = `https://localhost:44336/api/classification`;

    classifyImage (formData : FormData) : Observable<ClassificationResult> {
        let header = new HttpHeaders();
        header.append('Content-Type', 'multipart/form-data');
        return this.http.post<ClassificationResult>(`${this.apiUrl}/classifyImage`, formData, {headers: header});
    }
}