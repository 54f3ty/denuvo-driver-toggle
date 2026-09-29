namespace DriverToggle;

partial class Form1
{
    private System.ComponentModel.IContainer components = null!;
    private Label TitleLabel = null!;
    private Label SubtitleLabel = null!;
    private Label DriverPathLabel = null!;
    private TextBox DriverPathTextBox = null!;
    private Button BrowseButton = null!;
    private Button InstallButton = null!;
    private Button StartButton = null!;
    private Button StopButton = null!;
    private Button UninstallButton = null!;
    private Panel StatusPanel = null!;
    private Label StatusTitleLabel = null!;
    private Label StatusTextLabel = null!;
    private Label StatusIndicator = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        SuspendLayout();

        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(18, 18, 18);
        ClientSize = new System.Drawing.Size(620, 360);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = true;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Denuvo Driver Toggle";

        TitleLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.WhiteSmoke,
            Location = new Point(24, 20),
            Name = "TitleLabel",
            Size = new Size(260, 32),
            TabIndex = 0,
            Text = "Denuvo Driver Toggle"
        };

        SubtitleLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.Gainsboro,
            Location = new Point(24, 62),
            Name = "SubtitleLabel",
            Size = new Size(310, 19),
            TabIndex = 1,
            Text = "Install, monitor, and toggle your hypervisor driver."
        };

        DriverPathLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.WhiteSmoke,
            Location = new Point(24, 118),
            Name = "DriverPathLabel",
            Size = new Size(104, 15),
            TabIndex = 2,
            Text = "Driver file (.sys)"
        };

        DriverPathTextBox = new TextBox
        {
            BackColor = Color.FromArgb(30, 30, 30),
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.WhiteSmoke,
            Location = new Point(24, 140),
            Name = "DriverPathTextBox",
            ReadOnly = true,
            Size = new Size(420, 23),
            TabIndex = 3
        };

        BrowseButton = new Button
        {
            BackColor = Color.FromArgb(56, 56, 56),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.WhiteSmoke,
            Location = new Point(456, 139),
            Name = "BrowseButton",
            Size = new Size(130, 28),
            TabIndex = 4,
            Text = "Browse"
        };
        BrowseButton.FlatAppearance.BorderColor = Color.FromArgb(120, 120, 120);
        BrowseButton.Click += BrowseButton_Click;

        InstallButton = new Button
        {
            BackColor = Color.FromArgb(35, 107, 79),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.WhiteSmoke,
            Location = new Point(24, 190),
            Name = "InstallButton",
            Size = new Size(135, 38),
            TabIndex = 5,
            Text = "Install Driver"
        };
        InstallButton.FlatAppearance.BorderColor = Color.FromArgb(90, 181, 132);
        InstallButton.Click += InstallButton_Click;

        StartButton = new Button
        {
            BackColor = Color.FromArgb(45, 45, 45),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.WhiteSmoke,
            Location = new Point(175, 190),
            Name = "StartButton",
            Size = new Size(110, 38),
            TabIndex = 6,
            Text = "Start"
        };
        StartButton.FlatAppearance.BorderColor = Color.FromArgb(120, 120, 120);
        StartButton.Click += StartButton_Click;

        StopButton = new Button
        {
            BackColor = Color.FromArgb(45, 45, 45),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.WhiteSmoke,
            Location = new Point(295, 190),
            Name = "StopButton",
            Size = new Size(110, 38),
            TabIndex = 7,
            Text = "Stop"
        };
        StopButton.FlatAppearance.BorderColor = Color.FromArgb(120, 120, 120);
        StopButton.Click += StopButton_Click;

        UninstallButton = new Button
        {
            BackColor = Color.FromArgb(73, 44, 44),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.WhiteSmoke,
            Location = new Point(425, 190),
            Name = "UninstallButton",
            Size = new Size(164, 38),
            TabIndex = 8,
            Text = "Uninstall Driver"
        };
        UninstallButton.FlatAppearance.BorderColor = Color.FromArgb(153, 96, 96);
        UninstallButton.Click += UninstallButton_Click;

        StatusPanel = new Panel
        {
            BackColor = Color.FromArgb(28, 28, 28),
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(24, 255),
            Name = "StatusPanel",
            Size = new Size(565, 80),
            TabIndex = 9
        };

        StatusIndicator = new Label
        {
            BackColor = Color.Crimson,
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(18, 23),
            Name = "StatusIndicator",
            Size = new Size(18, 18),
            TabIndex = 10
        };

        StatusTitleLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.Gainsboro,
            Location = new Point(48, 24),
            Name = "StatusTitleLabel",
            Size = new Size(72, 15),
            TabIndex = 11,
            Text = "Status"
        };

        StatusTextLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.WhiteSmoke,
            Location = new Point(48, 42),
            Name = "StatusTextLabel",
            Size = new Size(124, 25),
            TabIndex = 12,
            Text = "Not installed"
        };

        StatusPanel.Controls.Add(StatusIndicator);
        StatusPanel.Controls.Add(StatusTitleLabel);
        StatusPanel.Controls.Add(StatusTextLabel);

        Controls.Add(TitleLabel);
        Controls.Add(SubtitleLabel);
        Controls.Add(DriverPathLabel);
        Controls.Add(DriverPathTextBox);
        Controls.Add(BrowseButton);
        Controls.Add(InstallButton);
        Controls.Add(StartButton);
        Controls.Add(StopButton);
        Controls.Add(UninstallButton);
        Controls.Add(StatusPanel);

        ResumeLayout(false);
        PerformLayout();
    }
}
