agileControllers.controller('editCardCtrl', function ($scope, $mdDialog, $http, taskService, boardService, card) {
    $scope.task = card;
    $scope.isNew = card.Id < 0;
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
    $scope.create = function (task) {
        taskService.createTask(task)
            .then(function (location) {
                boardService.notifyCardAdded(location);
                $mdDialog.hide();
        });
    };
    $scope.delete = function (id) {
        taskService.deleteTask(id);
        boardService.notifyCardDeleted(id);
        $mdDialog.hide('delete');
    };
});