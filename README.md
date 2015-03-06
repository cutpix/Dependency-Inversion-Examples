# Dependency-Inversion-Examples

Concrete examples of using Dependency Inversion Principle (DIP), Service Location (SL), Dependency Injection (DI) and Inversion of Control Containers (IOC).
I will show now provide a number of very simple example programs to show how to use DIP, SL, DI, and IOC in practice. 

The applications that will be implemented in the examples supports a single user story:

A string of data can be registered and stored in a data store. All registrations and data store operations are logged, and stored data and the log is printed when program terminates.

All the examples will be implemented as console projects that has an Application class to register some data, a Data class for persisting data and a Logging class for logging the activities of the application.
All the example code is very simple in the sense that any issues regarding exception handling and thread safety are ignored.


## Example 1 – Basic Implementation
The first example shows how the sample application looks with a straight forward implementation. Of course applications so trivial as this does not really show why DIP, DI and IOC containers are good ideas, but it make it easy to provide simple and fully functional examples. One problem in example 1 in relation to DIP and DI, is that the Application class instantiates the DataStore by calling it’s constructor, and thus has a dependency DataStore class implementation. Another problem is that Logger class is accessed using static methods, which makes it unnecessary hard to test.

## Example 2 – Dependency Inversion Principle
To introduce some dependency inversion to example 1, we need to have the Application class depend on abstractions of the low level Data and Logger classes. These abstraction will be interfaces named IData and ILogger, and these abstractions will depend on any details, but details should depend on abstractions, which means that the Data class will depend on the ILogger abstraction to perform the logging. 
Finally, the static nature of Logger must be eliminated to be able to use an abstraction for Logger. The resulting code ends up as the following, where the main changes are, that IData and ILogger are introduced, and the static parts of Logger has been made non-static.

## Example 3 – Poor mans Service Locator
Service Location is often considered an anti-pattern, but sometimes it may be necessary to use, because of constraints in frameworks. Example 3 shows how a poor mans Service Locator can be made, and as we will see, it is merely an object providing dependencies as properties, and the service locator it self is then made available statically via the static Current property.

## Example 4 – Poor mans Dependency Injection Instead 
Although example 3 is an improvement to example 2, because the client code no longer has dependencies, a better alternative to Poor man’s Service Locator, is to use Poor mans dependency injection (PMDI).
The benefits of PMDI are that the dependencies are now clearly stated by the constructors of Application and DataStore, and the classes no longer need to depend on the Service Locator class.
Like example 3, the dependencies are expressed with the Interfaces, so that the Application and DataStore classes does not have any references to the concrete implementations of their dependencies, which means they are loosely coupled.

## Example 5 – Service Location with an IOC container 
To introduce the concept of an IOC container, we return to example 3 and replace the manual implementation of a ServiceLocator with an IOC container. There are many different IOC containers available (Unity, Autofac, Ninject and many more and the new Asp.Net MVC 6 comes with its own built in IOC container), but I use SimpleInjector because it is simple.
SimpleInjector is installed in the Example 5 project using nuget:
PM> install-package simpleinjector
And the ServiceLocator class is changed to be a wrapper around a SimpleInjector Container. This makes the ServiceLocator simpler by introducing a generic method, instead of a property for each dependency type, but also allow the object life time features of an IOC container to be used. Instead of making a Container.RegisterSingle<T> call in the Service locator which implies Singleton lifetime, one could also have used Container.Register to get transient lifetime, which means that a new instance is created every time the type is resolved.

## Example 6 – Dependency Injection with an IOC container
Going back to example 4, here is how to do dependency injection with an IOC container. Notice have the manual creation of the dependencies now have been replaced by registrations in the IOC container (SimpleInjector installed like in example 5) and application is now created by resolving it from the IOC container, which then takes care of auto wiring and injecting all constructor injected dependencies.

