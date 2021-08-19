# DemoApp
Generic business system template

## ADRs
### Result and avoiding null checks
Consider a rule in static code analysis that fails the artifact build when a try
block is encountered. 
There may be some exception propagataion introduced, where there should be none.

Pay close attention to exceptions caught; strive towards infrastructure dependencies
being the sole exception originators.

Our business cases should be understood to an extent
where they don't result in exceptions, but a faulted result. Stick to Result return, unless you can't.

### No explicit configuration for QUIC / HTTP 3 despite support by the underlying framework
Since the standard is still in draft, and the way the application is consumed can be either in-cluster or exposed publicly,
leave it to the cluster gateway / load balancer/ reverse proxy to utilize the protocol for platform performance gains.

If needed, the protocol can be used for communication with the app, but it's a concern of the configuration module, 
and the rest of the application should not be affected by it, unless the standard introduces breaking changes to concepts known to controllers.

### Error propagation strategy
Infrastructure errors are not the concern of the application.

Example: in the context of the application Core being called by through a Web API: a controller should be introduced
to catch exceptions propagating that the application did not originate in its assembly.
"The application is not at fault".
Then the consumer can decide on logging strategy, depending on their hooks, orchestration across features etc.

## Controllers as a plugin-type module
Controllers assembly references framework-specific assemblies.
Still, by detaching it from web host configuration, we facilitate switching the assembly
to one containing controllers with view support etc. through a simple configuration change.

## Development module
For ease of development - in the early stages - a strong coupling to a development module
was introduced in the configuration / web host core.
This allows for faking out infrastructure / DB dependencies.
Removing the coupling requires web host configuration code changes, but they are limited 
to one small assembly.

## Breaking the explicit dependencies principle for sake of module isolation
Application services should not reference each other explicitly for sake of isolation / modularity.
In-process messaging is used in place of explicit references to enforce modularity by design.
This is to allow separating out application part into a module if needed - for the purposes of scaling,
performance reasons, technology change that future may bring given some limitations encountered.

## Road forward
Doamin could be split by intoducing the concept of a SaleItem, and divorcing it from Product.

Features could be split into services referencing their respective domains afterwards.
There's coupling between Product and Sale.
Solution: in or out-of process mediation could map between the domain models
and Sales features would never know of Product being a concept at all.

Splitting these off into separate assemblies, deployable as separate deployment units, is estimated to be Hour time cost.

Failure result should be made more robust, aggregating failures along the way / call stack. Logging can be a function-currying like invocation/aspect
over failure result retrieval to make logging transparent to developers extending functionality ("logging can't be skipped , because it's inherent in the design").




