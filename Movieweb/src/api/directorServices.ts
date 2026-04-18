import type { Director } from "../models/Director";

const API_URL = "https://localhost:7285/api/Director";

export const getDirectors = async (): Promise<Director[]> => {
  const response = await fetch(API_URL);
  if (!response.ok) {
    throw new Error("Error fetching movies");
  }
  return await response.json();
};
