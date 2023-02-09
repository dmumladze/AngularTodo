import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from '../shared/Project';

@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    public getAll(): Observable<Project[]> {
        return this.http.get<Project[]>(this.baseUrl + 'api/v1/projects');
    }

    public getOne(id: number): Observable<Project[]> {
        return this.http.get<Project[]>(this.baseUrl + `api/v1/projects/${id}`);
    }

    public create(project: Project): Observable<Project> {
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/json');
        return this.http.post<Project>(this.baseUrl + `api/v1/projects`, project, { headers: headers });
    }

    public update(project: Project): Observable<Project> {
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/json');
        return this.http.put<Project>(this.baseUrl + `api/v1/projects/${project.id}`, project, { headers: headers });
    }

    public delete(id: number): Observable<Object> {
        return this.http.delete(this.baseUrl + `api/v1/projects/${id}`);
	}

	public search(term: string): Observable<Project[]> {
		let data = { term: term };
		return this.http.get<Project[]>(this.baseUrl + 'api/v1/projects/search', { params: data });
	}
}
