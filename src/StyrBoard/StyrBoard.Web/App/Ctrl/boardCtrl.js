var agileControllers = angular.module('agileControllers', []);

agileControllers.controller('boardCtrl', function ($scope, $rootScope, $mdToast, $mdDialog, $filter, boardService, userStoryService) {
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
            var card = event.source.itemScope.modelValue;
            var targetColumnId = event.dest.sortableScope.$parent.col.Id;
            card.ColumnId = targetColumnId;
            boardService.moveTask(card.Id, targetColumnId).then(function (taskMoved) {
                $scope.isLoading = false;
                boardService.notifyCardUpdated(card.Id);
            }, onError);
            $scope.isLoading = true;
        },
        orderChanged: function(event) {
            var id = event.source.itemScope.card.Id;
            userStoryService.setPriority(id, 123);
        },
        //containment: '#board'//optional param.
    };

    $scope.sumOfPointsInColumn = function (id) {
        var index = $scope.getColumnIndexById(id);
        var sum = 0;
        for (var i = 0; i < $scope.columns[index].Cards.length; i += 1) {
            sum += $scope.columns[index].Cards[i].Points;
        }
        return sum;
    }

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
            for (var j = 0; j < $scope.columns[i].Cards.length; j += 1) {
                if ($scope.columns[i].Cards[j].Id === id) {
                    return $scope.columns[i].Cards[j];
                }
            }
        }
    };
    $scope.getCardIndexById = function (id) {
        for (var i = 0; i < $scope.columns.length; i += 1) {
            for (var j = 0; j < $scope.columns[i].Cards.length; j += 1) {
                if ($scope.columns[i].Cards[j].Id === id) {
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
        $scope.columns[columnIndex].Cards.splice(cardIndex, 1);
    }

    $scope.addCard = function (location) {
        userStoryService.get(location).then(function (card) {
            var columnIndex = $scope.getColumnIndexById(card.ColumnId);
            $scope.columns[columnIndex].Cards.push(card);
        });
    }

   
    $scope.updateCard = function (card) {
        $scope.deleteCard(card.Id);
        var columnIndex = $scope.getColumnIndexById(card.ColumnId);
        $scope.columns[columnIndex].Cards.push(card);
    }


    $scope.editCard = function editCard(ev, card) {
        $mdDialog.show({
            controller: 'editCardCtrl',
            templateUrl: '/App/Templates/EditCard.html',
            targetEvent: ev,
            locals: { card: card }
        })
            .then(function (action) {
                if (action === 'remove') {
                    $scope.deleteCard(card.Id);
                    toast('Card deleted successfully');
                }

            }, function () {

            });
    };

    $rootScope.$on('addNew', function (e) {
        var card = { Name: 'new', Description: 'body' }
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

    // Listen to the 'deleteCard' event and remove the card as a result
    $rootScope.$on("cardDeleted", function (evt, id) {
        $scope.deleteCard(id);
        toast('Card deleted successfully');
    });

    // Listen to the 'deleteCard' event and remove the card as a result
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