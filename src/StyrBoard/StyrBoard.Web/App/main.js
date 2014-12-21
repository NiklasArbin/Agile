var agile = agile || {};
agile.kanbanBoardApp = angular.module('kanbanBoardApp', ['ngMaterial', 'ui.sortable', 'ngRoute', 'agileControllers']);

agile.kanbanBoardApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/board', {
            templateUrl: '/App/Templates/KanbanBoard.html',
            controller: 'boardCtrl'
        }).
        when('/list', {
            templateUrl: '/App/Templates/List.html',
            controller: 'boardCtrl'
        }).
        otherwise({
            redirectTo: '/board'
        });
  }]);