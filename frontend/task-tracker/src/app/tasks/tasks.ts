import { Injectable } from '@angular/core';
import { Api } from '../services/api';

export interface Task {
  id?: string;
  title: string;
  description?: string;
  priority: string;
  dueDate: string;
  isDone: boolean;
}

@Injectable({
  providedIn: 'root',
})

export class Tasks {
  constructor(private api: Api) {}

  getAll() {
    return this.api.get<Task[]>('/Tasks');
  }

  get(id: string) {
    return this.api.get<Task>(`/Tasks/${id}`);
  }

  create(task: Task) {
    return this.api.post('/Tasks', task);
  }

  update(id: string, task: Task) {
    return this.api.put(`/Tasks/${id}`, task);
  }

  delete(id: string) {
    return this.api.delete(`/Tasks/${id}`);
  }
}
