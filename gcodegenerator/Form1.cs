using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CsPotrace;
using ImageGenerator;

namespace gcodegenerator
{
    public partial class Form1 : Form
    {
        private TextBox textBox;
        private Button fontButton;
        private Button exportButton;
        private Button helpButton;
        private Button aboutButton;
        private Button exportBMP;
        private Button importBMP;
        private Label labelText;
        private Label labelFont;
        private Label labelFontSize;
        private Label labelFontPadding;
        private NumericUpDown fontSizeNumericUpDown;
        private NumericUpDown fontPaddingNumericUpDown;
        private ComboBox fontComboBox;

        Bitmap TheBitmap = null;
        public static List<List<Curve>> ListOfPathes = new List<List<Curve>>();
        public Form1()
        {
            InitializeComponent();
            InitializeInterface();
        }

        private void InitializeInterface()
        {
            // form size and minimum size
            Size = new Size(450, 400);
            MinimumSize = new Size(450, 400);
            BackColor = Color.DarkSeaGreen;

            labelText = new Label();
            labelText.Text = "Type your text here:";
            labelText.Location = new Point(10, 10);
            labelText.Size = new Size(250, 30);
            labelText.Font = new Font("Arial", 12, FontStyle.Bold);

            labelFont = new Label();
            labelFont.Text = "Choose the font:";
            labelFont.Location = new Point(10, 50);
            labelFont.Size = new Size(200, 30);
            labelFont.Font = new Font("Arial", 12, FontStyle.Bold);

            labelFontSize = new Label();
            labelFontSize.Text = "Font Size:";
            labelFontSize.Location = new Point(10, 90);
            labelFontSize.Size = new Size(200, 30);
            labelFontSize.Font = new Font("Arial", 12, FontStyle.Bold);

            labelFontPadding = new Label();
            labelFontPadding.Text = "Font Padding:";
            labelFontPadding.Location = new Point(10, 130);
            labelFontPadding.Size = new Size(200, 30);
            labelFontPadding.Font = new Font("Arial", 12, FontStyle.Bold);

            // Create the Font Size NumericUpDown control
            fontSizeNumericUpDown = new NumericUpDown();
            fontSizeNumericUpDown.Location = new Point(210, 90);
            fontSizeNumericUpDown.Width = 100;
            fontSizeNumericUpDown.Minimum = 1;
            fontSizeNumericUpDown.Maximum = 100;
            fontSizeNumericUpDown.Value = 55;

            // Create the Font Padding NumericUpDown control
            fontPaddingNumericUpDown = new NumericUpDown();
            fontPaddingNumericUpDown.Location = new Point(210, 130);
            fontPaddingNumericUpDown.Width = 100;
            fontPaddingNumericUpDown.Minimum = 0;
            fontPaddingNumericUpDown.Maximum = 100;
            fontPaddingNumericUpDown.Value = 20;

            // TextBox
            textBox = new TextBox();
            textBox.Location = new Point(210, 10);
            textBox.Size = new Size(200, 20);

            /*// TextBox for Font Size
            fontSizeBox = new TextBox();
            fontSizeBox.Location = new Point(210, 90);
            fontSizeBox.Size = new Size(200, 20);

            // TextBox for Font Padding
            fontPaddingBox = new TextBox();
            fontPaddingBox.Location = new Point(210, 130);
            fontPaddingBox.Size = new Size(200, 20);*/

            //Font Dropdown
            fontComboBox = new ComboBox();
            fontComboBox.Location = new Point(210, 50);
            fontComboBox.Size = new Size(10, 10);
            fontComboBox.Width = 200;

            // Add the font names to the ComboBox
            fontComboBox.Items.Add("Arial");
            fontComboBox.Items.Add("Verdana");
            fontComboBox.Items.Add("Constantia");
            fontComboBox.Items.Add("Calibri");
            fontComboBox.Items.Add("Impact");

            // Set the default selected item
            fontComboBox.SelectedIndex = 0;

            // Export button
            exportButton = new Button();
            exportButton.Location = new Point(10, 220);
            exportButton.Size = new Size(200, 50);
            exportButton.Text = "Generate and Export G-Code File";
            exportButton.Click += ExportButton_Click;
            exportButton.BackColor = Color.DimGray;

            // Export BMP image button
            exportBMP = new Button();
            exportBMP.Location = new Point(10, 170);
            exportBMP.Size = new Size(200, 50);
            exportBMP.Text = "Export BMP File From Text";
            exportBMP.Click += ExportBMP_Click;
            exportBMP.BackColor = Color.DimGray;

            // Import BMP image button
            importBMP = new Button();
            importBMP.Location = new Point(210, 170);
            importBMP.Size = new Size(200, 50);
            importBMP.Text = "Import BMP File";
            importBMP.Click += ImportBMP_Click;
            importBMP.BackColor = Color.DimGray;

            // Create Help Button
            helpButton = new Button();
            helpButton.Location = new Point(10, 270);
            helpButton.Size = new Size(400, 50);
            helpButton.Text = "Help";
            helpButton.Click += Help_Click;
            helpButton.BackColor = Color.DimGray;

            // Create About Button
            aboutButton = new Button();
            aboutButton.Location = new Point(210, 220);
            aboutButton.Size = new Size(200, 50);
            aboutButton.Text = "About";
            aboutButton.Click += About_Click;
            aboutButton.BackColor = Color.DimGray;

            // Add event handlers for mouse events
            //displayLabel.MouseMove += DisplayLabel_MouseMove;

            // Add controls to the GroupBox
            //displayGroupBox.Controls.Add(displayLabel);

            // Add controls to the form
            Controls.Add(textBox);
            /*Controls.Add(fontSizeBox);
            Controls.Add(fontPaddingBox);*/
            Controls.Add(fontSizeNumericUpDown);
            Controls.Add(fontPaddingNumericUpDown);
            Controls.Add(labelText);
            Controls.Add(labelFont);
            Controls.Add(labelFontSize);
            Controls.Add(labelFontPadding);
            Controls.Add(fontButton);
            Controls.Add(exportButton);
            Controls.Add(exportBMP);
            Controls.Add(importBMP);
            Controls.Add(helpButton);
            Controls.Add(aboutButton);
            Controls.Add(fontComboBox);
        }

        /*private void FontButton_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = displayLabel.Font;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                displayLabel.Font = fontDialog.Font;
            }
        }*/

        private void ExportBMP_Click(object sender, EventArgs e)
        {
            var imageGenerator = new TextImageGenerator(Color.Black, Color.White, fontComboBox.Text, (int)fontPaddingNumericUpDown.Value, (int)fontSizeNumericUpDown.Value);
            var imageText = textBox.Text;

            imageGenerator.SaveAsBmp("imageBMP.bmp", imageText);
            /*SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "BMP files (*.bmp)|*.bmp";
            saveFileDialog.Title = "Save BMP File";
            saveFileDialog.FileName = "imageBMP.bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Text to BMP conversion completed successfully!", "Conversion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
            MessageBox.Show("Text to BMP conversion completed successfully!", "Conversion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ImportBMP_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BMP files (*.bmp)|*.bmp";
            openFileDialog.Title = "Select a BMP file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TheBitmap = Bitmap.FromFile(openFileDialog.FileName) as Bitmap;
                MessageBox.Show("Import was succesful! Next step: Generate G-Code!", "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            Potrace.Clear();
            ListOfPathes.Clear();
            Potrace.Potrace_Trace(TheBitmap, ListOfPathes);

            string gcodeCommand = "";
            gcodeCommand = Potrace.GetGCode(5, 5);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "G-code files (*.gcode)|*.gcode";
            saveFileDialog.Title = "Save G-code File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string gcodeFilePath = saveFileDialog.FileName;
                File.WriteAllText(gcodeFilePath, gcodeCommand);
                MessageBox.Show("BMP to G-code conversion completed succesfully!", "Generation Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Type your text, select font size and padding, export to BMP, import BMP and export it to G-Code. The files will be " +
                "saved in the project directory.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Sebastian-Dumitru Moisa", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}