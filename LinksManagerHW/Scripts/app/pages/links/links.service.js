(function (angular) {
	angular
        .module('linksModule')
        .factory('linksService', linksService);

	linksService.$inject = ['$http'];

	function linksService($http) {
		var service = {
			getLinks: getLinks,
			addLink: addLink,
			editLink: editLink,
			removeLink: removeLink
		};

		return service;

		function getLinks() {
			var promise = $http.get("/Link/GetLinks");
			return promise;
		};

		function addLink(link) {
			var promise = $http.post("/Link/AddLink", link);
			return promise;
		};

		function editLink(link) {
			var promise = $http.post("/Link/EditLink", link);
			return promise;
		};

		function removeLink(link) {
			var promise = $http.post("/Link/RemoveLink", link);
			return promise;
		}

	}
})(angular);