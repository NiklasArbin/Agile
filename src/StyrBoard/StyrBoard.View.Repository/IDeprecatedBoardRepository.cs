using System.Collections.Generic;
using StyrBoard.View.Model;

namespace StyrBoard.View.Repository
{
    public interface IDeprecatedBoardRepository
    {
        List<Column> GetColumns();
        void MoveTask(int taskId, int targetColId);
        Task GetTask(int taskId);
        void DeleteTask(int taskId);
    }
}