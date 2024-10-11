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
    public class CustomButton : Button
    {
        // Fields for appearance
        private int borderSize = 1;
        private int borderRadius = 30;  // Radius for rounded corners
        private Color borderColor = Color.Black;

        // Constructor
        public CustomButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0; // Borderless by default
            BackColor = Color.Blue; // Example default background color
            ForeColor = Color.White; // Text color
            Resize += new EventHandler(Button_Resize); // Adjust border radius on resize
        }

        // Property for border radius
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = (value <= Height) ? value : Height;
                Invalidate(); // Redraw the button when radius is set
            }
        }

        // Method to create a rounded path for the button
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);          // Top-left corner
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90); // Top-right corner
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);  // Bottom-right
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);  // Bottom-left
            path.CloseFigure();
            return path;
        }

        // Override OnPaint to draw custom button appearance
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF surfaceRect = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF borderRect = new RectangleF(1, 1, this.Width - 2, this.Height - 2);

            if (borderRadius > 2) // Draw rounded button
            {
                using (GraphicsPath pathSurface = GetFigurePath(surfaceRect, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(borderRect, borderRadius - 1))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.DrawPath(penSurface, pathSurface); // Button surface
                    if (borderSize >= 1) // Draw border if size is greater than 1
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawPath(penBorder, pathBorder); // Button border
                    }
                    this.Region = new Region(pathSurface); // Set region for rounded corners
                }
            }
            else // Draw square button
            {
                this.Region = new Region(surfaceRect);
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1); // Square button border
                    }
                }
            }
        }

        // Event handler for resizing button (adjust radius)
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
            {
                borderRadius = this.Height;
            }
        }
    }
}
