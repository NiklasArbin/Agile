var agileControllers = angular.module('agileControllers', []);

agileControllers.controller('boardCtrl', function ($scope, $rootScope, $mdToast, $mdDialog, $filter, boardService, userStoryService, sortingService) {
    // Model
    $scope.columns = [];
    $scope.isLoading = false;

    $scope.getCardsInColumn = function (columnId) {
        var colIndex = $scope.getColumnIndexById(columnId);
        return $scope.columns[colIndex].Cards;
    };

    $scope.dragControlListeners = {
        itemMoved: function (event) {
            var cards = $scope.getCardsInColumn(event.dest.sortableScope.$parent.col.Id);
            sortingService.itemMoved(event, cards);
        },
        orderChanged: function (event) {
            var cards = $scope.getCardsInColumn(event.dest.sortableScope.$parent.col.Id);
            sortingService.orderChanged(event, cards);
        },
    };

    function init() {
        $scope.isLoading = true;
        boardService.initialize().then(function (data) {
            $scope.isLoading = false;
            $scope.refreshBoard();
        }, onError);
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

    $scope.addCard = function (card) {
        var columnIndex = $scope.getColumnIndexById(card.ColumnId);
        $scope.columns[columnIndex].Cards.push(card);
    }


    $scope.updateCard = function (card) {
        $scope.deleteCard(card.Id);
        var columnIndex = $scope.getColumnIndexById(card.ColumnId);
        $scope.columns[columnIndex].Cards.push(card);
        $scope.columns[columnIndex].Cards.sort(function (a, b) {
            return a.Priority - b.Priority;
        });
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
                    $scope.toast('Card deleted successfully');
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
        $scope.toast('Board updated successfully');
    });

    // Listen to the 'updateCard' event and refresh the card as a result
    $rootScope.$on("cardUpdated", function (evt, card) {
        $scope.updateCard(card);
        $scope.toast('Card updated successfully');
    });

    // Listen to the 'deleteCard' event and remove the card as a result
    $rootScope.$on("cardDeleted", function (evt, id) {
        $scope.deleteCard(id);
        $scope.toast('Card deleted successfully');
    });


    $rootScope.$on("cardAdded", function (evt, card) {
        $scope.addCard(card);
        $scope.toast('Card added successfully');
    });

    // Listen to the 'cardPriorityChanged' event and update the card as a result
    $rootScope.$on("cardPriorityChanged", function (evt, list) {
        for (var i = 0; i < list.length; i += 1) {
            var card = $scope.getCardById(list[i].Key);
            card.Priority = list[i].Value;
        };
        $scope.toast('Cards changed priority successfully');
    });

    $scope.toast = function (message) {
        $mdToast.show(
            $mdToast.simple()
            .content(message)
            .position('right')
            );
    };

    var onError = function (errorMessage) {
        $scope.isLoading = false;
        $scope.toast('Error');
    };

    init();
});