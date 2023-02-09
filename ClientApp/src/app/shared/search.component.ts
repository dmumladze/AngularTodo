import { Component, Input } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
    selector: 'term-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent {

	@Input()
	term: string;    

	constructor(private searchService: SearchService) {
	}

	public search() {
		this.searchService.notifyTermChange(this.term);
    }

}
