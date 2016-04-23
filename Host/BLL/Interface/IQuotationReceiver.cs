using Host.Contract;
namespace Host.BLL.Interface
{
    public interface IQuotationReceiver
    {
        void ProcessQuotation(Quotation quotation, int volume);
    }
}