# FireStationDispatcher.exe

## How to run call dispatcher project

#### Source has been provided alongside a pre-built Debug and Release versions of the software. To run the pre-built Release version, keep reading.
* Navigate to `firestationcalldispatcher/FireStationCallDispatcher/bin/Release/net472` in a terminal
* Run the `FireStationCallDispatcher.exe` with an employees JSON file. A valid employees JSON has been provided in the `firestationcalldispatcher/FireStationCallDispatcher/data` directory. 
* Example: 
```
C:\...\firestationcalldispatcher\FireStationCallDispatcher\bin\Release\net472>FireStationCallDispatcher.exe ..\..\..\data\employees.json
```

## How to run call dispatcher test project

#### Only source has been provided for the test application, as it has been designed to compile to a dll for simplicity. To load and run the test project, keep reading.
* Navigate to `firestationcalldispatcher/` in a file explorer.
* Double click the `FireStationCallDispatcher.sln` to open the solution in Visual Studio (hopefully you have it installed)
* Navigate to the test explorer and click "Run All Tests In View"

## Dependencies

* This C# console application depends on Newtonsoft.Json - for reading JSON data - and Moq (only the test project) - for mocking during tests.
* Dependencies can be installed via the NuGet package manager but they have also been included alongside the source in the packages/ directory.

## Git

* I used git for version control during this project. I have left the files in alongside the source so you can browse my commit history with `git log`. It is not the most tidy, but this project was time limited.

## Decisions

* I decided to use static methods wherever a state was not required, for example with factories and logging. The overhead of instantiating a class could be avoided with the static classes.
* I decided to seperate employees into different lists held in a map and accessed with the appropriate Seniority enum. This leads to extra O(n) processing when all employees need to be gathered into one big group, but this is fairly infrequent. What was far more common was a request of one employee of a specific type. By pre-sorting the employees by seniority, accessing an employee of a specific rank became an O(1) operation because of the map.
* I thought it was a little ambiguous whether the calls should have been passed to the program via the command line, or another script should have sent messages to the call dispatch program while it was running to deliver messages. Both of these seemed unlikely because of the complexity and the time provided. I decided to generate the calls within the program to allow more control and dynamism of the number of calls generated. All the call information is tracked within each `Call` object and I believe the logging provides an insight into the relevant parts of this object as it moves through the system.
* I decided to make use of Interfaces for the EmployeeManager and CallManager classes. This was mainly to aid testing by allowing these classes to be mocked and therefore monitored more closely in the tests
* With more time I would expand the testing in this project. I strongly believe that testing is essential to aid avoidance of regression bugs alongside enabling an engineer to isolate code for testing, increasing development speed.