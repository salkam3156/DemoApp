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

### Controllers as a plugin-type module
Controllers assembly references framework-specific assemblies.
Still, by detaching it from web host configuration, we facilitate switching the assembly
to one containing controllers with view support etc. through a simple configuration change.

### Development module
For ease of development - in the early stages - a strong coupling to a development module
was introduced in the configuration / web host core.
This allows for faking out infrastructure / DB dependencies.
Removing the coupling requires web host configuration code changes, but they are limited 
to one small assembly.

### Breaking the explicit dependencies principle for sake of module isolation
Application services should not reference each other explicitly for sake of isolation / modularity.
In-process messaging is used in place of explicit references to enforce modularity by design.
This is to allow separating out application part into a module if needed - for the purposes of scaling,
performance reasons, technology change that future may bring given some limitations encountered.

## Road forward

If this is to be both a demo and a template, repositories should not map 1-1 to CRUD operations. Need to make domain richer to be able to think up / demonstrate some sensical use cases.

Application Queries and Commands should percolate up to controller level, and basically be the Action params.
In such a way, the application functionality is naturally documented for the API consumer.
Controller action maps to a feature invoked 1-1.
Some clients don't support that (GET requests basically requiring a body - REST levels of maturity and other such lofty discussions), so that needs to be kept in mind.
In such a way, reasoning about the use cases from the Client perspective should be a matter of calling specific features without much choreography
(save for when a location header etc. are returned for asynchronous operations).

Swagger Codegen -> code around 1-1 feature calls, as exposed by API -> Done.

Domain (still called core from before service layer emerged with the concept of Notification) could be split by intoducing the concept of a SaleItem, and divorcing it from Product.

Features could be split into services referencing their respective domains afterwards.
There's coupling between Product and Sale.
Solution: in or out-of process mediation could map between the domain models
and Sales features would never know of Product being a concept at all.

Splitting these off into separate assemblies, deployable as separate deployment units, is estimated to be Hour time cost.

Failure result should be made more robust, aggregating failures along the way / call stack. Logging can be a function-currying like invocation/aspect
over failure result retrieval to make logging transparent to developers extending functionality ("logging can't be skipped , because it's inherent in the design").

## General guidelines and ponderances
Each feature does this:

Asks for something through mediation. Who fulfills this demand ? No one cares.

Extracts the result out.

Decides how to respond, as it understands the implication of the result for the application.

If a method in the service layer does anything more than that - there might be something wrong in the new feature being introduced.

Say, if we need to compose an email to provide a response - add a service for email composition and pass to notifier implementaion etc.

For noticably CPU and/or memory bound operations we assume application being run on a single thread, and having limited memory / being expected to respond quickly.

If heavy processing is to be done, we consider if this is not a case for acknowledgement for furute results promise as opposed to immediate handling. 
Assume asynchrony and low compute capabilities untill this is not an option.

We should pass a resource location as a promise of work done at external dependencies behest. 
Track and notify through client notifier. Which notifier is it ? Framework where the app is glued together with dependencies will decide.

If long-running processing fails - it fails. We should assume that we don't need to wait for that failue, if it's the de facto response to the user, and our in-process flow has finished (waiting for results etc.).
If a feature is demanded to be executed via synchronous call, then that's what's needed, and we should provide it (eg. client has no capability of hooking into the notifier implementation).

 Out-of-cluster services must have retry logic (RegionalTaxProvider etc.)
