import type { Director } from "./Director";

export class Movie {
    id: number;
    name: string;
    releaseYear?: string;
    gender?: string;
    duration?: string; 
    fkdirector?: number;
    fkdirectorNavigation?: Director;

    constructor() {
        this.id = 0;
        this.name = "";
    }
}

