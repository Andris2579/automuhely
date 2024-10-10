using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMuhely
{
    public class CustomTextBox : UserControl
    {
        // Fields for appearance
        private int borderSize = 2;
        private int borderRadius = 15; // Radius for rounded corners
        private Color borderColor = Color.Black;
        private Color backgroundColor = Color.White;

        // The internal TextBox control
        private TextBox textBox;

        // Constructor
        public CustomTextBox()
        {
            // Set default properties for the UserControl
            this.BackColor = backgroundColor;
            this.Padding = new Padding(borderRadius / 2);

            // Create and configure the internal TextBox
            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None, // No border on the internal TextBox
                BackColor = backgroundColor,
                ForeColor = Color.Black,
                Dock = DockStyle.Fill // Fill the UserControl area
            };

            // Add the TextBox to the UserControl
            this.Controls.Add(textBox);

            // Resize event handler
            this.Resize += new EventHandler(TextBox_Resize);
        }

        // Property to expose the TextBox's text
        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        // Property for border radius
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = (value <= Height) ? value : Height;
                Invalidate(); // Redraw when the radius is changed
            }
        }

        // Property for border size
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                Invalidate(); // Redraw when the size is changed
            }
        }

        // Property for border color
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate(); // Redraw when the color is changed
            }
        }

        // Property for password masking
        public char PasswordChar
        {
            get { return textBox.PasswordChar; }
            set { textBox.PasswordChar = value; }
        }

        // Property for multiline support
        public bool Multiline
        {
            get { return textBox.Multiline; }
            set
            {
                textBox.Multiline = value;
                if (value)
                {
                    // Adjust padding to make room for multiline textbox
                    this.Padding = new Padding(borderRadius / 2, borderRadius / 2, borderRadius / 2, 10);
                }
                else
                {
                    this.Padding = new Padding(borderRadius / 2);
                }
            }
        }

        // Method to create a rounded rectangle path for drawing
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // Top-left corner
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90); // Top-right corner
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90); // Bottom-right corner
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90); // Bottom-left corner
            path.CloseFigure();
            return path;
        }

        // Override OnPaint event to draw the custom border and background
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Enable anti-aliasing for smoother corners
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the rounded rectangle border
            Rectangle borderRect = this.ClientRectangle;
            borderRect.Inflate(-1, -1); // Adjust for border size

            using (GraphicsPath pathBorder = GetRoundedRectanglePath(borderRect, borderRadius))
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                this.Region = new Region(pathBorder); // Set region for rounded corners
                e.Graphics.DrawPath(penBorder, pathBorder); // Draw the border
            }
        }

        // Event handler for resizing the control, adjusting the border radius
        private void TextBox_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
            {
                borderRadius = this.Height;
            }
        }
    }
}
