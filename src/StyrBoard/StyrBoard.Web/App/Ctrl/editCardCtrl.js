agileControllers.controller('editCardCtrl', function ($scope, $mdDialog, $http, userStoryService, boardService, taskService, card) {
    $scope.card = card;
    $scope.isNew = !card.Id;
    $scope.tabs = { selectedIndex: 0 };
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.save = function (task) {
        userStoryService.put(task);
        boardService.notifyCardUpdated(task.Id);
        $mdDialog.hide();
    };
    $scope.create = function (task) {
        userStoryService.post(task)
            .then(function (location) {
                boardService.notifyCardAdded(location);
                $mdDialog.hide();
            });
    };
    $scope.createTask = function (evt) {
        if (evt.keyCode === 13) {
            taskService.post({ Name: evt.currentTarget.value, UserStoryId: card.Id });
        }
    };
    $scope.deleteUserStory = function (id) {
        userStoryService.deleteUserStory(id);
        boardService.notifyCardDeleted(id);
        $mdDialog.hide('remove');
    };
});