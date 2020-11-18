import { Component, OnInit } from '@angular/core';
import { ClassificationResult } from '../../models/classification-result';
import { ClassificationService } from '../../services/classification.service';
import { FormGroup, FormBuilder } from '@angular/forms';
@Component({
  selector: 'app-classification',
  templateUrl: './classification.component.html',
  styleUrls: ['./classification.component.css']
})
export class ClassificationComponent implements OnInit {

  constructor(private classificationService : ClassificationService,
    private formBuilder: FormBuilder) { }
  public classificationResult : ClassificationResult; 
  uploadForm: FormGroup;  

  ngOnInit() {
    this.uploadForm = this.formBuilder.group({
      profile: ['']
    });
  }
  fileChange(files: File[]){
    let file = files[0];
    this.uploadForm.get('profile').setValue(file);
    const formData = new FormData();
    formData.append('file', this.uploadForm.get('profile').value);
    //formData.append("file", file);   
//    let file = event.target.files[0];
    this.classificationService
        .classifyImage(formData)
        .subscribe(
          data =>{
            this.classificationResult = data;
          }
        );
  }

}
