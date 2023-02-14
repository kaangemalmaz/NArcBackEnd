# N-tier .Net Core Base Project 
## _Layers_
- Core: It is a generic layer that allows tools to be used in other projects.
- Entities: This layer contains database table classes and dto' classes created based on these classes.
- DataAccess: This layer where we do database operations.
- Business: This layer where we develop our business rules.
- WebAPI: This layer is the layer where we provide restful services communication.
 
## _Features_
- Repository Design Pattern
- Entity Framework Core Orm
- Background service for schedule jobs
- Autofac 
- Authentication(with Claim Extension)
- Json Web Token 
- Business Rules Const.
- Hashing and Verify hash
- IoC Container - External
- Result Const.
- Custom Extensions Middleware (Centralization)
- Cross cutting concerns
- Aspect Oriented Programing
- Memory Cache
- Fluent Validation
- Unit Of work

## _Principles_
- Solid
- DRY (Dont Repeat Yourself)
