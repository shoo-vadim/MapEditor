namespace Code
{
    public class Off : BaseMode
    {
        // Использую этот режим для сброса на старте приложения
        public Off(CursorManager cursorManager) : base(cursorManager) { }

        public override void OnSetup()
        {
            CursorManager.SelectionBox.Show(false);
            
            foreach (var cursor in CursorManager.Cursors)
                cursor.Show(false);
        }
    }
}