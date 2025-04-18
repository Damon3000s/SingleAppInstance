[assembly: DoNotParallelize()]

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
		File.Delete(pidFilePath);

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

	[TestMethod]
	public void IsAlreadyRunning_WhenPidFileDoesNotExist_ShouldReturnFalse()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.Delete(pidFilePath);

		// Act
		bool result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void IsAlreadyRunning_WithCurrentProcessId_ShouldReturnFalse()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.Delete(pidFilePath);

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

		// Act
		bool result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void IsAlreadyRunning_WithLegacyPidFile_ShouldStillWork()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.Delete(pidFilePath);
		int currentPid = Environment.ProcessId;
		File.WriteAllText(pidFilePath, currentPid.ToString(CultureInfo.InvariantCulture));

		// Act
		bool result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result); // Should return false because it's the current process
	}

	[TestMethod]
	public void IsAlreadyRunning_WithNonExistentPid_ShouldReturnFalse()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.Delete(pidFilePath);
		var processInfo = new ProcessInfo
		{
			ProcessId = -1, // Invalid PID
			ProcessName = "NonExistentProcess",
			StartTime = DateTime.Now,
			MainModuleFileName = "NonExistentFile.exe"
		};

		string json = JsonSerializer.Serialize(processInfo);
		File.WriteAllText(pidFilePath, json);

		// Act
		bool result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void IsAlreadyRunning_WithInvalidJsonInPidFile_ShouldHandleGracefully()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.Delete(pidFilePath);
		File.WriteAllText(pidFilePath, "This is not valid JSON");

		// Act
		bool result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result); // Should handle the error gracefully and return false
	}

	[TestMethod]
	public void IsAlreadyRunning_WithEmptyPidFile_ShouldHandleGracefully()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.Delete(pidFilePath);
		File.WriteAllText(pidFilePath, string.Empty);

		// Act
		bool result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result); // Should handle the error gracefully and return false
	}

	[TestMethod]
	public void ShouldLaunch_WithNoExistingInstance_ShouldReturnTrue()
	{
		// Arrange
		string pidFilePath = SingleAppInstance.PidFilePath;
		File.Delete(pidFilePath);

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

	// This class needs to mirror the internal ProcessInfo class for testing
	private sealed class ProcessInfo
	{
		public int ProcessId { get; set; }
		public string? ProcessName { get; set; }
		public DateTime StartTime { get; set; }
		public string? MainModuleFileName { get; set; }
	}
}
