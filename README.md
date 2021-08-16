# DemoApp
Generic business system template

## ADRs
### Option and avoiding null checks
Consider a rule in static code analysis that fails the artifact build when a try
block is encountered. 

Pay close attention to exceptions caught; strive towards external dependencies
being the sole exception originators.

Our business cases should be understood to an extent
where they don't result in exceptions, but a faulted result. Stick to option return, unless you can't.

This ties in to error propagation strategy, where infrastructure errors are not the concern of the application.

Exampl: in the context of the application Core being called by through a Web API: a controller should be introduced
to catch exceptions propagating that the application did not originate in its assembly.
"The application is not at fault".
Then the consumer can decide on logging strategy, depending on their hooks, orchestration across features etc.

## No explicit configuration for QUIC / HTTP 3 despite support by the underlying framework
Since the standard is still in draft, and the way the application is consumed can be either in-cluster or exposed publicly,
leave it to the cluster gateway / load balancer/ reverse proxy to utilize the protocol for platform performance gains.

If needed, the protocol can be used for communication with the app, but it's a concern of the configuration module, 
and the rest of the application should not be affected by it, unless the standard introduces breaking changes to concepts known to controllers.
