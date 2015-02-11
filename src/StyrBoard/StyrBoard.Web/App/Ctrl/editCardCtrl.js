agileControllers.controller('editCardCtrl', function ($scope, $mdDialog, $http, userStoryService, boardService, card) {
    $scope.task = card;
    $scope.isNew = !card.Id;
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.save = function (task) {
        userStoryService.saveTask(task);
        boardService.notifyCardUpdated(task.Id);
        $mdDialog.hide();
    };
    $scope.create = function (task) {
        userStoryService.createTask(task)
            .then(function (location) {
                boardService.notifyCardAdded(location);
                $mdDialog.hide();
        });
    };
    $scope.delete = function (id) {
        userStoryService.deleteTask(id);
        boardService.notifyCardDeleted(id);
        $mdDialog.hide('delete');
    };
});