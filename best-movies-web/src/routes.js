import Home from './Home/Home.svelte';
import MovieDetail from './Movies/MovieDetail.svelte';
import NotFound from './Misc/NotFound.svelte';
import SearchResult from './Search/SearchResult.svelte';
import SignUp from './Authentication/SignUp.svelte';
import SignIn from './Authentication/SignIn.svelte';
import FavoriteMovies from './Favorites/FavoriteMovies.svelte';

export default {
    "/": Home,
    "/movie/:id": MovieDetail,
    "/search/:filter": SearchResult,
    "/signup": SignUp,
    "/signin": SignIn,
    "/myfavorites": FavoriteMovies,
    // If route not recognized:
    "*": NotFound
}