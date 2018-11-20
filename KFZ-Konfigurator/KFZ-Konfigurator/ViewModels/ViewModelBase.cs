namespace KFZ_Konfigurator.ViewModels
{
    public abstract class ViewModelBase
    {

        public double Price { get; }
        public int Id { get; }

        protected ViewModelBase(int id, double price = 0)
        {
            Id = id;
            Price = price;
        }

    }
}