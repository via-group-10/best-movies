<script>
  import { push } from "svelte-spa-router";
  import { User } from "../store";
  import { signin } from "../api";

  let username;
  let password;

  const submit = async () => {
    let res = await signin(username, password);
    let response = await res.json()
     
    if (res.ok) {
      User.signin(response.authToken);
      push("/");
    }
  };
</script>

<div class="container-fluid pt-3">
  <div class="row">
    <div class="col-12">
      <main class="form-signin">
        <form on:submit|preventDefault={submit}>
          <h1 class="h3 mb-3 fw-normal">Please sign in</h1>

          <div class="form-floating">
            <input
              bind:value={username}
              type="username"
              class="form-control"
              id="floatingInput"
              placeholder="Name"
              style="background-repeat: no-repeat; background-attachment: scroll; background-size: 16px 18px; background-position: 98% 50%;"
              autocomplete="off"
              required
            />
            <label for="floatingInput">Username</label>
          </div>
          <div class="form-floating">
            <input
              type="password"
              bind:value={password}
              class="form-control"
              id="floatingPassword"
              placeholder="Password"
              style="background-repeat: no-repeat; background-attachment: scroll; background-size: 16px 18px; background-position: 98% 50%;"
              autocomplete="off"
              required
            />
            <label for="floatingPassword">Password</label>
          </div>
          <button class="w-100 btn btn-lg btn-primary" type="submit">
            Sign in
          </button>
        </form>
      </main>
    </div>
  </div>
</div>

<style>
  .form-signin {
    width: 100%;
    max-width: 330px;
    padding: 15px;
    margin: auto;
  }
</style>
