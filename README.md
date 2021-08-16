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
