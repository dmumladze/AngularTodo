import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class SearchService {

	public termChanged: EventEmitter<string>;

	constructor() {    
		this.termChanged = new EventEmitter<string>();
	}

	public notifyTermChange(term: string) {
		this.termChanged.emit(term);
	}
}
