# Bad Monolith
The monolith itself is not a bad architecture or solution.

## Gall's Law
A complex system that works is invariably found to have evolved from a simple system that worked.  
The inverse proposition also appears to be true: A complex system designed from scratch never works and cannot be made to work.  
You have to start over, beginning with a working simple system

## Coupling
In software engineering, coupling is the degree of interdependence between software modules,  
a measure of how closely connected two routines or modules are.  
It measures the strength of the relationships between modules.

## Types of Coupling
### Procedural Programming
A module here refers to a subroutine of any kind, i.e. a set of one or more statements having a name and preferably its own set of variable names.
### Content coupling (high)
Content coupling is said to occur when one module uses the code of another module, for instance a branch.  
This violates information hiding – a basic software design concept.
### Common coupling
Common coupling is said to occur when several modules have access to the same global data.  
But it can lead to uncontrolled error propagation and unforeseen side-effects when changes are made.
### External coupling
External coupling occurs when two modules share an externally imposed data format, communication protocol, or device interface.  
This is basically related to the communication to external tools and devices.