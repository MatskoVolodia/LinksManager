(function (angular) {
    var linksModule = angular
        .module("linksModule")
        .controller("LinkController", LinkController);

    LinkController.$inject = ['$scope', 'linksService'];

    linksModule.filter("jsDate", function () {
        return function (x) {
            return new Date(parseInt(x.substr(6)));
        };
    });

    function LinkController($scope, linksService) {
        var linksCtrl = this;

        linksCtrl.links = [];
        linksCtrl.user = "Volodymyr";
        linksCtrl.newLinkName = "";
        linksCtrl.onAddNewLink = onAddNewLink;

        linksCtrl.isLinkValid = isLinkValid;

        linksCtrl.itemOnEdit = null;
        linksCtrl.editedLinkName = "";
        linksCtrl.onEdit = false;
        linksCtrl.openEdit = openEdit;
        linksCtrl.closeEdit = closeEdit;
        linksCtrl.editLink = editLink;

        linksCtrl.removeLink = removeLink;
        linksCtrl.showAdder = false;
        linksCtrl.toggleAdder = toggleAdder;

        activate();

        function toggleAdder() {
            linksCtrl.showAdder = !linksCtrl.showAdder;
        }

        function activate() {
            var linksPromise = linksService.getLinks();
            linksPromise.then(function (response) {
                console.log("[LinkController] linksPromise - ", response);
                linksCtrl.links.push.apply(linksCtrl.links, response.data);
            });
        }

        function onAddNewLink() {
            var createdLink = { Id: 0, LinkName: linksCtrl.newLinkName, Date: Date.now() };
            linksService
                .addLink(createdLink)
                .then(function (response) {
                    console.log("[LinkController] onAddNewLink - success", arguments);
                    linksCtrl.links.push(response.data);
                    linksCtrl.newLinkName = "";
                }, function (response) {
                    console.log("[LinkController] onAddNewLink - fail", arguments);
                    alert("Adding a new link has failed.");
                });
        };

        function isLinkValid(urlToCheck) {
            var urlregex = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/;
            return urlregex.test(urlToCheck);
        };

        function openEdit(item) {
            linksCtrl.onEdit = true;
            linksCtrl.itemOnEdit = item;
            linksCtrl.editedLinkName = item.LinkName;
        };

        function closeEdit() {
            linksCtrl.onEdit = false;
            linksCtrl.itemOnEdit = null;
        };

        function editLink(link) {
            link.LinkName = linksCtrl.editedLinkName;
            linksService
                .editLink(link)
                .then(function (response) {
                    console.log("[LinkController] edit - success");
                }, function (response) {
                    console.log("[LinkController] edit - fail");
                    alert("Editing link has failed");
                });

            linksCtrl.closeEdit();
        };

        function removeLink(url) {
            linksService
                .removeLink(url)
                .then(function (response) {
                    console.log("[LinkController] remove - success");
                    angular.forEach(linksCtrl.links, function (link, index) {
                        if (url.Id === link.Id) {
                            linksCtrl.links.splice(index, 1);
                        }
                    });
                }, function (response) {
                    console.log("[LinkController] remove - fail");
                    alert("Removing a link has failed");
                });
        };
    };
})(angular);