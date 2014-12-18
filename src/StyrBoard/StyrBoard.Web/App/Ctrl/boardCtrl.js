agile.kanbanBoardApp.controller('boardCtrl', function ($scope, $mdToast, $mdDialog, boardService) {
    // Model
    $scope.columns = [];
    $scope.isLoading = false;

    function init() {
        $scope.isLoading = true;
        boardService.initialize().then(function (data) {
            $scope.isLoading = false;
            $scope.refreshBoard();
        }, onError);
    };

    $scope.refreshBoard = function refreshBoard() {
        $scope.isLoading = true;
        boardService.getColumns()
           .then(function (data) {
               $scope.isLoading = false;
               $scope.columns = data;
           }, onError);
    };

    $scope.onDrop = function (data, targetColId) {
        boardService.canMoveTask(data.ColumnId, targetColId)
            .then(function (canMove) {
                if (canMove) {
                    boardService.moveTask(data.Id, targetColId).then(function (taskMoved) {
                        $scope.isLoading = false;
                        boardService.sendRequest();
                    }, onError);
                    $scope.isLoading = true;
                }

            }, onError);
    };

    $scope.editCard = function editCard(ev, taskId) {
        $mdDialog.show({
            controller: CardController,
            templateUrl: '/App/Templates/EditCard.html',
            targetEvent: ev,
            locals: { taskId: taskId }
        })
            .then(function() {
                boardService.sendRequest();
            }, function() {
                
            });
    };

    function CardController($scope, $mdDialog, $http, taskId, taskService) {

        $scope.hide = function () {
            $mdDialog.hide();
        };
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.save = function (task) {
            taskService.saveTask(task);
            $mdDialog.hide(task);
        };

        taskService.getTask(taskId).then(function (data) {
            $scope.task = data;
        }, onError);

            
        
    }

    // Listen to the 'refreshBoard' event and refresh the board as a result
    $scope.$parent.$on("refreshBoard", function (e) {
        $scope.refreshBoard();
        $mdToast.show(
            $mdToast.simple()
            .content('Board updated successfully')
            .position('right')
            );
    });

    var onError = function (errorMessage) {
        $scope.isLoading = false;
        $mdToast.show($mdToast.simple().content('Error').position('right'));
    };

    init();
});