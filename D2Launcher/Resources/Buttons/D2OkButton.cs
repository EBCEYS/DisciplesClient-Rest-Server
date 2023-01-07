namespace D2Launcher.Resources.Buttons
{
    internal class D2OkButton : Button
    {
        public D2OkButton()
        {
            BackColor = Color.Black;
            Image = Properties.Resources.D2OKButtonImage;
            ImageAlign = ContentAlignment.MiddleCenter;
            Size = new Size(31, 30);
            UseVisualStyleBackColor = false;
            FlatStyle = FlatStyle.Flat;
            Text = string.Empty;
            FlatAppearance.BorderSize = 0;
        }
    }
}
