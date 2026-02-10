import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Tasks } from '../tasks';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, ReactiveFormsModule],
  templateUrl: './task-form.html',
  styleUrl: './task-form.css',
})
export class TaskForm implements OnInit {
  taskId: string | null = null;
  form!: ReturnType<FormBuilder['group']>;

  constructor(
    private fb: FormBuilder,
    private tasks: Tasks,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      priority: [''],
      dueDate: [''],
      isDone: [false]
    });

    const id = this.route.snapshot.paramMap.get('id');
    
    if(id != "new"){
      this.taskId = id;
    }
    if (this.taskId) {
      this.tasks.get(this.taskId).subscribe(task => {
        this.form.patchValue(task);
      });
    }
  }

  submit() {
    const value = this.form.value as any;

    if (this.taskId) {
      this.tasks.update(this.taskId, value).subscribe(() => {
        this.router.navigate(['/tasks']);
      });
    } else {
      this.tasks.create(value).subscribe(() => {
        this.router.navigate(['/tasks']);
      });
    }
  }

  cancel() {
    this.router.navigate(['/tasks']);
  }
}
