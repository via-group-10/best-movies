<script>
    import { onMount } from "svelte";
    export let movie;
    export let params;

    let id = params.id;
    const url = "BestMoviesApiUrl";
    const endpoint = `/movies?id=${id}`;

    onMount(async () => {
        const res = await fetch(url + endpoint);
        movie = await res.json();
    })
</script>


<div class="container">
    <div class="row" style="margin-top: 2rem;">
        <div class="col">
            <div class="card shadow-sm">
                <svg
                    ><title>Placeholder</title><rect
                        width="100%"
                        height="100%"
                        fill="#55595c"
                    /><text x="50%" y="50%" fill="#eceeef" dy=".3em"
                        >{id}</text
                    ></svg
                >

                <div class="card-body">
                    <p class="card-text">
                        This is a wider card with supporting text below as a
                        natural lead-in to additional content. This content is a
                        little bit longer.
                    </p>
                    <div
                        class="d-flex justify-content-between align-items-center"
                    >
                        <div class="btn-group">
                            <button
                                type="button"
                                class="btn btn-sm btn-outline-secondary"
                                >View</button
                            >
                            <button
                                type="button"
                                class="btn btn-sm btn-outline-secondary"
                                >Edit</button
                            >
                        </div>
                        <small class="text-muted">9 mins</small>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
