namespace Code
{
    public abstract class BaseMode
    {
        // Можно было бы сделать async методами, например чтобы отыграть какую-нибудь анимацию
        public virtual void OnSetup() {}
        public virtual void OnDrop() {}
        
        public virtual void OnUpdate(float deltaTime) {}
        
        protected CursorManager CursorManager { get; }

        protected BaseMode(CursorManager cursorManager)
        {
            CursorManager = cursorManager;
        }
    }
}