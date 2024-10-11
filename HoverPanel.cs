using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMuhely
{
    [ToolboxItem(true)]  // Ensure it shows in the toolbox
    [DesignerCategory("Code")]  // Mark this as a code component
    public class HoverPanel : Panel
    {
        private Color originalColor = Color.FromArgb(24, 30, 54); // Default original color
        private Color hoverColor = Color.FromArgb(46, 51, 73); // Default hover color
        private bool isHovered = false;  // Tracks hover state
        private bool isSelected = false; // Tracks if the panel is selected
        private int hoverLineThickness = 5;  // Thickness of the blue line
        private Color hoverLineColor = Color.FromArgb(3, 135, 246);  // Color of the blue line

        private static HoverPanel selectedPanel = null; // Static reference to track the selected panel

        // Constructor
        public HoverPanel()
        {
            this.BackColor = originalColor;  // Set the initial background color

            // Attach event handlers for hover and click events
            this.MouseEnter += HoverPanel_MouseEnter;
            this.MouseLeave += HoverPanel_MouseLeave;
            this.MouseClick += HoverPanel_MouseClick;

            // Attach event handlers to all current child controls
            AttachMouseEventsToChildren(this.Controls);
        }

        // Property to set the default (original) background color from designer
        [Category("Appearance")]
        [Description("The original background color of the panel.")]
        public Color OriginalColor
        {
            get { return originalColor; }
            set
            {
                originalColor = value;
                if (!isHovered && !isSelected)
                {
                    this.BackColor = originalColor; // Update the panel's background only if not hovered or selected
                }
                Invalidate();  // Redraw the panel with the new color
            }
        }

        // Property to set the hover color from designer
        [Category("Appearance")]
        [Description("The background color when the mouse hovers over the panel.")]
        public Color HoverColor
        {
            get { return hoverColor; }
            set { hoverColor = value; }
        }

        // Override the DefaultSize property to set a default size for the panel
        protected override Size DefaultSize
        {
            get
            {
                return new Size(269, 57); // Set default size: 269x57
            }
        }

        // Override OnControlAdded to attach mouse events to newly added controls
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            AttachMouseEventsToControl(e.Control);  // Attach mouse events to new control
        }

        // Attach mouse enter/leave events to all child controls
        private void AttachMouseEventsToChildren(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                AttachMouseEventsToControl(control);
            }
        }

        // Attach mouse enter/leave events to a single control
        private void AttachMouseEventsToControl(Control control)
        {
            control.MouseEnter += (sender, e) => HoverPanel_MouseEnter(sender, e);
            control.MouseLeave += (sender, e) => HoverPanel_MouseLeave(sender, e);
            control.MouseClick += (sender, e) => HoverPanel_MouseClick(sender, e);

            // If the control has its own child controls, apply the same logic recursively
            if (control.HasChildren)
            {
                AttachMouseEventsToChildren(control.Controls);
            }
        }

        // Method to handle mouse enter (change color to hover color and set hover flag)
        private void HoverPanel_MouseEnter(object sender, EventArgs e)
        {
            if (!isSelected) // Only change color if not selected
            {
                this.BackColor = hoverColor;
                isHovered = true;  // Set hover state to true
                Invalidate();  // Trigger repaint to show the blue line
            }
        }

        // Method to handle mouse leave (revert to original color and clear hover flag)
        private void HoverPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!isSelected && !this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                this.BackColor = originalColor;
                isHovered = false;  // Set hover state to false
                Invalidate();  // Trigger repaint to hide the blue line
            }
        }

        // Method to handle mouse click (select the panel and update its state)
        private void HoverPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedPanel != null && selectedPanel != this)
            {
                // Deselect the previously selected panel
                selectedPanel.isSelected = false;
                selectedPanel.BackColor = selectedPanel.originalColor;
                selectedPanel.Invalidate();
            }

            // Set the clicked panel as the new selected panel
            selectedPanel = this;
            isSelected = true;
            this.BackColor = hoverColor;
            Invalidate();  // Trigger repaint to show the blue line
        }

        // Override OnPaint to draw the blue line on the left side when hovered or selected
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isHovered || isSelected)
            {
                using (Pen hoverPen = new Pen(hoverLineColor, hoverLineThickness))
                {
                    // Draw a vertical line on the left side of the panel
                    e.Graphics.DrawLine(hoverPen, 0, 0, 0, this.Height);
                }
            }
        }
    }
}