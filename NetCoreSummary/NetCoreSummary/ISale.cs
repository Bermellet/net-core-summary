
namespace NetCoreSummary
{
    interface ISale
    {
        decimal Total { get; set; }
    }

    interface ISave
    {
        public void Save();
    }
}
