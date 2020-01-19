# Repository Service
This is basically an interface over other repositories and implements the Repository Pattern via Rest methods (Get, Post, Put, Delete).  I have toyed with implementing patch, but I am not sure how that will look.  Only two underlying repositories are LiteDB and Mongo, but I would like to eventually do Postgres and Couch.

1. To Install in your project: `Install-Package Kamabery.Slackers.Services.Repository -Version 1.0.0`
2. See Sample projects for setup.