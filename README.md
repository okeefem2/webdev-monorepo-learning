# Example app built following <https://www.udemy.com/course/complete-guide-to-building-an-app-with-net-core-and-react/>

Setting up as a mono repo with nrwl nx tools. This mono repo will include a couple other example react apps, as well as a nestjs backend mirroring the dotnet core backend. I will attempt to fit the dotnet core app into the monorepo as well, we will see how it goes

Need to be on at least node v 10.13.0
Set up using the command `npx create-nx-workspace reactivity` with an empty workspace backed by nx cli `npm install -g @nrwl/cli`

Then added these dev packages `npm install --save-dev @nrwl/react` and `npm install --save-dev @nrwl/nest`

Then `nx generate @nrwl/react:application booking` `nx generate @nrwl/react:application admin` -- no routing for this

`nx serve booking` `nx test booking` `nx e2e booking` can be used

`nx affected:dep-graph` shows a vis of the projects dependency graph

To create a shared lib `nx generate @nrwl/workspace:library common`

To run e2e tests `nx e2e todos-e2e --watch`

`nx g @nrwl/nest:app api --frontendProject=todos` By default this will be added to the apps folder

To generate code using nestjs schematics `npm install -g @nestjs/schematics`
Then `nx generate @nestjs/schematics:controller todos --source-root apps/api/src --path app`
