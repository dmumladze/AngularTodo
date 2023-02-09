import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Project } from '../shared/Project';
import { ProjectService } from './project.service';

@Injectable({
    providedIn: 'root'
})
export class ProjectDetailsResolver implements Resolve<Project[]> {

    constructor(private projectService: ProjectService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Project[] | Observable<Project[]> | Promise<Project[]> {
        return this.projectService.getOne(+route.params['id']);
    }
   
}
