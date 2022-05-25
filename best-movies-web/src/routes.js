import Home from './Home/Home.svelte';
import MovieDetail from './Movies/MovieDetail.svelte';
import NotFound from './Misc/NotFound.svelte';
import SearchResult from './Search/SearchResult.svelte';
import MovieComments from './Movies/MovieComments.svelte';
import SignUp from './Authentication/SignUp.svelte';
import SignIn from './Authentication/SignIn.svelte';

export default {
    "/": Home,
    "/movie/:id": MovieDetail,
    "/search/:filter": SearchResult,
    "/movie/comments/:movie_id": MovieComments,
    "/signup/": SignUp,
    "/signin/": SignIn,
    // If route not recognized:
    "*": NotFound
}