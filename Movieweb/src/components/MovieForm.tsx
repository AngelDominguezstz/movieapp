import React from "react";
import { Movie } from "../models/Movie";
import { createMovie, updateMovie } from "../api/movieServices";
import { Director } from "../models/Director";

interface MovieFormProps {
    setCurrentMovie: React.Dispatch<React.SetStateAction<Movie>>;
    currentMovie: Movie;
    Directors: Director[];
    setMovies: React.Dispatch<React.SetStateAction<Movie[]>>;
    success: string | null;
    error: string | null;
    setSuccess: React.Dispatch<React.SetStateAction<string | null>>;
    setError: React.Dispatch<React.SetStateAction<string | null>>;
}
const MovieForm: React.FC<MovieFormProps> = ({ setCurrentMovie, currentMovie, Directors, setMovies, setSuccess, setError, error, success }) => {


    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setCurrentMovie({ ...currentMovie, [name]: value });
    };


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            let saved: Movie;
            if (currentMovie.id !== 0) {
                saved = await updateMovie(currentMovie);
                setSuccess(`Movie "${saved.name}" updated successfully!`);
                setError(null);

            } else {
                saved = await createMovie(currentMovie);
                setSuccess(`Movie "${saved.name}" created successfully!`);
                setError(null);
            }



            setMovies(prev => {
                const exists = prev.find(m => m.id === saved.id);
                const director = Directors.find(d => d.id === saved.fkdirector);

                const movieWithDirector = { ...saved, fkdirectorNavigation: director };

                if (exists) {
                    return prev.map(m => m.id === saved.id ? movieWithDirector : m);
                } else {
                    return [...prev, movieWithDirector];
                }
            });
            setCurrentMovie(saved);
        } catch (err: any) {
            setError("Error saving movie: " + err.message);
            setSuccess(null);
        }
    };
    const handleChangeDirector = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setCurrentMovie({ ...currentMovie, [name]: value });
    };
    return (
        <div className="container mt-2">
            {error && (
                <div className="alert alert-danger alert-dismissible fade show" role="alert">
                    {error}
                    <button type="button" className="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
            )}

            {success && (
                <div className="alert alert-success alert-dismissible fade show" role="alert">
                    {success}
                    <button type="button" className="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
            )}


            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label className="form-label">Name</label>
                    <input
                        type="text"
                        name="name"
                        value={currentMovie.name}
                        onChange={handleChange}
                        className="form-control"
                        minLength={2}
                        maxLength={100}
                    />
                </div>

                <div className="mb-3">
                    <label className="form-label">Release Year</label>
                    <input
                        type="date"
                        name="releaseYear"
                        value={currentMovie.releaseYear || ""}
                        onChange={handleChange}
                        className="form-control"
                        required
                    />
                </div>

                <div className="mb-3">
                    <label className="form-label">Gender</label>
                    <input
                        type="text"
                        name="gender"
                        value={currentMovie.gender || ""}
                        onChange={handleChange}
                        className="form-control"
                        maxLength={50}
                    />
                </div>

                <div className="mb-3">
                    <label className="form-label">Duration</label>
                    <input
                        type="time"
                        name="duration"
                        value={currentMovie.duration || ""}
                        onChange={handleChange}
                        className="form-control"
                        required
                    />
                </div>

                <div className="mb-3">
                    <label className="form-label">Director</label>
                    <select
                        name="fkdirector"
                        value={currentMovie.fkdirector}
                        onChange={handleChangeDirector}
                        className="form-select"
                        required
                    >
                        <option value={0}>-- Select Director --</option>
                        {Directors.map(d => (
                            <option key={d.id} value={d.id}>
                                {d.name}
                            </option>
                        ))}
                    </select>
                </div>

                <button type="submit" className="btn btn-success">
                    Save Movie
                </button>
            </form>
        </div>
    );
};

export default MovieForm;
