import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { Project } from '../shared/Project';
import { Todo } from '../shared/Todo';
import { TodoService } from './todo.service';

@Component({
    selector: 'todo-list',
    templateUrl: './todo-list.component.html',
    styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit, AfterViewInit {

    private fragmentsSubscription: Subscription;

    @Input()
    public todos: Todo[] = [];

    @Input()
    project: Project

    @Input()
    color: string = "yellow"

    @Input()
    projectTitle: string

    public todoTitle: string;

    @Input()
    eventsSubject: Subject<string>;

    constructor(private todoService: TodoService, private route: ActivatedRoute) {
        this.eventsSubject = new Subject<string>();
    }

    ngOnInit(): void {
        this.route.data.subscribe(
            (data: any) => {
                let todos = data['todos'] as Todo[];
                let sorted = todos.sort((a, b) => a.createdDate < b.createdDate ? 0 : -1);
                this.todos = sorted;
            },
            error => console.error(error)
        );
    }

    ngAfterViewInit(): void {
        if (!this.fragmentsSubscription) {
            this.fragmentsSubscription = this.route.fragment.subscribe(
                fragment => {
                    if (fragment)
                        this.eventsSubject.next(fragment);
                }
            );
        }
    }

    public create() {
        if (this.todoTitle && this.todoTitle.trim()) {
            let todo = Object.assign(new Todo(), {
                title: this.todoTitle,
                createdDate: new Date(),
                projectId: this.project?.id
            });
            this.todoService.create(todo).subscribe(
                data => {
                    this.todos.unshift(data as Todo);
                    this.todoTitle = "";
                    console.log(`Created ${data.id}`);
                },
                error => {
                    console.error(error)
                }
            );
        }
    }

    public update(todo: Todo) {
        this.todoService.update(todo).subscribe(
            data => {
                console.log(`Updated ${data.id}`);
            },
            error => {
                console.error(error)
            }
        );
    }

    public delete(todo: Todo) {
        this.todoService.delete(todo.id).subscribe(
            data => {
                console.log(`Deleted ${todo.id}`);
            },
            error => {
                console.error(error);
            }
        );
        let index = this.todos.findIndex(x => x.id == todo.id);
        if (index > -1) {
            this.todos.splice(index, 1);
        }
    }

    public sortByDueDate() {
        this.applySort((a, b) => a.dueDate < b.dueDate ? 0 : -1);
    }

    public sortByRemindDate() {
        this.applySort((a, b) => a.remindDate < b.remindDate ? 0 : -1);
    }

    public sortByCreationDate() {
        this.applySort((a, b) => a.createdDate < b.createdDate ? 0 : -1);
    }

    public sortAlphabetically() {
        this.applySort((a, b) => a.title < b.title ? -1 : 0);
    }

    private applySort(comparer: (a: any, b: any) => number) {
        let sorted = this.todos.sort(comparer);
        this.todos = sorted;
    }
}
