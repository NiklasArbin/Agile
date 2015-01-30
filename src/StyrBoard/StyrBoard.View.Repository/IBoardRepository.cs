using StyrBoard.View.Model;

namespace StyrBoard.View.Repository
{
    public interface IBoardRepository
    {
        Board Get();
        void MoveUserStory(int userStoryId, int columnId);
    }
}
