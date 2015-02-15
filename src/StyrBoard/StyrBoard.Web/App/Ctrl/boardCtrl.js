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
            var prio = $scope.getNewPriority(card, 0);
            userStoryService.setPriority(card.Id, prio).then(function() {
                boardService.moveTask(card.Id, targetColumnId).then(function (taskMoved) {
                    $scope.isLoading = false;
                    boardService.notifyCardUpdated(card.Id);
                }, onError);
            });
            
            $scope.isLoading = true;
        },
        orderChanged: function (event) {
            var card = event.source.itemScope.card;
            var direction = event.dest.index - event.source.index;
            var prio = $scope.getNewPriority(card, direction);
            userStoryService.setPriority(card.Id, prio);
        },
        //containment: '#board'//optional param.
    };

    $scope.getNewPriority = function (card, direction) {
        var colIndex = $scope.getColumnIndexById(card.ColumnId);
        var col = $scope.columns[colIndex];

        for (var i = 0; i < col.Cards.length; i += 1) {
            if (col.Cards[i].Id === card.Id) {
                var prio = 0;
                if (i + 1 < col.Cards.length) {
                    //found card below
                    prio = col.Cards[i + 1].Priority;
                    if (direction > 0) {
                        //Move card down
                        prio = prio - 1;
                    }
                    return prio;
                }
                if (i !== 0) {
                    //found card above
                    prio = col.Cards[i - 1].Priority;
                    if (direction <= 0) {
                        //Move card up
                        prio = prio + 1;
                    }
                }
                if (prio !== 0) {
                    return prio;
                }
                return card.Priority;
            }
        }

    }

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

    // Listen to the 'cardPriorityChanged' event and update the card as a result
    $rootScope.$on("cardPriorityChanged", function (evt, list) {
        for (var i = 0; i < list.length; i += 1) {
            var card = $scope.getCardById(list[i].Key);
            card.Priority = list[i].Value;
        };
        toast('Cards changed priority successfully');
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