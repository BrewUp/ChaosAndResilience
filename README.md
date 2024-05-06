# Evolutionary Architectures
An evolutionary architecture supports guided, incremental change as the first principle across multiple dimensions.

## What are Fitness Functions
Architectural goals and constraints may all change independently of functional expectations.  
Fitness functions describe how close an architecture is to achieving an architectural aim.  
During test-driven development we write tests to verify that features conform to desired business outcomes.  
With fitness function-driven development we can also write tests that measure a system’s alignment to architectural goals.

Architecture, like business capability and infrastructure, can be expressed in code through the use of appropriate fitness functions, as demonstrated in this example.  
What are the benefits of expressing architecture through the code and driving development with fitness functions? 
> - Fitness function-driven development objectively measures technical debt and drives code quality.
> - Fitness function-driven development can inform coding choices for interfaces, events, and APIs related to downstream processes
> - Fitness function-driven development communicates architectural standards as code and empowers development teams to deliver features that are aligned with architectural goals.
> - With architecture goals expressed as code, conformance tests can be incorporated in build pipelines to monitor alignment with the architectural “-ilities” that are most critical.