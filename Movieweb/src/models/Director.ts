import type { Movie } from "./Movie";

export class Director {
    id: number;
    name: string;
    nationality?: string;
    age?: number;
    active?: boolean;
    movies: Movie[];

    constructor() {
        this.id = 0;
        this.name = "";
        this.movies = [];
    }
}