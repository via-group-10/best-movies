export function getMovies(filter) {
    if  (filter === undefined)
    {
        filter = {
            limit: 15,
            offset: 0
        }
    }
    if (filter.limit === undefined) 
        filter.limit = 10;
    if (filter.offset === undefined) 
        filter.offset = 0;

    const url = "BestMoviesApiUrl";
    const endpoint = `/movies?limit=${filter.limit}&offset=${filter.offset}${filter.title ? "&title=" + filter.title : ""}`;

    const request = {
        method: "get",
        headers: { 'Authorization': window.localStorage.getItem('authToken') }
    };

    return fetch(url + endpoint, request);
}

export function getMovie(movieId) {
    if (movieId === undefined)
        return Promise.reject("no movie id provided");
    const url = "BestMoviesApiUrl";
    const endpoint = `/movies/${movieId}`;

    const request = {
        method: "get",
        headers: { 'Authorization': window.localStorage.getItem('authToken') }
    };

    return fetch(url + endpoint, request);
}