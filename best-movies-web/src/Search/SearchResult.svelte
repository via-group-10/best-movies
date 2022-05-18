<script>
    import { onMount } from "svelte";

    onMount(async () => {
        const res = await fetch('http://localhost:5252/api/movies?title={}')
        movies = await res.json();
    })
</script>