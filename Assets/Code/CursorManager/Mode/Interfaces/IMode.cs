namespace Code
{
    /*
     * Запихал все в один интерфейс, согласно SOLID стоило бы бить на несколько,
     * но для этого проекта это будет явно избыточно.
     * Интерфейс нужен, чтобы спрятать дженерик в CursorManager
     */
    public interface IMode
    {
        void OnSetup();
        void OnDrop();
        void OnUpdate(float deltaTime);
    }
}