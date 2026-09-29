using System.ServiceProcess;

namespace DriverToggle;

public partial class Form1 : Form
{
    private readonly System.Windows.Forms.Timer _statusTimer;

    public Form1()
    {
        InitializeComponent();
        BackColor = Color.FromArgb(18, 18, 18);
        ForeColor = Color.WhiteSmoke;
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

        _statusTimer = new System.Windows.Forms.Timer
        {
            Interval = 2000
        };
        _statusTimer.Tick += (_, _) => RefreshStatus();
        _statusTimer.Start();

        RefreshStatus();
    }

    private void BrowseButton_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Select a compatible hypervisor driver",
            Filter = "Driver files (*.sys)|*.sys|All files (*.*)|*.*",
            CheckFileExists = true,
            RestoreDirectory = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            DriverPathTextBox.Text = dialog.FileName;
        }
    }

    private void InstallButton_Click(object sender, EventArgs e)
    {
        try
        {
            var selectedPath = DriverPathTextBox.Text.Trim();
            DriverServiceManager.InstallDriver(selectedPath);
            RefreshStatus();
            MessageBox.Show(this, $"Driver installed as service '{DriverServiceManager.ServiceName}'.", "Installation complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Unable to install driver", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        try
        {
            DriverServiceManager.StartDriver();
            RefreshStatus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Unable to start driver", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void StopButton_Click(object sender, EventArgs e)
    {
        try
        {
            DriverServiceManager.StopDriver();
            RefreshStatus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Unable to stop driver", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UninstallButton_Click(object sender, EventArgs e)
    {
        try
        {
            DriverServiceManager.UninstallDriver();
            DriverPathTextBox.Text = string.Empty;
            RefreshStatus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Unable to uninstall driver", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefreshStatus()
    {
        var installed = DriverServiceManager.IsInstalled;
        var status = DriverServiceManager.GetStatus();
        var statusText = DriverServiceManager.GetStatusText();

        StatusTextLabel.Text = installed ? statusText : "Not installed";
        StatusTextLabel.ForeColor = installed ? Color.WhiteSmoke : Color.LightGray;
        StatusIndicator.BackColor = installed ? (status == ServiceControllerStatus.Running ? Color.SeaGreen : Color.Orange) : Color.Crimson;

        InstallButton.Enabled = !installed;
        StartButton.Enabled = installed && status != ServiceControllerStatus.Running;
        StopButton.Enabled = installed && status == ServiceControllerStatus.Running;
        UninstallButton.Enabled = installed;
    }
}
