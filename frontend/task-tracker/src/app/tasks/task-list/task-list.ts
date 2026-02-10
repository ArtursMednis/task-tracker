import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Task, Tasks } from '../tasks';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css',
})
export class TaskList implements OnInit {
  tasks: Task[] = [];

  constructor(
    private tasksService: Tasks,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.tasksService.getAll().subscribe(tasks => {
      this.tasks = tasks;
      this.cdr.detectChanges();
    });
  }

  delete(id: string) {
    this.tasksService.delete(id).subscribe(() => this.load());
  }
}
