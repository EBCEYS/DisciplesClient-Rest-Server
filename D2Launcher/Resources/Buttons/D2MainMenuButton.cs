namespace D2Launcher.Resources.Buttons
{
    internal class D2MainMenuButton : Button
    {
        public D2MainMenuButton()
        {
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Black;
            Image = Properties.Resources.ButtonsBackGroundImage;
            Size = new Size(134, 46);
            UseVisualStyleBackColor = true;
        }
    }
}
