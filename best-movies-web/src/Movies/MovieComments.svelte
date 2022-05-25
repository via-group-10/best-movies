<script>
    export let movie;
    let comments;

    onMount(async () => {
            const res = await fetch(url + endpoint);
            comments = await res.json();
        })
</script>

<div class="container">
    <div class="row">
        <div class="col-md-2">
            <div class="movie-picture"></div>
        </div>
        <div class="col-md-10">
            <row>
                <div class="col-md-12">
                  <h4>Title</h4>
                </div>
            </row>
            <div class="row">
                <div class="col-md-12">
                    <h3>User Comments</h3>
                    
                </div>
            </div>
        </div>
    </div>
</div>

<style>
    .movie-picture {
            display: block;
            background-color: blanchedalmond;
            aspect-ratio: 16/9;
        }
</style>