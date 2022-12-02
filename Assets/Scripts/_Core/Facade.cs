namespace Core
{
    public abstract class Facade
    {
        private Presenter _presenter;
        
        public Facade(Presenter presenter)
        {
            _presenter = presenter;
        }
    }
}
