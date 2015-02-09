namespace StyrBoard.Domain.Model
{
    public interface IDescribed
    {
        string Title { get; set; }
        string Description { get; set; }
    }
}