import {
    NgxMatDateFormats,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,
    NgxMatTimepickerModule,
    NGX_MAT_DATE_FORMATS
} from '@angular-material-components/datetime-picker';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExtraOptions, RouterModule } from '@angular/router';
import { NgsContenteditableModule } from '@ng-stack/contenteditable';
import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ProjectDetailsComponent } from './projects/project-details.component';
import { ProjectDetailsResolver } from './projects/project-details.resolver';
import { ProjectsComponent } from './projects/project-list.component';
import { ProjectListResolver } from './projects/project-list.resolver';
import { TodoDetailsComponent } from './todos/todo-details.component';
import { TodoListComponent } from './todos/todo-list.component';
import { TodoListResolver } from './todos/todo-list.resolver';

const INTL_DATE_INPUT_FORMAT = {
    year: 'numeric',
    month: 'numeric',
    day: 'numeric',
    hourCycle: 'h12',
    hour: '2-digit',
    minute: '2-digit',
};

const MAT_DATE_FORMATS: NgxMatDateFormats = {
    parse: {
        dateInput: INTL_DATE_INPUT_FORMAT,
    },
    display: {
        dateInput: INTL_DATE_INPUT_FORMAT,
        monthYearLabel: { year: 'numeric', month: 'short' },
        dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
        monthYearA11yLabel: { year: 'numeric', month: 'long' },
    },
};

const routerOptions: ExtraOptions = {
    scrollPositionRestoration: 'enabled',
    anchorScrolling: 'enabled',
    scrollOffset: [0, 64],
    onSameUrlNavigation: 'ignore',
};

@NgModule({
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
    ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        TodoDetailsComponent,
        TodoListComponent,
        ProjectDetailsComponent,
        ProjectsComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'todos', component: TodoListComponent, resolve: { todos: TodoListResolver } },
            { path: 'projects', component: ProjectsComponent, resolve: { projects: ProjectListResolver } },
            { path: 'projects/:id', component: ProjectDetailsComponent, resolve: { project: ProjectDetailsResolver } }
        ], routerOptions),
        NgsContenteditableModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatFormFieldModule,
        MatInputModule,
        NgxMatDatetimePickerModule,
        NgxMatTimepickerModule,
        NgxMatNativeDateModule,
        MatMenuModule,
        MatIconModule,
        ToastrModule.forRoot({
            preventDuplicates: true,
            countDuplicates: true,
            includeTitleDuplicates: true            
        })
    ],
    providers: [
        { provide: NGX_MAT_DATE_FORMATS, useValue: MAT_DATE_FORMATS }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
