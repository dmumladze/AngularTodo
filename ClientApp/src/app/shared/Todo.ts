
export class Todo {
    id: number;
    projectId: number;
    predecessorId: number;
    successorId: number;
    title: string;
    notes: string;
    createdDate: Date;
    dueDate: Date;
    remindDate: Date;
    completedDate: Date;   
}
