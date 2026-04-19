import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js'; 

import MoviePages from './pages/MoviePages.tsx';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <MoviePages/>
  </StrictMode>,
)
