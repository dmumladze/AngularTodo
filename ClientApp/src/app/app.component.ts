import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReminderService } from './services/reminder.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
 
    constructor(private reminder: ReminderService, private router: Router)
    {
    }

    ngOnInit(): void {
        this.reminder.connectToHubs();
        this.reminder.startHubListeners();
    }

    @HostListener('document:click', ['$event'])
    public handleClick(event: Event): void {
        let element = event.target as HTMLAnchorElement;
        if (!element || element.className !== 'reminderLink') {
            return;
        }
        event.preventDefault();
        let route = element?.getAttribute('href');
        if (route) {
            let idIndex = route.indexOf('#');
            let id = route.substring(idIndex + 1, route.length);
            let routeIndex = route.indexOf('/');
            let routeUrl = route.substring(routeIndex, idIndex);
            this.router.navigate([routeUrl], { fragment: id });
        }
    }
}
