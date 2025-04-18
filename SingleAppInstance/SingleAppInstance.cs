namespace ktsu.SingleAppInstance;

using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

using ktsu.AppDataStorage;
using ktsu.Extensions;
using ktsu.StrongPaths;

/// <summary>
/// Provides a mechanism to ensure that only one instance of an application is running at a time.
/// </summary>
public static class SingleAppInstance
{
	internal static AbsoluteDirectoryPath PidDirectoryPath { get; } = AppData.Path;
	internal static AbsoluteFilePath PidFilePath { get; } = PidDirectoryPath / $".{nameof(SingleAppInstance)}.pid".As<FileName>();

	/// <summary>
	/// Exits the application if another instance is already running.
	/// </summary>
	/// <remarks>
	/// This method checks if another instance of the application is already running by calling <see cref="ShouldLaunch"/>.
	/// If another instance is detected, the current application exits with a status code of 0.
	/// </remarks>
	public static void ExitIfAlreadyRunning()
	{
		if (!ShouldLaunch())
		{
			Environment.Exit(0);
		}
	}

	/// <summary>
	/// Determines whether the application should launch.
	/// </summary>
	/// <returns>
	/// <c>true</c> if the application should launch; otherwise, <c>false</c>.
	/// </returns>
	/// <remarks>
	/// This method checks if there is already an instance of the application running.
	/// If no other instance is running, it writes the current process ID to a PID file
	/// and waits for a short period to handle potential race conditions. It then checks
	/// again to ensure no other instance started during the wait period.
	/// </remarks>
	public static bool ShouldLaunch()
	{
		// if there is already an instance running, exit
		if (IsAlreadyRunning())
		{
			return false;
		}

		// if no other instance is running, write our pid to the pid file and wait to see
		// if another instance was attempting to start at the same time
		WritePidFile();
		Thread.Sleep(1000);

		// in case there was a race and another instance is starting at the same time we
		// need to check again to see if we won the lock
		return !IsAlreadyRunning();
	}

	/// <summary>
	/// Represents process information stored in the PID file.
	/// </summary>
	internal class ProcessInfo
	{
		/// <summary>
		/// Gets or sets the process ID.
		/// </summary>
		public int ProcessId { get; set; }

		/// <summary>
		/// Gets or sets the name of the process.
		/// </summary>
		public string? ProcessName { get; set; }

		/// <summary>
		/// Gets or sets the start time of the process.
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// Gets or sets the main module filename of the process.
		/// </summary>
		public string? MainModuleFileName { get; set; }
	}

	/// <summary>
	/// Checks if there is already an instance of the application running.
	/// </summary>
	/// <returns>
	/// <c>true</c> if another instance is running; otherwise, <c>false</c>.
	/// </returns>
	/// <remarks>
	/// This method reads the PID file to get the process information of the running instance.
	/// It then checks if the process with that ID is still running and verifies it's the same application.
	/// </remarks>
	internal static bool IsAlreadyRunning()
	{
		int currentPid = Environment.ProcessId;
		try
		{
			string pidFileContents = File.ReadAllText(PidFilePath);

			// Try to deserialize the JSON content
			ProcessInfo? storedProcess;
			try
			{
				storedProcess = JsonSerializer.Deserialize<ProcessInfo>(pidFileContents);
				if (storedProcess == null)
				{
					return false;
				}
			}
			catch (JsonException)
			{
				// Fallback for backward compatibility with older versions that only stored the PID
				if (int.TryParse(pidFileContents, CultureInfo.InvariantCulture, out int filePid))
				{
					// If it's the current process, allow it to run
					if (filePid == currentPid)
					{
						return false;
					}

					// Legacy check - just verify if the process is running
					return Process.GetProcesses().Any(p => p.Id == filePid);
				}

				return false;
			}

			// If the stored PID is the current process, allow it to run
			if (storedProcess.ProcessId == currentPid)
			{
				return false;
			}

			// Check if the process is still running
			try
			{
				var runningProcess = Process.GetProcessById(storedProcess.ProcessId);

				// Verify it's the same application by checking name and module filename
				if (runningProcess != null &&
					!runningProcess.HasExited &&
					string.Equals(runningProcess.ProcessName, storedProcess.ProcessName, StringComparison.Ordinal) &&
					runningProcess.MainModule != null &&
					string.Equals(runningProcess.MainModule.FileName, storedProcess.MainModuleFileName, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			catch (ArgumentException)
			{
				// Process not found - no longer running
			}
			catch (InvalidOperationException)
			{
				// Process has exited
			}
			catch (System.ComponentModel.Win32Exception)
			{
				// Access denied - handle more conservatively
				// If we can't access process details, we can't verify if it's our app
				// Let's be cautious and assume it might be our application

				try
				{
					var process = Process.GetProcessById(storedProcess.ProcessId);

					if (process != null && !process.HasExited)
					{
						// Process exists but we can't access its details
						// Check just the process name which is typically accessible
						if (string.Equals(process.ProcessName, storedProcess.ProcessName, StringComparison.Ordinal))
						{
							return true;
						}
					}
				}
				catch (ArgumentException)
				{
					// Process doesn't exist
				}
				catch (InvalidOperationException)
				{
					// Process has exited
				}
			}
		}
		catch (DirectoryNotFoundException)
		{
		}
		catch (FileNotFoundException)
		{
		}
		catch (FormatException)
		{
		}

		return false;
	}

	/// <summary>
	/// Writes the current process information to the PID file.
	/// </summary>
	/// <remarks>
	/// This method writes the current process information to the PID file in the application data path.
	/// </remarks>
	internal static void WritePidFile()
	{
		Directory.CreateDirectory(PidDirectoryPath);

		var currentProcess = Process.GetCurrentProcess();
		var processInfo = new ProcessInfo
		{
			ProcessId = currentProcess.Id,
			ProcessName = currentProcess.ProcessName,
			StartTime = currentProcess.StartTime,
			MainModuleFileName = currentProcess.MainModule?.FileName
		};

		string json = JsonSerializer.Serialize(processInfo);
		File.WriteAllText(PidFilePath, json);
	}
}
