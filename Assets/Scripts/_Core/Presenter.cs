namespace Core
{
    public abstract class Presenter<TOne,TTwo> 
        where TOne: Model
        where TTwo: View
    {
        protected TOne model;
        protected TTwo view;

        public Presenter(TOne model, TTwo view)
        {
            this.model = model;
            this.view = view;
        }
    }
}
