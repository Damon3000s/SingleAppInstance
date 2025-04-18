# ktsu.SingleAppInstance

## Overview

The `SingleAppInstance` class provides a mechanism to ensure that only one instance of an application is running at a time. This is useful for applications that should not have multiple instances running simultaneously, such as desktop applications or services.

## Features

- **Single Instance Enforcement**: Ensures that only one instance of the application is running.
- **PID File Management**: Uses a PID file to track the running instance.
- **Race Condition Handling**: Handles potential race conditions when starting multiple instances simultaneously.

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

You can also customize the launch condition:

```csharp
using ktsu.SingleAppInstance;

class Program
{
    static void Main(string[] args)
    {
        SingleAppInstance.ShouldLaunch = () => !File.Exists("custom.lock");
        if (!SingleAppInstance.ShouldLaunch())
        {
            Console.WriteLine("Another instance is already running.");
            return;
        }

        Console.WriteLine("Application is running.");
    }
}
```

## Methods

- **`ExitIfAlreadyRunning()`**: Exits the application if another instance is already running.
- **`ShouldLaunch()`**: Determines whether the application should launch based on whether another instance is running.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

## Support

If you encounter any issues or have questions, please open an issue on GitHub.

## License

This project is licensed under the MIT License.
