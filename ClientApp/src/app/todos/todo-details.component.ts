import { Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Project } from '../shared/Project';
import { Todo } from '../shared/Todo';

@Component({
    selector: 'todo-details',
    templateUrl: './todo-details.component.html',
    styleUrls: ['./todo-details.component.css'],
})
export class TodoDetailsComponent implements OnInit, OnDestroy  {

    private eventsSubscription: Subscription;

    @Input()
    events: Observable<string>;

    @Input()
    color: string

    @Input()
    todo: Todo;

    @Output()
    onUpdate: EventEmitter<Todo> = new EventEmitter<Todo>();

    @Output()
    onDelete: EventEmitter<Todo> = new EventEmitter<Todo>();

    @ViewChild('container') containerDiv: ElementRef;

    ngOnInit(): void {
        if (!this.eventsSubscription) {
            this.eventsSubscription = this.events.subscribe(value => {
                if (value === this.todo.id.toString()) {
                    console.log(value);
                    let div = this.containerDiv.nativeElement as HTMLDivElement
                    div?.classList.add('sticky-content-wiggle');
                    setTimeout(() => div?.classList.remove('sticky-content-wiggle'), 1000);
                }
            });
        }
    }

    ngOnDestroy(): void {
        this.eventsSubscription.unsubscribe();
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
        this.onUpdate.emit(this.todo);
    }

    public delete() {
        this.onDelete.emit(this.todo);
    }
    
}
