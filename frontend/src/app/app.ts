import { Component, OnInit, inject, signal } from '@angular/core';
import { TodoService } from './services/todo.service';
import { Todo } from './models/todo';

@Component({
  imports: [],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App implements OnInit {
  private readonly todoService = inject(TodoService);

  protected readonly todos = signal<Todo[]>([]);
  protected readonly newTitle = signal('');

  ngOnInit(): void {
    this.loadTodos();
  }

  private loadTodos(): void {
    this.todoService.getAll().subscribe((todos) => this.todos.set(todos));
  }

  protected addTodo(): void {
    const title = this.newTitle().trim();
    if (!title) {
      return;
    }

    this.todoService.add(title).subscribe(() => {
      this.newTitle.set('');
      this.loadTodos();
    });
  }

  protected deleteTodo(id: number): void {
    this.todoService.delete(id).subscribe(() => this.loadTodos());
  }
}