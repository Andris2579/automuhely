using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Text;


namespace AutoMuhely
{
    [ToolboxItem(true)]
    [DesignerCategory("Code")]
    public class CustomDateTimePicker : DateTimePicker
    {
        private Color _backColor = Color.FromArgb(58, 63, 81);
        private Color _textColor = Color.FromArgb(245, 245, 241);
        private Color _disabledBackColor = Color.FromKnownColor(KnownColor.Control); // Background color when disabled
        private Image _image = null;  // Image next to the dropdown button

        public CustomDateTimePicker()
        {
            this.Format = DateTimePickerFormat.Short;
            this.SetStyle(ControlStyles.UserPaint, true); // Enable custom drawing
        }

        [Category("Appearance")]
        [Description("The background color of the DateTimePicker.")]
        public new Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("The background color when the DateTimePicker is disabled.")]
        public Color BackDisabledColor
        {
            get { return _disabledBackColor; }
            set { _disabledBackColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("The text color of the DateTimePicker.")]
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("The image to display next to the dropdown button.")]
        public Image Image
        {
            get { return _image; }
            set { _image = value; Invalidate(); }
        }

        // Custom paint method
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Dropdown button rectangle
            var ddbRect = new Rectangle(ClientRectangle.Width - 17, 0, 17, ClientRectangle.Height);

            // Background brush
            Brush backgroundBrush;

            // Determine the background color based on enabled/disabled state
            if (this.Enabled)
            {
                backgroundBrush = new SolidBrush(this.BackColor);
            }
            else
            {
                backgroundBrush = new SolidBrush(this._disabledBackColor);
            }

            // Fill the background of the control
            g.FillRectangle(backgroundBrush, 0, 0, ClientRectangle.Width, ClientRectangle.Height);

            // Draw the text with custom text color
            g.DrawString(this.Text, this.Font, new SolidBrush(this.TextColor), 5, 2);

            // If an image is provided, draw it next to the dropdown button
            if (_image != null)
            {
                var imageRect = new Rectangle(ClientRectangle.Width - 40, 4, ClientRectangle.Height - 8, ClientRectangle.Height - 8);
                g.DrawImage(_image, imageRect);
            }

            // Draw the dropdown button
            ComboBoxRenderer.DrawDropDownButton(g, ddbRect, this.Enabled ? ComboBoxState.Normal : ComboBoxState.Disabled);

            backgroundBrush.Dispose();
        }
    }
}
