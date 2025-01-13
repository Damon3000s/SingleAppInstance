namespace ktsu.SingleAppInstance.Test;

[TestClass]
public class SingleAppInstanceTests
{
	[TestMethod]
	public void TestExitIfAlreadyRunningWhenAnotherInstanceIsRunningShouldExit()
	{
		// Arrange
		var originalShouldLaunch = SingleAppInstance.ShouldLaunch;
		SingleAppInstance.ShouldLaunch = () => false;

		// Act & Assert
		Assert.ThrowsException<Environment.ExitException>(SingleAppInstance.ExitIfAlreadyRunning);

		// Cleanup
		SingleAppInstance.ShouldLaunch = originalShouldLaunch;
	}

	[TestMethod]
	public void TestShouldLaunchWhenNoOtherInstanceIsRunningShouldReturnTrue()
	{
		// Arrange
		var originalIsAlreadyRunning = SingleAppInstance.IsAlreadyRunning;
		SingleAppInstance.IsAlreadyRunning = () => false;

		// Act
		var result = SingleAppInstance.ShouldLaunch();

		// Assert
		Assert.IsTrue(result);

		// Cleanup
		SingleAppInstance.IsAlreadyRunning = originalIsAlreadyRunning;
	}

	[TestMethod]
	public void TestShouldLaunchWhenAnotherInstanceIsRunningShouldReturnFalse()
	{
		// Arrange
		var originalIsAlreadyRunning = SingleAppInstance.IsAlreadyRunning;
		SingleAppInstance.IsAlreadyRunning = () => true;

		// Act
		var result = SingleAppInstance.ShouldLaunch();

		// Assert
		Assert.IsFalse(result);

		// Cleanup
		SingleAppInstance.IsAlreadyRunning = originalIsAlreadyRunning;
	}

	[TestMethod]
	public void TestIsAlreadyRunningWhenPidFileDoesNotExistShouldReturnFalse()
	{
		// Arrange
		var pidFilePath = SingleAppInstance.PidFilePath;
		if (File.Exists(pidFilePath))
		{
			File.Delete(pidFilePath);
		}

		// Act
		var result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void TestIsAlreadyRunningWhenPidFileExistsAndProcessIsRunningShouldReturnTrue()
	{
		// Arrange
		var pidFilePath = SingleAppInstance.PidFilePath;
		var currentPid = Process.GetCurrentProcess().Id;
		File.WriteAllText(pidFilePath, currentPid.ToString(CultureInfo.InvariantCulture));

		// Act
		var result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsTrue(result);

		// Cleanup
		File.Delete(pidFilePath);
	}

	[TestMethod]
	public void TestIsAlreadyRunningWhenPidFileExistsAndProcessIsNotRunningShouldReturnFalse()
	{
		// Arrange
		var pidFilePath = SingleAppInstance.PidFilePath;
		var nonExistentPid = -1;
		File.WriteAllText(pidFilePath, nonExistentPid.ToString(CultureInfo.InvariantCulture));

		// Act
		var result = SingleAppInstance.IsAlreadyRunning();

		// Assert
		Assert.IsFalse(result);

		// Cleanup
		File.Delete(pidFilePath);
	}
}
