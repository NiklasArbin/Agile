agile.kanbanBoardApp.controller('editCardCtrl', function ($scope, $mdDialog, $http, taskService, card) {
    $scope.task = card;
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.save = function (task) {
        taskService.saveTask(task);
        boardService.notifyCardUpdated(task.Id);
        $mdDialog.hide();
    };

    $scope.delete = function (id) {
        taskService.deleteTask(id);
        boardService.notifyCardDeleted(id);
        $mdDialog.hide('delete');
    };
});