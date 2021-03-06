export function getMovies(filter) {
    if (filter === undefined) {
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

export function signin(name, password) {
    if (name == undefined || password == undefined)
        return Promise.reject("no user provided");

    const url = "BestMoviesApiUrl";
    const endpoint = "/Authentication/signin";

    const request = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            "username": name,
            "password": password
        }),
    }

    return fetch(url + endpoint, request);
}

export function signup(name, password) {
    if (name == undefined || password == undefined)
        return Promise.reject("no user provided");

    const url = "BestMoviesApiUrl";
    const endpoint = "/Authentication/signup";

    const request = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            "username": name,
            "password": password
        }),
    }

    return fetch(url + endpoint, request);
}

export function postComment(movieId, comment) {
    if (movieId == undefined || comment == undefined)
        return Promise.reject("no comment provided");

    const url = "BestMoviesApiUrl";
    const endpoint = `/movies/${movieId}/comments`;

    const request = {
        method: "POST",
        headers:
        {
            'Authorization': window.localStorage.getItem('authToken'),
            'Content-Type': "application/json"
        },
        body: JSON.stringify({
            "text": comment,
        }),
    };

    let finalUrl = url + endpoint;
    return fetch(finalUrl, request);
}

export function getFavoriteMovies() {
    const url = "BestMoviesApiUrl";
    const endpoint = `/movies/myfavorite`;

    const request = {
        method: "get",
        headers: { 'Authorization': window.localStorage.getItem('authToken') }
    };

    return fetch(url + endpoint, request);
}

export function addToFavorites(movieId) {
    const url = "BestMoviesApiUrl";
    const endpoint = `/movies/myfavorite/${movieId}`;

    const request = {
        method: "put",
        headers: { 'Authorization': window.localStorage.getItem('authToken') }
    };

    return fetch(url + endpoint, request);
}