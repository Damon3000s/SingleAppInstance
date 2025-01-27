namespace ktsu.SingleAppInstance;

using System.Diagnostics;
using System.Globalization;
using ktsu.AppDataStorage;
using ktsu.Extensions;
using ktsu.StrongPaths;

/// <summary>
/// Provides a mechanism to ensure that only one instance of an application is running at a time.
/// </summary>
public static class SingleAppInstance
{
	internal static AbsoluteFilePath PidFilePath { get; } = AppData.Path / $".{nameof(SingleAppInstance)}.pid".As<FileName>();

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
	/// Checks if there is already an instance of the application running.
	/// </summary>
	/// <returns>
	/// <c>true</c> if another instance is running; otherwise, <c>false</c>.
	/// </returns>
	/// <remarks>
	/// This method reads the PID file to get the process ID of the running instance.
	/// It then checks if the process with that ID is still running.
	/// </remarks>
	internal static bool IsAlreadyRunning()
	{
		int currentPid = Environment.ProcessId;
		try
		{
			string pidFileContents = File.ReadAllText(PidFilePath);
			int filePid = int.Parse(pidFileContents, CultureInfo.InvariantCulture);
			if (filePid == currentPid)
			{
				//if the pid in the file is the same as the current pid, exit
				return false;
			}

			//is the process still running?
			if (Process.GetProcesses().Any(p => p.Id == filePid))
			{
				//if the process is still running, exit
				return true;
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
	/// Writes the current process ID to the PID file.
	/// </summary>
	/// <remarks>
	/// This method writes the current process ID to the PID file in the application data path.
	/// </remarks>
	internal static void WritePidFile() => File.WriteAllText(PidFilePath, Environment.ProcessId.ToString(CultureInfo.InvariantCulture));
}
