# Scripts

Following after the [`Github "Scripts to Rule Them All" pattern`](https://github.com/github/scripts-to-rule-them-all), scripts have been wired up.

Why? Good question! - Helps solve the problem of "how do you set things up? How do you do X?" by externalizing it to a scripted source of truth. So, that's nice. (Plus, hey - it's versioned!)

| Script | Description |
| - | - |
| [`_helpers`](./_helpers) | Shared tid bits for consistency throughout the following scripts to handle logging, version checking, etc. |
| [`build`](./build) | Builds the thing, so it can be shipped and run! |
| [`cibuild`](./cibuild) | Composed by the above, this is meant to run through all the necessary checks and balances. |
| [`server`](./server) | Spins up the project for use/testing. |
