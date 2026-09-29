using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;

namespace DriverToggle;

public static class DriverServiceManager
{
    public const string ServiceName = "DenuvoDriver";

    public static bool IsInstalled => ServiceExists(ServiceName);

    public static string GetStatusText()
    {
        if (!IsInstalled)
        {
            return "Not installed";
        }

        try
        {
            using var controller = new ServiceController(ServiceName);
            return controller.Status switch
            {
                ServiceControllerStatus.Running => "Running",
                ServiceControllerStatus.StartPending => "Starting",
                ServiceControllerStatus.StopPending => "Stopping",
                ServiceControllerStatus.Stopped => "Stopped",
                ServiceControllerStatus.Paused => "Paused",
                ServiceControllerStatus.ContinuePending => "Continuing",
                ServiceControllerStatus.PausePending => "Pausing",
                _ => controller.Status.ToString()
            };
        }
        catch (InvalidOperationException)
        {
            return "Unknown";
        }
    }

    public static ServiceControllerStatus GetStatus()
    {
        if (!IsInstalled)
        {
            return ServiceControllerStatus.Stopped;
        }

        try
        {
            using var controller = new ServiceController(ServiceName);
            return controller.Status;
        }
        catch (InvalidOperationException)
        {
            return ServiceControllerStatus.Stopped;
        }
    }

    public static void InstallDriver(string driverPath)
    {
        if (string.IsNullOrWhiteSpace(driverPath))
        {
            throw new ArgumentException("Select a valid .sys file before installing the driver.", nameof(driverPath));
        }

        var fullPath = Path.GetFullPath(driverPath);
        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException("The selected driver file could not be found.", fullPath);
        }

        if (!string.Equals(Path.GetExtension(fullPath), ".sys", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Driver toggle requires a .sys file.");
        }

        if (IsInstalled)
        {
            return;
        }

        RunScCommand($"create {ServiceName} binPath= \"{fullPath}\" type= kernel start= demand error= normal");
    }

    public static void StartDriver()
    {
        if (!IsInstalled)
        {
            throw new InvalidOperationException("The driver is not installed.");
        }

        RunScCommand($"start {ServiceName}");
    }

    public static void StopDriver()
    {
        if (!IsInstalled)
        {
            throw new InvalidOperationException("The driver is not installed.");
        }

        RunScCommand($"stop {ServiceName}");
    }

    public static void UninstallDriver()
    {
        if (!IsInstalled)
        {
            return;
        }

        try
        {
            using var controller = new ServiceController(ServiceName);
            if (controller.Status == ServiceControllerStatus.Running)
            {
                controller.Stop();
                controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(15));
            }
        }
        catch (InvalidOperationException)
        {
            // Service may have already been removed, so continue to delete it.
        }
        catch (Win32Exception)
        {
            // Stop calls can fail when the driver is already in a bad state; continue to delete.
        }

        RunScCommand($"delete {ServiceName}");
    }

    private static bool ServiceExists(string serviceName)
    {
        return ServiceController.GetServices().Any(service =>
            string.Equals(service.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase));
    }

    private static void RunScCommand(string arguments)
    {
        var scPath = Path.Combine(Environment.SystemDirectory, "sc.exe");
        var startInfo = new ProcessStartInfo
        {
            FileName = scPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(startInfo);
        if (process is null)
        {
            throw new InvalidOperationException("The Service Control Manager command could not be started.");
        }

        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            var message = string.IsNullOrWhiteSpace(error) ? output : error;
            throw new InvalidOperationException($"The service command failed: {message}");
        }
    }
}
