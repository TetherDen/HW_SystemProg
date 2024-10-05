using System.Drawing.Printing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HW_08;

public partial class Form1 : Form
{
    // ���������� ��� ������
    private string result = default;
    private DateTime startTime;

    private void Form1_Shown(object sender, EventArgs e)
    {
        // ��� ������� ���������� ������ ����� �� InputTextBox � ������ � ����� ������
        InputTextBox.Focus();
        InputTextBox.Select(InputTextBox.Text.Length, 0);
    }
    private void Form1_Load(object sender, EventArgs e)
    {
        InputTextBox.Text = RegistryManager.ReadRegistryValue("UserText", "");
        NameTextBox.Text = RegistryManager.ReadRegistryValue("UserName", "Jimbo");

        NameTextBox.Font = new Font(NameTextBox.Font.FontFamily, 12);

        string fontName = RegistryManager.ReadRegistryValue("FontName", "Arial");
        float fontSize = float.Parse(RegistryManager.ReadRegistryValue("FontSize", "10"));
        FontStyle fontStyle = (FontStyle)RegistryManager.ReadRegistryValue("FontStyle", 0);

        Font font = new Font(fontName, fontSize, fontStyle);
        InputTextBox.Font = font;
        OutputLabel.Font = font;

        int colorArgb = int.Parse(RegistryManager.ReadRegistryValue("TextColor", Color.Black.ToArgb().ToString()));
        InputTextBox.ForeColor = Color.FromArgb(colorArgb);
        OutputLabel.ForeColor = Color.FromArgb(colorArgb);

        startTime = DateTime.Now; //������
    }

    public Form1()
    {
        InitializeComponent();
        this.Load += Form1_Load;
        this.Shown += Form1_Shown;
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        string userName = NameTextBox.Text;
        RegistryManager.WriteRegistryValue("UserName", userName);
        RegistryManager.WriteRegistryValue("UserText", InputTextBox.Text);

        RegistryManager.WriteRegistryValue("FontName", InputTextBox.Font.Name);
        RegistryManager.WriteRegistryValue("FontSize", InputTextBox.Font.Size);
        RegistryManager.WriteRegistryValue("FontStyle", (int)InputTextBox.Font.Style);

        RegistryManager.WriteRegistryValue("TextColor", InputTextBox.ForeColor.ToArgb());
    }

    private void LoadButton_Click(object sender, EventArgs e)
    {
        NameTextBox.Text = RegistryManager.ReadRegistryValue("UserName", "Jimbo");
        InputTextBox.Text = RegistryManager.ReadRegistryValue("UserText", "");

        string fontName = RegistryManager.ReadRegistryValue("FontName", "Arial");
        float fontSize = float.Parse(RegistryManager.ReadRegistryValue("FontSize", "10"));
        FontStyle fontStyle = (FontStyle)RegistryManager.ReadRegistryValue("FontStyle", 0);

        Font font = new Font(fontName, fontSize, fontStyle);
        InputTextBox.Font = font;
        OutputLabel.Font = font;

        int colorArgb = int.Parse(RegistryManager.ReadRegistryValue("TextColor", Color.Black.ToArgb().ToString()));
        InputTextBox.ForeColor = Color.FromArgb(colorArgb);
        OutputLabel.ForeColor = Color.FromArgb(colorArgb);
    }

    private void FontButton_Click(object sender, EventArgs e)
    {
        using (FontDialog fontDialog = new FontDialog())
        {
            fontDialog.Font = InputTextBox.Font;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                InputTextBox.Font = fontDialog.Font;
                OutputLabel.Font = fontDialog.Font;
            }
        }
    }

    private void ColorButton_Click(object sender, EventArgs e)
    {
        using (ColorDialog colorDialog = new ColorDialog())
        {
            colorDialog.Color = InputTextBox.ForeColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                InputTextBox.ForeColor = colorDialog.Color;
                OutputLabel.ForeColor = colorDialog.Color;
            }
        }
    }

    private void InputTextBox_TextChanged(object sender, EventArgs e)
    {
        OutputLabel.Text = InputTextBox.Text;
        TextLengthLabel.Text = $"Text Length: {InputTextBox.Text.Length}";
    }

    private void OutputLabel_Click(object sender, EventArgs e)
    {

    }

    private void PrintButton_Click(object sender, EventArgs e)
    {
        result = InputTextBox.Text;
        PrintDocument printDocument = new PrintDocument();
        printDocument.PrintPage += PrintDocument_PrintPage; ;
        PrintDialog printDialog = new PrintDialog();
        printDialog.Document = printDocument;

        if (printDialog.ShowDialog() == DialogResult.OK)
        {
            printDialog.Document.Print();
        }

    }

    //  ��� ����� �� � ���� ������?
    //private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
    //{
    //    e.Graphics.DrawString(result,new Font("Arial", 14),Brushes.Black, 10 , 10);
    //}

    private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
    {
        // ������������� ��������� ����������
        float x = e.MarginBounds.Left; // �������� � ������ ����
        float y = e.MarginBounds.Top; // �������� � �������� ����

        // ����� ��� ������
        Font font = new Font(InputTextBox.Font.FontFamily, InputTextBox.Font.Size, InputTextBox.Font.Style);

        // ������������ ������ ������
        float maxWidth = e.MarginBounds.Width;

        // �������� ���� �� InputTextBox
        Brush textBrush = new SolidBrush(InputTextBox.ForeColor);

        // ��������� ����� �� ������
        string[] lines = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        foreach (string line in lines)
        {
            // ���������, ��������� �� ������ ������������ ������
            if (e.Graphics.MeasureString(line, font).Width > maxWidth)
            {
                string[] words = line.Split(' '); // ��������� ������ �� �����
                string currentLine = "";

                foreach (string word in words)
                {
                    string testLine = currentLine + word + " ";
                    if (e.Graphics.MeasureString(testLine, font).Width > maxWidth)
                    {
                        // ���� ���������� ����� ��������� ������, ������� ������� ������ � ��������� �� �����
                        e.Graphics.DrawString(currentLine.Trim(), font, textBrush, x, y);
                        currentLine = word + " "; // �������� ����� ������
                        y += font.GetHeight(e.Graphics); // ��������� ����
                    }
                    else
                    {
                        currentLine = testLine; // ��������� ������� ������
                    }
                }
                // ������� ������� ������� ������
                if (!string.IsNullOrEmpty(currentLine))
                {
                    e.Graphics.DrawString(currentLine.Trim(), font, textBrush, x, y);
                    y += font.GetHeight(e.Graphics); // ��������� ����
                }
            }
            else
            {
                // ���� ����� ������ ������ ������������ ������, ������ ������� �
                e.Graphics.DrawString(line, font, textBrush, x, y);
                y += font.GetHeight(e.Graphics); // ��������� ����
            }
        }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        TimeSpan elapsedTime = DateTime.Now - startTime;
        TimerLabel.Text = $"Timer: {elapsedTime:hh\\:mm\\:ss\\.ff}";
    }

    private void TextLengthLabel_Click(object sender, EventArgs e)
    {

    }

    private void Form1_Load_1(object sender, EventArgs e)
    {

    }
}
