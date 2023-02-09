import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../shared/Todo';

@Injectable({
    providedIn: 'root'
})
export class TodoService {

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    public getAll(): Observable<Todo[]> {
        return this.http.get<Todo[]>(this.baseUrl + 'api/v1/todos');
    }

    public create(todo: Todo): Observable<Todo> {
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/json');
        return this.http.post<Todo>(this.baseUrl + `api/v1/todos`, todo, { headers: headers });
    }

    public update(todo: Todo): Observable<Todo> {
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/json');
        return this.http.put<Todo>(this.baseUrl + `api/v1/todos/${todo.id}`, todo, { headers: headers });
    }

    public delete(id: number): Observable<Object> {
        return this.http.delete(this.baseUrl + `api/v1/todos/${id}`);
	}

	public search(term: string, projectId: number): Observable<Todo[]> {
		let data = { term: term, projectId: projectId || '' };
		return this.http.get<Todo[]>(this.baseUrl + 'api/v1/todos/search', { params: data });
	}
}
