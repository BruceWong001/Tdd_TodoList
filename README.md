# TodoApi Solution

This solution is a simple Todo application written in C#. It uses a memory-based repository for storing todo items and provides APIs for managing these items. The solution is divided into several projects:

## TodoApi

This is the main project of the solution. It contains the `TodoItem` class which represents a todo item with properties like `Id`, `StartDate`, `EndDate`, and `Title`. It also contains the `IRepository` interface which defines the contract for a repository that can save and retrieve todo items. The `MemoryRepository` class is an implementation of this interface that uses an in-memory list to store the items. The `ItemCollection` class is a collection of todo items that uses an `IRepository` to save and retrieve items.

## TodoApi.test

This project contains unit tests for the `TodoApi` project. It uses the xUnit testing framework and Moq for mocking dependencies. The tests cover various scenarios like adding a new item, retrieving all items, updating an existing item, and removing an item.

## TodoService.test

This project contains unit tests for the service layer of the application. It tests the functionality of the `ItemCollection` class and the `TodoItem` class.

## TodoAPIService.test.cs

This project contains integration tests for the API layer of the application. It uses the `WebApplicationFactory` class to create a test server and send HTTP requests to the API.

## Files

- `TodoApi/TodoItem.cs`: Contains the `TodoItem` class, `IRepository` interface, `ItemCollection` class, and `MemoryRepository` class.
- `TodoApi.test/memoryrepository.cs`: Contains unit tests for the `MemoryRepository` class.
- `TodoApi.test/TodoAPIService.test.cs`: Contains integration tests for the API layer.
- `TodoApi.test/todoservice.cs`: Contains unit tests for the service layer.

## How to Run

To run the application, you need to have .NET 6.0 or later installed. You can run the application using the `dotnet run` command from the `TodoApi` project directory. To run the tests, you can use the `dotnet test` command from the solution directory.

## How to Contribute

To contribute to this project, you can create a fork of the repository, make your changes, and then submit a pull request. Please make sure to add unit tests for any new functionality and ensure that all existing tests pass before submitting your pull request.


# TodoApi 解决方案

这个解决方案是一个简单的Todo应用，使用C#编写。它使用基于内存的仓库来存储待办事项，并提供了管理这些事项的API。解决方案被划分为几个项目：

## TodoApi

这是解决方案的主项目。它包含了`TodoItem`类，这个类代表了一个待办事项，有`Id`、`StartDate`、`EndDate`和`Title`等属性。它还包含了`IRepository`接口，这个接口定义了一个可以保存和检索待办事项的仓库的契约。`MemoryRepository`类是这个接口的一个实现，它使用一个内存列表来存储事项。`ItemCollection`类是一个使用`IRepository`来保存和检索事项的待办事项集合。

## TodoApi.test

这个项目包含了`TodoApi`项目的单元测试。它使用xUnit测试框架和Moq来模拟依赖。测试覆盖了各种场景，如添加新事项、检索所有事项、更新现有事项和移除事项。

## TodoService.test

这个项目包含了应用的服务层的单元测试。它测试了`ItemCollection`类和`TodoItem`类的功能。

## TodoAPIService.test.cs

这个项目包含了应用的API层的集成测试。它使用`WebApplicationFactory`类来创建一个测试服务器并向API发送HTTP请求。

## 文件

- `TodoApi/TodoItem.cs`：包含了`TodoItem`类、`IRepository`接口、`ItemCollection`类和`MemoryRepository`类。
- `TodoApi.test/memoryrepository.cs`：包含了`MemoryRepository`类的单元测试。
- `TodoApi.test/TodoAPIService.test.cs`：包含了API层的集成测试。
- `TodoApi.test/todoservice.cs`：包含了服务层的单元测试。

## 如何运行

要运行应用，你需要安装.NET 5.0或更高版本。你可以使用`dotnet run`命令从`TodoApi`项目目录运行应用。要运行测试，你可以从解决方案目录使用`dotnet test`命令。

## 如何贡献

要对这个项目做出贡献，你可以创建一个仓库的分支，做出你的改变，然后提交一个拉取请求。请确保为任何新功能添加单元测试，并确保在提交你的拉取请求前所有现有测试都通过。
