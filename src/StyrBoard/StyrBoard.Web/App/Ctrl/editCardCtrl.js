agileControllers.controller('editCardCtrl', function ($scope, $mdDialog, $http, userStoryService, boardService, card) {
    $scope.card = card;
    $scope.isNew = !card.Id;
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
    $scope.deleteUserStory = function (id) {
        userStoryService.deleteUserStory(id);
        boardService.notifyCardDeleted(id);
        $mdDialog.hide('remove');
    };
});