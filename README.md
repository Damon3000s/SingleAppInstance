# ktsu.SingleAppInstance

## Overview

The `SingleAppInstance` class provides a mechanism to ensure that only one instance of an application is running at a time. This is useful for applications that should not have multiple instances running simultaneously, such as desktop applications or services.

## Features

- **Single Instance Enforcement**: Ensures that only one instance of the application is running.
- **PID File Management**: Uses a PID file to track the running instance.
- **Race Condition Handling**: Handles potential race conditions when starting multiple instances simultaneously.

## Installation

To install the `ktsu.SingleAppInstance` package, run the following command in a console:

```bash
dotnet add package ktsu.SingleAppInstance
```

## Getting Started

To get started with `ktsu.SingleAppInstance`, follow these steps:

1. Install the package using the command above.
2. Add the necessary `using` directive to your code.
3. Call the `ExitIfAlreadyRunning` method at the start of your application.

## Usage

To use the `SingleAppInstance` class, call the `ExitIfAlreadyRunning` method at the start of your application. This method will check if another instance is already running and exit the current instance if so.

```csharp
using ktsu.SingleAppInstance;

class Program
{
    static void Main(string[] args)
    {
        SingleAppInstance.ExitIfAlreadyRunning();

    // Your application code here
    }
}
```

Or if you prefer to explicitly handle the case where another instance is already running, you can call the `ShouldLaunch()` method to check if another instance is running.

```csharp
using ktsu.SingleAppInstance;

class Program
{
    static void Main(string[] args)
    {
        if (SingleAppInstance.ShouldLaunch())
        {
            // Your application code here
        }
        else
        {
            // Handle the case where another instance is already running
            Console.WriteLine("Another instance is already running.");
            Environment.Exit(1);
        }
    }
}
```

## Methods

### `ExitIfAlreadyRunning`

Exits the application if another instance is already running.

### `ShouldLaunch`

Determines whether the application should launch.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

## Support

If you encounter any issues or have questions, please open an issue on GitHub.

## License

This project is licensed under the MIT License.
