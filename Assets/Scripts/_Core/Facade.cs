namespace Core
{
    public abstract class Facade<TOne,TTwo> 
        where TOne: Model
        where TTwo: View
    {
        private Presenter<TOne,TTwo> _presenter;
        
        public Facade(Presenter<TOne,TTwo> presenter)
        {
            _presenter = presenter;
        }
    }
}
