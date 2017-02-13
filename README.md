# Toy Robot

Toy Robot implementation using .NET Core (Specification from Question 2 posed by OP here: https://groups.google.com/forum/#!topic/happy-programming/O0-lg6yn1F0). Lots of other implementations for comparison (https://github.com/search?l=C%23&o=desc&q=toy+robot&s=updated&type=Repositories).

run:
```{r, engine='bash', count_lines}
$ dotnet restore
$ dotnet test
$ echo -e "PLACE 0,1,NORTH\nMOVE\nREPORT\n" | dotnet run
```

output:
```
0,2,NORTH
```

For self-contained deployment remove the `"type": "platform"` part from the frameworks section of the project.json file.

Then publish with:

```{r, engine='bash', count_lines}
$ dotnet publish -c release -r ubuntu.15.10-x64
```
And to run (from within the `bin/release/netcoreapp1.1/publish/` directory):

```{r, engine='bash', count_lines}
$ sudo chmod +x ./PassInTableDimensions.dll
$ echo -e "PLACE 3,4,NORTH\nMOVE\nREPORT\n" | ./PassInTableDimensions.dll 5 7
```

output:
```
3,5,NORTH
```

(Reference at https://docs.microsoft.com/en-us/dotnet/articles/core/deploying/index).

For .NET Core download (https://www.microsoft.com/net/download/core).

Introduction to using `dotnet` the .NET Core CLI tool (https://docs.microsoft.com/en-us/dotnet/articles/core/tutorials/using-with-xplat-cli).

For an IDE Rider is currently easier to use over Visual Studio Code, (though both took more work than I was prepared to do to actually get building/running working through the IDE).

Using Moq (https://github.com/Moq/moq4/wiki/Quickstart) and XUnit (http://blog.benhall.me.uk/2008/01/introduction-to-xunit/).
