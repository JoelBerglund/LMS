﻿(function () {
	var app = angular.module('KlassApp', []);

	app.controller('KlassMemberTable', ["$scope", "$http", function ($scope, $http) {
		$scope.KlassId = null;
		$scope.RemoveMember = function (UId) {
			$http.get("../RemoveKlassMember?Id=" + $scope.KlassId + "&UId=" + UId)
			.then(function Success(response) {
				//do UX reloading shit
			}, function Error(response) {
				alert(response);
			});
		}
	}]);
}());