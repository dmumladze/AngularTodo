import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Project } from '../shared/Project';
import { ProjectService } from './project.service';

@Component({
    selector: 'project-details',
    templateUrl: './project-details.component.html',
    styleUrls: ['./project-details.component.css'],
})
export class ProjectDetailsComponent implements OnInit {

    @Input()
    project: Project;

    @Output()
    onUpdate: EventEmitter<Project> = new EventEmitter<Project>();

    @Output()
    onDelete: EventEmitter<Project> = new EventEmitter<Project>();

    constructor(private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.route.data.subscribe(
            (data: any) => {
                let project = data['project'] as Project;
                this.project = project;
            },
            error => console.error(error)
        );
    }

    public get dueMinDate(): Date {
        let date = new Date();
        date.setSeconds(0);
        date.setMinutes(date.getMinutes() + 1);        
        return date;
    }

    public get remindMinDate(): Date {
        let date = new Date();
        date.setSeconds(0);
        date.setMinutes(date.getMinutes() + 1);        
        return date;
    }

    public update() {        
        this.onUpdate.emit(this.project);
    }

    public delete() {
        this.onDelete.emit(this.project);
    }
    
}
