import { useEffect, useState } from 'react';

import { Movie } from '../models/Movie';
import { deleteMovie, getMovies } from '../api/movieServices';
import MovieForm from './MovieForm';
import { getDirectors } from '../api/directorServices';
import type { Director } from '../models/Director';


export const EMPTY_MOVIE: Movie = {
    id: 0,
    name: "",
    releaseYear: "",
    gender: "",
    duration: "",
    fkdirector: 0,
};

const MovieList = () => {
    const [movies, setMovies] = useState<Movie[]>([]);
    const [Directors, setDirectors] = useState<Director[]>([]);
    const [currentMovie, setCurrentMovie] = useState<Movie>(EMPTY_MOVIE);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<string | null>(null);

    useEffect(() => {
        getMovies()
            .then(setMovies)
            .catch(err => setError("Error loading movies: " + err.message));


        getDirectors()
            .then(setDirectors)
            .catch(err => console.error("Error loading directors:", err));
    }, []);

    const handleDelete = async (id: number) => {
        try {
            await deleteMovie(id);
            setMovies(movies.filter(m => m.id !== id));
        } catch (err: any) {
            setError("Error deleting movie: " + err.message);
        }
    };


    const OpenCreateModal = () => {

        setCurrentMovie(EMPTY_MOVIE);
        setSuccess(null);
        setError(null);
    };

    const OpenEditModal = (movie:Movie) => {

       setCurrentMovie(movie)
        setSuccess(null);
        setError(null);
    };


    return (
        <div className="container mt-4">
            <h2 className="text-primary mb-3">Movies</h2>

            {error && <div className="alert alert-danger">{error}</div>}
            <div className="d-flex justify-content-end mb-3">
                <button
                    type="button"
                    className="btn btn-success"
                    data-bs-toggle="modal"
                    data-bs-target="#movieModal"
                    onClick={() => OpenCreateModal()}
                >
                    Add Movie
                </button>
            </div>
            <table className="table table-striped table-hover">
                <thead className="table-dark">
                    <tr>
                        <th>Name</th>
                        <th>Release Year</th>
                        <th>Duration</th>
                        <th>Director</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {movies.map(m => (
                        <tr key={m.id}>
                            <td>{m.name}</td>
                            <td>{m.releaseYear}</td>
                            <td>{m.duration}</td>
                            <td>{m.fkdirectorNavigation?.name}</td>
                            <td>
                                <button data-bs-toggle="modal"
                                    data-bs-target="#movieModal" className="btn btn-warning btn-sm me-2" onClick={() => OpenEditModal(m)}>Edit</button>

                                <button
                                    className="btn btn-danger btn-sm"
                                    onClick={() => handleDelete(m.id)}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div className="modal fade" id="movieModal" tabIndex={-1} aria-hidden="true">
                <div className="modal-dialog modal-lg modal-dialog-centered">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">
                                {currentMovie === null || currentMovie?.id === 0 ? "Add Movie" : "Edit Movie"}
                            </h5>

                            <button
                                type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                            ></button>
                        </div>
                        <div className="modal-body">
                            <MovieForm
                                setCurrentMovie={setCurrentMovie}
                                currentMovie={currentMovie}
                                Directors={Directors}
                                setMovies={setMovies}
                                error={error}
                                success={success}
                                setError={setError}
                                setSuccess={setSuccess}
                            />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    );
};

export default MovieList;
