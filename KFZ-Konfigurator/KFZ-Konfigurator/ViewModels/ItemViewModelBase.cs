using System.ComponentModel.DataAnnotations;

namespace KFZ_Konfigurator.ViewModels
{
    public abstract class ItemViewModelBase : ViewModelBase
    {
        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double Price { get; }
        public int Id { get; }
        public bool IsSelected { get; set; }

        protected ItemViewModelBase(int id, double price = 0)
        {
            Id = id;
            Price = price;
        }

    }
}