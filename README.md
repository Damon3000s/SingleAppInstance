# ktsu.SingleAppInstance

## Overview

The `SingleAppInstance` class provides a mechanism to ensure that only one instance of an application is running at a time. This is useful for applications that should not have multiple instances running simultaneously, such as desktop applications or services.

## Features

- **Single Instance Enforcement**: Ensures that only one instance of the application is running.
- **Enhanced Process Identification**: Uses detailed process information (PID, process name, start time, main module filename) to accurately identify running instances.
- **PID File Management**: Stores process information in JSON format in the application data directory.
- **Race Condition Handling**: Handles potential race conditions when starting multiple instances simultaneously with a timeout mechanism.
- **Backward Compatibility**: Maintains compatibility with older versions that only stored the PID.

## Installation

To install the `ktsu.SingleAppInstance` package, run the following command:

```bash
dotnet add package ktsu.SingleAppInstance
```

## Usage

### Basic Usage

To use the `SingleAppInstance` class, call the `ExitIfAlreadyRunning` method at the start of your application:

```csharp
using ktsu.SingleAppInstance;

class Program
{
    static void Main(string[] args)
    {
        SingleAppInstance.ExitIfAlreadyRunning();
        
        // Your application code here
        Console.WriteLine("Application is running.");
    }
}
```

### Custom Launch Logic

If you prefer to explicitly handle the case where another instance is already running:

```csharp
using ktsu.SingleAppInstance;

class Program
{
    static void Main(string[] args)
    {
        if (SingleAppInstance.ShouldLaunch())
        {
            // Your application code here
            Console.WriteLine("Application is running.");
        }
        else
        {
            // Handle the case where another instance is already running
            Console.WriteLine("Another instance is already running.");
        }
    }
}
```

## Methods

- **`ExitIfAlreadyRunning()`**: Exits the application with a status code of 0 if another instance is already running.
- **`ShouldLaunch()`**: Determines whether the application should launch based on whether another instance is running. It also handles writing the current process information to a PID file and implements a waiting mechanism to handle potential race conditions.

## Technical Implementation

Under the hood, SingleAppInstance:

1. Stores a JSON-serialized `ProcessInfo` object containing:
   - Process ID
   - Process name
   - Start time
   - Main module filename

2. Uses the application data directory to store the PID file. The file is named `.SingleAppInstance.pid`.

3. When checking for running instances, it:
   - Reads the PID file
   - Verifies if a process with the stored ID exists
   - Confirms it's the same application by comparing process name and executable path
   - Handles access restrictions and various edge cases

4. Implements a 1-second timeout in `ShouldLaunch()` to detect race conditions when multiple instances start simultaneously.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

## Support

If you encounter any issues or have questions, please open an issue on GitHub.

## License

This project is licensed under the MIT License.
