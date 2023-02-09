import { formatDate } from '@angular/common';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class ReminderService {

    private hub: HubConnection;    
    private toastr: ToastrService;    

    constructor(toastr: ToastrService) {
        this.toastr = toastr;        
    }

    public connectToHubs = () => {
        this.hub = new HubConnectionBuilder()
            .withUrl('http://localhost:5252/hub')
            .withAutomaticReconnect()
            .build();        
        this.hub.start()
            .then(() => {
                console.log('Hub connection started.');
            })
            .catch(error => {
                console.error(`Error starting hub connection: ${error}`);
            });
    }

    public startHubListeners = () => {
        this.hub.on('remindTodo', (data) => {
            let due  = data.dueDate || data.remindDate;
            let date = formatDate(due, 'MMM dd, yyyy', 'en-US');
            let time = formatDate(due, 'hh:mm a', 'en-US');
            this.toastr.info(`<a class='reminderLink' href='/todos#${data.id}'>Due on ${date} at ${time}</a>`,
                `To-do: ${data.title}`, {
                tapToDismiss: true,
                closeButton: true,
                newestOnTop: true,
                disableTimeOut: true,
                enableHtml: true,
                positionClass: 'toast-bottom-center'
            });
        });
        this.hub.on('remindProject', (data) => {
            let due  = data.dueDate || data.remindDate;
            let date = formatDate(due, 'MMM dd, yyyy', 'en-US');
            let time = formatDate(due, 'hh:mm a', 'en-US');
            this.toastr.info(`<a class='reminderLink' href='/projects#${data.id}'>Due on ${date} at ${time}</a>`,
                `Project: ${data.title}`, {
                tapToDismiss: true,
                closeButton: true,
                newestOnTop: true,
                disableTimeOut: true,
                enableHtml: true,
                positionClass: 'toast-bottom-center'
            });
        });
        this.hub.on('remindProjectTodo', (data) => {
            let due = data.dueDate || data.remindDate;
            let date = formatDate(due, 'MMM dd, yyyy', 'en-US');
            let time = formatDate(due, 'hh:mm a', 'en-US');
            this.toastr.info(`<a class='reminderLink' href='/projects/${data.projectId}#${data.id}'>Due on ${date} at ${time}</a>`,
                `Project To-do: ${data.title}`, {
                tapToDismiss: true,
                closeButton: true,
                newestOnTop: true,
                disableTimeOut: true,
                enableHtml: true,
                positionClass: 'toast-bottom-center'
            });
        });
    }
}
