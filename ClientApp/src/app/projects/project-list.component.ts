import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SearchService } from '../services/search.service';
import { Project } from '../shared/Project';
import { ProjectService } from './project.service';

@Component({
    selector: 'project-list',
    templateUrl: './project-list.component.html',
    styleUrls: ['./project-list.component.css']
})
export class ProjectsComponent implements OnInit, AfterViewInit, OnDestroy
{

	private fragmentSub: Subscription;
	private searchSub: Subscription;

    public projectTitle: string;
    public projects: Project[] = [];

    @ViewChild('container') container: ElementRef;

	constructor(private projectService: ProjectService, private route: ActivatedRoute, private searchService: SearchService) {
    }

    ngOnInit(): void {
        this.route.data.subscribe(
            (data: any) => {
                let projects = data['projects'] as Project[];
                let sorted = projects.sort((a, b) => a.createdDate < b.createdDate ? 0 : -1);
                this.projects = sorted;
            },
            error => console.error(error)
		);
		if (!this.searchSub) {
			this.searchSub = this.searchService.termChanged.subscribe(
				(term: string) => {
					this.search(term);
				}
			);
		}
    }

    ngAfterViewInit(): void {
        if (!this.fragmentSub) {
            this.fragmentSub = this.route.fragment.subscribe(
                fragment => {
                    if (!fragment) {
                        return;
                    }
                    let project = this.projects.filter(x => x.id.toString() === fragment)
                    if (project) {
                        console.log(fragment);
                        let table = this.container.nativeElement as HTMLTableElement
                        let row = table.rows.namedItem(fragment);
                        row?.classList.add('project-line-flash');
                        setTimeout(() => row?.classList.remove('project-line-flash'), 1000);
                    }
                }
            );
        }
	}

	ngOnDestroy(): void {
		if (this.fragmentSub) {
			this.fragmentSub.unsubscribe();
		}
		if (this.searchSub) {
			this.searchSub.unsubscribe();
		}
	}

	private search(term: string) {
		this.projectService.search(term).subscribe(
			data => {
				let sorted = data.sort((a, b) => a.createdDate < b.createdDate ? 0 : -1);
				this.projects = sorted;
			},
			error => {
				console.error(error)
			}
		);
	}

    public create() {
        if (this.projectTitle && this.projectTitle.trim()) {
            let project = Object.assign(new Project(), {
                title: this.projectTitle,
                createdDate: new Date()
            });
            this.projectService.create(project).subscribe(
                data => {
                    this.projects.unshift(data as Project);
                    this.projectTitle = "";
                    console.log(`Created ${data.id}`);
                },
                error => {
                    console.error(error)
                }
            );
        }
    }

    public update(project: Project) {
        this.projectService.update(project).subscribe(
            data => {
                console.log(`Updated ${data.id}`);
            },
            error => {
                console.error(error)
            }
        );
    }

    public delete(id: number) {
        this.projectService.delete(id).subscribe(
            data => {
                console.log(`Deleted ${id}`);
            },
            error => {
                console.error(error);
            }
        );
        let index = this.projects.findIndex(x => x.id == id);
        if (index > -1) {
            this.projects.splice(index, 1);
        }
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
        let sorted = this.projects.sort(comparer);
        this.projects = sorted;
    }
}
