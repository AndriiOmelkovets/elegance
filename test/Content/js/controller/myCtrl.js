(function () {
    'use strict';

    angular
        .module('app')
        .controller('myCtrl', myCtrl);

    myCtrl.$inject = ['postFactory'];

    function myCtrl(postFactory) {
        /* jshint validthis: true */
        var vm = this;
        vm.formData = {};
        vm.getData = getData;
        vm.messages = [];
        vm.postMessage = postMessage;

        getData();

        function getData() {
            postFactory.getData()
                .then(function (data) {
                    vm.messages = data;
                });
        }

        function postMessage() {
            if (vm.formData.text) {
                postFactory.createPost(vm.formData)
                    .then(function (data) {
                        vm.messages.push({
                            text: data.text,
                            creationDate: data.creationDate
                        });
                        vm.formData.text = '';
                    });
            }
        }
    }
})();
