# Example app built following <https://www.udemy.com/course/complete-guide-to-building-an-app-with-net-core-and-react/>

## JS/TS MonoRepo

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

## Dotnet Core Project

To add projects to the sln file:

`dotnet sln add Domain` etc.

To add references to other projects
`cd Application/ && dotnet add reference ../Domain/ && dotnet add reference ../Persistence/`

`dotnet run -p API` to run from cli or `dotnet run` from the API folder then <http://localhost:5000> is where it is served

`dotnet ef migrations add MigrationName -p Persistance -s API` specifies the persistence and startup projects

`dotnet ef database update -p Persistence -s API`

### Section 3 Notes

The course uses Create React App to create the react app, I am using nx.

nx apps use main.tsx instead of index.tsx for bootstrapping

background knowledge/terms to know:

* TS, TSX/JSX
* Boot strapping
* Webpack

React uses one way binding to the virtual dom then to the dom versus two way binding
^ look into this some more

We install axios for use in http requests <https://github.com/axios/axios>

### Section 4 notes

Setting up db, postgres with docker

docker run -d --name reactivity -p 5432:5432 -e POSTGRES_PASSWORD=reactivitydev postgres:11.5
Using 11.5 because 12 has a bug with typeorm :(

docker exec -it reactivity psql -U postgres -c "create database reactivity"
docker exec -it reactivity psql -U postgres -c "create database reactivitynest"

Setting up connection with nestjs

npm install --save @nestjs/typeorm typeorm pg

Set up activities module

nx generate @nestjs/schematics:module activities --source-root apps/api/src --path app
nx generate @nestjs/schematics:controller activities --source-root apps/api/src --path app
nx generate @nestjs/schematics:service activities --source-root apps/api/src --path app

<https://dev.to/thisdotmedia/how-to-fly-with-nest-js-development-ggi>
<https://dev.to/nestjs/advanced-nestjs-dynamic-providers-1ee> dynamic providers

Seeding data

* cd reactivity/apps/api/migrations && npx typeorm migration:create --name SEED_DATA
* npx typeorm migration:run

CQRS Command/Query separation

<https://martinfowler.com/bliki/CQRS.html>

Using an event store for commands:
stores commands as events to be replayed as queries

Mediatr: object in => processing => object out

Use <https://snippet-generator.app/> to create snippets from code!

in Persistence project
dotnet ef migrations add "Added UserActivity" -s ../API

After upgrading to version 3 this did not work, I needed to run dotnet tool install --global dotnet-ef

and then added dotnet tools to path <https://stackoverflow.com/questions/56862089/cannot-find-command-dotnet-ef> in ~/.bash_profile

dotnet ef database update -s ../API
