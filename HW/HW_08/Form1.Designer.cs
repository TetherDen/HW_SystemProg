namespace HW_08;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        NameLabel = new Label();
        NameTextBox = new TextBox();
        SaveButton = new Button();
        LoadButton = new Button();
        FontButton = new Button();
        ColorButton = new Button();
        InputTextBox = new TextBox();
        OutputLabel = new Label();
        PrintButton = new Button();
        TextLengthLabel = new Label();
        timer = new System.Windows.Forms.Timer(components);
        TimerLabel = new Label();
        SuspendLayout();
        // 
        // NameLabel
        // 
        NameLabel.BackColor = SystemColors.ButtonFace;
        NameLabel.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
        NameLabel.Location = new Point(12, 9);
        NameLabel.Name = "NameLabel";
        NameLabel.Size = new Size(81, 26);
        NameLabel.TabIndex = 0;
        NameLabel.Text = "Username:";
        NameLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // NameTextBox
        // 
        NameTextBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
        NameTextBox.Location = new Point(99, 9);
        NameTextBox.Multiline = true;
        NameTextBox.Name = "NameTextBox";
        NameTextBox.Size = new Size(164, 26);
        NameTextBox.TabIndex = 1;
        NameTextBox.TextAlign = HorizontalAlignment.Center;
        NameTextBox.TextChanged += NameTextBox_TextChanged;
        // 
        // SaveButton
        // 
        SaveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
        SaveButton.Location = new Point(740, 402);
        SaveButton.Name = "SaveButton";
        SaveButton.Size = new Size(96, 36);
        SaveButton.TabIndex = 2;
        SaveButton.Text = "Save";
        SaveButton.UseVisualStyleBackColor = true;
        SaveButton.Click += SaveButton_Click;
        // 
        // LoadButton
        // 
        LoadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
        LoadButton.Location = new Point(485, 402);
        LoadButton.Name = "LoadButton";
        LoadButton.Size = new Size(96, 36);
        LoadButton.TabIndex = 2;
        LoadButton.Text = "Load";
        LoadButton.UseVisualStyleBackColor = true;
        LoadButton.Click += LoadButton_Click;
        // 
        // FontButton
        // 
        FontButton.BackColor = SystemColors.ControlLight;
        FontButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
        FontButton.Location = new Point(44, 127);
        FontButton.Name = "FontButton";
        FontButton.Size = new Size(49, 26);
        FontButton.TabIndex = 3;
        FontButton.Text = "Font";
        FontButton.UseVisualStyleBackColor = false;
        FontButton.Click += FontButton_Click;
        // 
        // ColorButton
        // 
        ColorButton.BackColor = SystemColors.ControlLight;
        ColorButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
        ColorButton.Location = new Point(110, 127);
        ColorButton.Name = "ColorButton";
        ColorButton.Size = new Size(49, 26);
        ColorButton.TabIndex = 3;
        ColorButton.Text = "Color";
        ColorButton.UseVisualStyleBackColor = false;
        ColorButton.Click += ColorButton_Click;
        // 
        // InputTextBox
        // 
        InputTextBox.Location = new Point(12, 159);
        InputTextBox.Multiline = true;
        InputTextBox.Name = "InputTextBox";
        InputTextBox.Size = new Size(418, 279);
        InputTextBox.TabIndex = 4;
        InputTextBox.TextChanged += InputTextBox_TextChanged;
        // 
        // OutputLabel
        // 
        OutputLabel.BackColor = SystemColors.ControlLight;
        OutputLabel.Location = new Point(457, 9);
        OutputLabel.Name = "OutputLabel";
        OutputLabel.Size = new Size(418, 369);
        OutputLabel.TabIndex = 5;
        OutputLabel.Click += OutputLabel_Click;
        // 
        // PrintButton
        // 
        PrintButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
        PrintButton.Location = new Point(615, 402);
        PrintButton.Name = "PrintButton";
        PrintButton.Size = new Size(96, 36);
        PrintButton.TabIndex = 6;
        PrintButton.Text = "Print";
        PrintButton.UseVisualStyleBackColor = true;
        PrintButton.Click += PrintButton_Click;
        // 
        // TextLengthLabel
        // 
        TextLengthLabel.Font = new Font("Segoe UI", 9.75F);
        TextLengthLabel.Location = new Point(289, 24);
        TextLengthLabel.Name = "TextLengthLabel";
        TextLengthLabel.Size = new Size(129, 25);
        TextLengthLabel.TabIndex = 7;
        TextLengthLabel.Text = "TextLength:";
        TextLengthLabel.TextAlign = ContentAlignment.MiddleLeft;
        TextLengthLabel.Click += TextLengthLabel_Click;
        // 
        // timer
        // 
        timer.Enabled = true;
        timer.Interval = 10;
        timer.Tick += timer_Tick;
        // 
        // TimerLabel
        // 
        TimerLabel.AutoSize = true;
        TimerLabel.Font = new Font("Segoe UI", 9.75F);
        TimerLabel.Location = new Point(289, 7);
        TimerLabel.Name = "TimerLabel";
        TimerLabel.Size = new Size(72, 17);
        TimerLabel.TabIndex = 8;
        TimerLabel.Text = "TimerLabel";
        TimerLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(887, 469);
        Controls.Add(TimerLabel);
        Controls.Add(TextLengthLabel);
        Controls.Add(PrintButton);
        Controls.Add(OutputLabel);
        Controls.Add(InputTextBox);
        Controls.Add(ColorButton);
        Controls.Add(FontButton);
        Controls.Add(LoadButton);
        Controls.Add(SaveButton);
        Controls.Add(NameTextBox);
        Controls.Add(NameLabel);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Form1";
        Text = "My User RegistryEditor";
        Load += Form1_Load_1;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label NameLabel;
    private TextBox NameTextBox;
    private Button SaveButton;
    private Button LoadButton;
    private Button FontButton;
    private Button ColorButton;
    private TextBox InputTextBox;
    private Label OutputLabel;
    private Button PrintButton;
    private Label TextLengthLabel;
    private System.Windows.Forms.Timer timer;
    private Label TimerLabel;
}
