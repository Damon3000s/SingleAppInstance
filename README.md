# ktsu.SingleAppInstance

> A .NET library that ensures only one instance of your application is running at a time.

[![License](https://img.shields.io/github/license/ktsu-dev/SingleAppInstance)](https://github.com/ktsu-dev/SingleAppInstance/blob/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/ktsu.SingleAppInstance.svg)](https://www.nuget.org/packages/ktsu.SingleAppInstance/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ktsu.SingleAppInstance.svg)](https://www.nuget.org/packages/ktsu.SingleAppInstance/)
[![Build Status](https://github.com/ktsu-dev/SingleAppInstance/workflows/build/badge.svg)](https://github.com/ktsu-dev/SingleAppInstance/actions)
[![GitHub Stars](https://img.shields.io/github/stars/ktsu-dev/SingleAppInstance?style=social)](https://github.com/ktsu-dev/SingleAppInstance/stargazers)

## Introduction

`ktsu.SingleAppInstance` is a lightweight .NET library that provides a robust mechanism to ensure only one instance of an application is running at a time. This is essential for desktop applications, services, or any software that requires instance exclusivity to prevent resource conflicts, maintain data integrity, or provide a consistent user experience.

## Features

- **Single Instance Enforcement**: Ensures only one instance of the application is running
- **Enhanced Process Identification**: Uses multiple attributes (PID, name, start time, executable path) for accurate identification
- **Race Condition Handling**: Manages potential conflicts when instances start simultaneously
- **PID File Management**: Stores process information securely in the application data directory
- **Cross-Platform Support**: Works on Windows, macOS, and Linux
- **Backward Compatibility**: Maintains compatibility with older versions that only stored the PID
- **Simple API**: Clean, easy-to-use interface with just two primary methods

## Installation

### Package Manager Console

```powershell
Install-Package ktsu.SingleAppInstance
```

### .NET CLI

```bash
dotnet add package ktsu.SingleAppInstance
```

### Package Reference

```xml
<PackageReference Include="ktsu.SingleAppInstance" Version="x.y.z" />
```

## Usage Examples

### Basic Example

The simplest way to use SingleAppInstance is to call the `ExitIfAlreadyRunning` method at the start of your application:

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
            
            // Optional: Activate the existing window, pass command-line arguments, etc.
        }
    }
}
```

## Advanced Usage

### WPF Application Integration

```csharp
using System.Windows;
using ktsu.SingleAppInstance;

namespace MyWpfApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            if (!SingleAppInstance.ShouldLaunch())
            {
                // Another instance is already running
                MessageBox.Show("Application is already running.");
                Shutdown();
                return;
            }
            
            // Continue with normal startup
            MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
```

### Console Application with Command Passing

For a more complex scenario where you want to pass commands to an existing instance:

```csharp
using System;
using ktsu.SingleAppInstance;

class Program
{
    static void Main(string[] args)
    {
        // Check if we can launch
        if (!SingleAppInstance.ShouldLaunch())
        {
            // Another instance is already running
            // Here you could implement an IPC mechanism to pass arguments to the running instance
            Console.WriteLine("Another instance is already running. Sending arguments...");
            
            // Example: Using named pipes, TCP, or memory-mapped files to communicate
            // SendArgsToRunningInstance(args);
            
            return;
        }
        
        // We're the primary instance
        Console.WriteLine("Application is running.");
        
        // Optional: Setup your IPC listening mechanism
        // SetupIpcListener();
        
        // Application main loop
        Console.ReadLine();
    }
}
```

## API Reference

### `SingleAppInstance` Class

The static class that provides instance management functionality.

#### Methods

| Name | Return Type | Description |
|------|-------------|-------------|
| `ExitIfAlreadyRunning()` | `void` | Exits the application with a status code of 0 if another instance is already running |
| `ShouldLaunch()` | `bool` | Determines whether the application should launch based on whether another instance is running |

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

Contributions are welcome! Here's how you can help:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please make sure to update tests as appropriate and adhere to the existing coding style.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
