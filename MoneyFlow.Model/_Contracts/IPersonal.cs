namespace MoneyFlow.Model
{
    public interface IPersonal
    {
        int AccountId { get; set; }
        Account Account { get; set; }
    }
}