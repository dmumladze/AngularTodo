import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Todo } from '../shared/Todo';
import { TodoService } from './todo.service';

@Injectable({
    providedIn: 'root'
})
export class TodoListResolver implements Resolve<Todo[]> {

    constructor(private todoService: TodoService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Todo[] | Observable<Todo[]> | Promise<Todo[]> {
        return this.todoService.getAll();
    }
   
}
