using System.ComponentModel.DataAnnotations;

namespace KFZ_Konfigurator.ViewModels
{
    public abstract class ViewModelBase
    {
        [DisplayFormat(DataFormatString = "{0:N0} EUR")]
        public double Price { get; }
        public int Id { get; }

        protected ViewModelBase(int id, double price = 0)
        {
            Id = id;
            Price = price;
        }

    }
}