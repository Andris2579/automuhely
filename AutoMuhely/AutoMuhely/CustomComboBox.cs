using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoMuhely
{
    [ToolboxItem(true)]  // Ensure it shows in the toolbox
    [DesignerCategory("Code")]  // Mark this as a code component
    public class CustomComboBox : ComboBox
    {
        private Color originalBackColor = Color.FromArgb(58, 63, 81); // Default background color
        private Color originalForeColor = Color.FromArgb(245, 245, 241); // Default text color
        private Color borderColor = Color.FromArgb(87, 88, 92); // Border color
        private int borderThickness = 2; // Border thickness

        public CustomComboBox()
        {
            // Set the default colors
            this.BackColor = originalBackColor;
            this.ForeColor = originalForeColor;
            this.DropDownStyle = ComboBoxStyle.DropDownList;  // Avoid user typing in combo box
            this.DrawMode = DrawMode.OwnerDrawFixed; // Enable custom drawing
            this.FlatStyle = FlatStyle.Flat;  // Set to flat to avoid default border drawing
            this.Margin = new Padding(0);  // Remove any extra margins
        }

        // Property for custom back color
        [Category("Appearance")]
        [Description("The background color of the ComboBox.")]
        public Color OriginalBackColor
        {
            get { return originalBackColor; }
            set { originalBackColor = value; this.BackColor = value; }
        }

        // Property for custom fore color
        [Category("Appearance")]
        [Description("The text color of the ComboBox.")]
        public Color OriginalForeColor
        {
            get { return originalForeColor; }
            set { originalForeColor = value; this.ForeColor = value; }
        }

        // Property for custom border color
        [Category("Appearance")]
        [Description("The border color of the ComboBox.")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        // Property for custom border thickness
        [Category("Appearance")]
        [Description("The border thickness of the ComboBox.")]
        public int BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; this.Invalidate(); }
        }

        // Override OnPaint to customize the border and background
        protected override void OnPaint(PaintEventArgs e)
{
    base.OnPaint(e);

    // Draw the background
    using (Brush backgroundBrush = new SolidBrush(this.BackColor))
    {
        e.Graphics.FillRectangle(backgroundBrush, this.ClientRectangle);
    }

    // Draw the border manually with the custom color
    using (Pen borderPen = new Pen(borderColor, borderThickness))
    {
        e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
    }

    // Draw the dropdown arrow
    var arrowRect = new Rectangle(this.Width - 20, 2, 16, this.Height - 4);
    if (this.Enabled)  // Check if ComboBox is enabled
    {
        ControlPaint.DrawComboButton(e.Graphics, arrowRect, ButtonState.Normal); // Use Normal for enabled state
    }
    else
    {
        ControlPaint.DrawComboButton(e.Graphics, arrowRect, ButtonState.Inactive); // Use Inactive for disabled state
    }
}


        // Handle drawing the combo box items
        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox comboBox = sender as ComboBox;

            // Retrieve the item text (only the value part)
            var item = (KeyValuePair<int, string>)comboBox.Items[e.Index];
            string itemText = item.Value; // Only show the Value (the name of the car)

            // Set custom colors
            Color backgroundColor = originalBackColor; // Custom background color
            Color textColor = originalForeColor;       // Custom text color

            // Draw the background
            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Draw the item text
            using (Brush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(itemText, e.Font, textBrush, e.Bounds);
            }

            // Draw focus rectangle if needed
            e.DrawFocusRectangle();
        }

        // Ensure the combo box draws properly when it is initialized
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.DrawItem += comboBox1_DrawItem;
        }

        // Method to load items into the ComboBox and display them correctly
        public void LoadItems<T>(List<KeyValuePair<int, string>> items)
        {
            this.DataSource = items;
            this.DisplayMember = "Value"; // Display the string (car model)
            this.ValueMember = "Key";     // Store the id in the background
        }
    }
}
