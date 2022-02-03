namespace Code
{
    public class Off : BaseMode
    {
        public Off(CursorManager cursorManager) : base(cursorManager) { }

        public override void OnSetup()
        {
            foreach (var cursor in CursorManager.Cursors)
            {
                cursor.gameObject.SetActive(false);
            }
        }
    }
}