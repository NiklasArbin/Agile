agile.kanbanBoardApp.service('sortingService', function (boardService, userStoryService) {

    var itemMoved = function (event, cards) {
        var card = event.source.itemScope.modelValue;
        var targetColumnId = event.dest.sortableScope.$parent.col.Id;
        card.ColumnId = targetColumnId;
        var prio = getNewPriority(card, event.dest.index - event.source.index, cards);
        userStoryService.setPriority(card.Id, prio).then(function () {
            boardService.moveTask(card.Id, targetColumnId).then(function (taskMoved) {
                boardService.notifyCardUpdated(card.Id);
            });
        });
    };

    var orderChanged = function (event, cards) {
        var card = event.source.itemScope.card;
        var direction = event.dest.index - event.source.index;
        var prio = getNewPriority(card, direction, cards);
        userStoryService.setPriority(card.Id, prio);
    }


    var getNewPriority = function (card, direction, cards) {

        for (var i = 0; i < cards.length; i += 1) {
            if (cards[i].Id === card.Id) {
                var prio = 0;
                if (i + 1 < cards.length) {
                    //found card below
                    prio = cards[i + 1].Priority;
                    if (direction > 0) {
                        //Move card down
                        prio = prio - 1;
                    }
                    return prio;
                }
                if (i !== 0) {
                    //found card above
                    prio = cards[i - 1].Priority;
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
    };

    return {
        itemMoved: itemMoved,
        orderChanged: orderChanged
    };
});
