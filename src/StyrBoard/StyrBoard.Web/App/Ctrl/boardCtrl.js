var agileControllers = angular.module('agileControllers', []);

agileControllers.controller('boardCtrl', function ($scope, $rootScope, $mdToast, $mdDialog, $filter, boardService, taskService) {
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

    $scope.dragControlListeners = {
        //accept: function (sourceItemHandleScope, destSortableScope) {
        //    return true;

        //},
        itemMoved: function (event) {
            console.log('source ' + event.source.index);
            console.log('dest ' + event.dest.index);
            var taskId = event.source.itemScope.modelValue.Id;
            var targetColumnId = event.dest.sortableScope.$parent.col.Id;
            boardService.moveTask(taskId, targetColumnId).then(function (taskMoved) {
                $scope.isLoading = false;
                boardService.notifyCardUpdated(taskId);
            }, onError);
            $scope.isLoading = true;
        },
        //orderChanged: function (event) { },
        //containment: '#board'//optional param.
    };

    $scope.refreshBoard = function refreshBoard() {
        $scope.isLoading = true;
        boardService.getColumns()
           .then(function (data) {
               $scope.isLoading = false;
               $scope.columns = data;
           }, onError);
    };

    $scope.getCardById = function (id) {
        for (var i = 0; i < $scope.columns.length; i += 1) {
            for (var j = 0; j < $scope.columns[i].Tasks.length; j += 1) {
                if ($scope.columns[i].Tasks[j].Id === id) {
                    return $scope.columns[i].Tasks[j];
                }
            }
        }
    };
    $scope.getCardIndexById = function (id) {
        for (var i = 0; i < $scope.columns.length; i += 1) {
            for (var j = 0; j < $scope.columns[i].Tasks.length; j += 1) {
                if ($scope.columns[i].Tasks[j].Id === id) {
                    return j;
                }
            }
        }
    };

    $scope.getColumnIndexById = function (id) {
        for (var i = 0; i < $scope.columns.length; i += 1) {
            if ($scope.columns[i].Id === id) {
                return i;
            }
        }
    };
    $scope.deleteCard = function (id) {
        var currentCard = $scope.getCardById(id);
        var columnIndex = $scope.getColumnIndexById(currentCard.ColumnId);
        var cardIndex = $scope.getCardIndexById(id);
        $scope.columns[columnIndex].Tasks.splice(cardIndex, 1);
    }

    $scope.addCard = function (location) {
        var card = taskService.getTask(location);

    }

    $scope.updateCard = function (card) {
        var currentCard = $scope.getCardById(card.Id);
        if (card.ColumnId !== currentCard.ColumnId) {
            //card should be moved to a new column
            var sourceColumnIndex = $scope.getColumnIndexById(currentCard.ColumnId);
            var targetColumnIndex = $scope.getColumnIndexById(card.ColumnId);
            var sourceCardIndex = $scope.getCardIndexById(currentCard.Id);

            $scope.columns[sourceColumnIndex].Tasks.splice(sourceCardIndex, 1);
            $scope.columns[targetColumnIndex].Tasks.push(card);
        } else {
            //only card data has been updated
            currentCard.Name = card.Name;
            currentCard.Description = card.Description;
            currentCard.ColumnId = card.ColumnId;
        }
    }


    $scope.editCard = function editCard(ev, card) {
        $mdDialog.show({
            controller: 'editCardCtrl',
            templateUrl: '/App/Templates/EditCard.html',
            targetEvent: ev,
            locals: { card: card }
        })
            .then(function (action) {
                if (action === 'delete') {
                    $scope.deleteCard(card.Id);
                    toast('Card deleted successfully');
                }

            }, function () {

            });
    };

    $rootScope.$on('addNew', function (e) {
        var card = { Id: -1, Name: 'new', Description: 'body' }
        $scope.editCard(e, card);
    });

    // Listen to the 'refreshBoard' event and refresh the board as a result
    $rootScope.$on("refreshBoard", function (e) {
        $scope.refreshBoard();
        toast('Board updated successfully');
    });

    // Listen to the 'updateCard' event and refresh the card as a result
    $rootScope.$on("cardUpdated", function (evt, card) {
        $scope.updateCard(card);
        toast('Card updated successfully');
    });

    // Listen to the 'deleteCard' event and delete the card as a result
    $rootScope.$on("cardDeleted", function (evt, id) {
        $scope.deleteCard(id);
        toast('Card deleted successfully');
    });

    // Listen to the 'deleteCard' event and delete the card as a result
    $rootScope.$on("cardAdded", function (evt, location) {
        $scope.addCard(location);
        toast('Card added successfully');
    });

    var toast = function (message) {
        $mdToast.show(
            $mdToast.simple()
            .content(message)
            .position('right')
            );
    };

    var onError = function (errorMessage) {
        $scope.isLoading = false;
        toast('Error');
    };

    init();
});