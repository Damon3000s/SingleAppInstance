// Disable parallel execution of tests to prevent file access conflicts
[assembly: Parallelize(Workers = 1, Scope = ExecutionScope.ClassLevel)]

namespace ktsu.SingleAppInstance.Test;

using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

[TestClass]
public class SingleAppInstanceTests
{
	[TestMethod]
	public void WritePidFile_ShouldCreateFileWithCurrentProcessInfo()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		if (File.Exists(pidFilePath))
		{
			File.Delete(pidFilePath);
		}

		try
		{
			// Act
			SingleAppInstance.WritePidFile();

			// Assert
			Assert.IsTrue(File.Exists(pidFilePath));

			// Verify file content
			string fileContent = File.ReadAllText(pidFilePath);
			var processInfo = JsonSerializer.Deserialize<ProcessInfo>(fileContent);

			Assert.IsNotNull(processInfo);
			Assert.AreEqual(Environment.ProcessId, processInfo.ProcessId);
			Assert.AreEqual(Process.GetCurrentProcess().ProcessName, processInfo.ProcessName);
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	[TestMethod]
	public void IsAlreadyRunning_WhenPidFileDoesNotExist_ShouldReturnFalse()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		if (File.Exists(pidFilePath))
		{
			File.Delete(pidFilePath);
		}

		try
		{
			// Act
			bool result = SingleAppInstance.IsAlreadyRunning();

			// Assert
			Assert.IsFalse(result);
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	[TestMethod]
	public void IsAlreadyRunning_WithCurrentProcessId_ShouldReturnFalse()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;

		// Create ProcessInfo with current process ID
		var currentProcess = Process.GetCurrentProcess();
		var processInfo = new ProcessInfo
		{
			ProcessId = currentProcess.Id,
			ProcessName = currentProcess.ProcessName,
			StartTime = currentProcess.StartTime,
			MainModuleFileName = currentProcess.MainModule?.FileName
		};

		string json = JsonSerializer.Serialize(processInfo);
		File.WriteAllText(pidFilePath, json);

		try
		{
			// Act
			bool result = SingleAppInstance.IsAlreadyRunning();

			// Assert
			Assert.IsFalse(result);
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	[TestMethod]
	public void IsAlreadyRunning_WithLegacyPidFile_ShouldStillWork()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		int currentPid = Environment.ProcessId;
		File.WriteAllText(pidFilePath, currentPid.ToString(CultureInfo.InvariantCulture));

		try
		{
			// Act
			bool result = SingleAppInstance.IsAlreadyRunning();

			// Assert
			Assert.IsFalse(result); // Should return false because it's the current process
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	[TestMethod]
	public void IsAlreadyRunning_WithNonExistentPid_ShouldReturnFalse()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		var processInfo = new ProcessInfo
		{
			ProcessId = -1, // Invalid PID
			ProcessName = "NonExistentProcess",
			StartTime = DateTime.Now,
			MainModuleFileName = "NonExistentFile.exe"
		};

		string json = JsonSerializer.Serialize(processInfo);
		File.WriteAllText(pidFilePath, json);

		try
		{
			// Act
			bool result = SingleAppInstance.IsAlreadyRunning();

			// Assert
			Assert.IsFalse(result);
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	[TestMethod]
	public void IsAlreadyRunning_WithInvalidJsonInPidFile_ShouldHandleGracefully()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.WriteAllText(pidFilePath, "This is not valid JSON");

		try
		{
			// Act
			bool result = SingleAppInstance.IsAlreadyRunning();

			// Assert
			Assert.IsFalse(result); // Should handle the error gracefully and return false
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	[TestMethod]
	public void IsAlreadyRunning_WithEmptyPidFile_ShouldHandleGracefully()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.WriteAllText(pidFilePath, string.Empty);

		try
		{
			// Act
			bool result = SingleAppInstance.IsAlreadyRunning();

			// Assert
			Assert.IsFalse(result); // Should handle the error gracefully and return false
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	[TestMethod]
	public void ShouldLaunch_WithNoExistingInstance_ShouldReturnTrue()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		if (File.Exists(pidFilePath))
		{
			File.Delete(pidFilePath);
		}

		try
		{
			// Verify that no PID file exists at the start
			Assert.IsFalse(File.Exists(pidFilePath), "PID file should not exist at test start");

			// Check if IsAlreadyRunning returns false initially
			Assert.IsFalse(SingleAppInstance.IsAlreadyRunning(), "IsAlreadyRunning should return false when no PID file exists");

			// Act
			bool result = SingleAppInstance.ShouldLaunch();

			// Assert
			Assert.IsTrue(result, "ShouldLaunch should return true when no previous instance was running");

			// ShouldLaunch should create the PID file
			Assert.IsTrue(File.Exists(pidFilePath), "PID file should be created by ShouldLaunch");

			// Read the PID file content for debugging
			if (File.Exists(pidFilePath))
			{
				string fileContent = File.ReadAllText(pidFilePath);
				Console.WriteLine($"PID file content: {fileContent}");

				try
				{
					var processInfo = JsonSerializer.Deserialize<ProcessInfo>(fileContent);
					Console.WriteLine($"Deserialized ProcessInfo: PID={processInfo?.ProcessId}, Name={processInfo?.ProcessName}");
					Console.WriteLine($"Current process: PID={Environment.ProcessId}, Name={Process.GetCurrentProcess().ProcessName}");
				}
				catch (JsonException ex)
				{
					Console.WriteLine($"Failed to deserialize PID file: {ex.Message}");
				}
			}
		}
		finally
		{
			// Cleanup
			if (File.Exists(pidFilePath))
			{
				File.Delete(pidFilePath);
			}
		}
	}

	// This class needs to mirror the internal ProcessInfo class for testing
	private sealed class ProcessInfo
	{
		public int ProcessId { get; set; }
		public string? ProcessName { get; set; }
		public DateTime StartTime { get; set; }
		public string? MainModuleFileName { get; set; }
	}
}
