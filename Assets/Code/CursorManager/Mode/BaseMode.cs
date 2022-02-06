namespace Code
{
    public abstract class BaseMode<TSettings> : IMode
        where TSettings : ModeSettings
    {
        public CursorManager CursorManager { get; init; }
        
        public TSettings Settings { get; init; }
        
        // Можно было бы сделать async методами, например чтобы отыграть какую-нибудь анимацию
        public virtual void OnSetup() {}
        
        public virtual void OnDrop() {}
        
        
        public virtual void OnUpdate(float deltaTime) {}
    }
}