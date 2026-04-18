import { Movie } from "../models/Movie";

const API_URL = "https://localhost:7285/api/Movie";

export const getMovies = async (): Promise<Movie[]> => {
  const response = await fetch(API_URL);
  if (!response.ok) {
    throw new Error("Error fetching movies");
  }
  return await response.json();
};

export const getMovieById = async (id: number): Promise<Movie> => {
  const response = await fetch(`${API_URL}/${id}`);
  if (!response.ok) {
    throw new Error("Error fetching movie by ID");
  }
  return await response.json();
};

export const createMovie = async (movie: Movie): Promise<Movie> => {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(movie),
  });
  if (!response.ok) {
    throw new Error("Error creating movie");
  }
  return await response.json();
};

export const updateMovie = async (movie: Movie): Promise<Movie> => {
  const response = await fetch(`${API_URL}/${movie.id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(movie),
  });
  if (!response.ok) {
    throw new Error("Error updating movie");
  }
  return await response.json();
};

export const deleteMovie = async (id: number): Promise<void> => {
  const response = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
  if (!response.ok) {
    throw new Error("Error deleting movie");
  }
};
