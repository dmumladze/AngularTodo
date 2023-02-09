import { Todo } from "./Todo";

export class Project {
    id: number;
    title: string;
    description: string;
    createdDate: Date;
    dueDate: Date;
    remindDate: Date;
    completedDate: Date;   
    todos: Todo[];
}
